using checkin4you.Server.DataAccess;
using checkin4you.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace checkin4you.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly ILogger<ReservationsController> _logger;

        private readonly AidaX_kleiner_ItalienerContext _context;

        public ReservationsController(
            ILogger<ReservationsController> logger,
            AidaX_kleiner_ItalienerContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("GetCheckedInReservationIds")]
        public List<string> GetCheckedInReservationIds()
        {          
            try
            {
                var fileName = "checkedinReservations.json";
                
                if (!System.IO.File.Exists(fileName))
                {
                    System.IO.File.WriteAllText(fileName, "[]");
                }

                var fileContent = System.IO.File.ReadAllText(fileName);

                List<string> checkedInReservationIds = JsonSerializer.Deserialize<List<string>>(fileContent);

                if (checkedInReservationIds == null || checkedInReservationIds.Count < 1)
                {
                    _logger.LogInformation("[ReservationsController] Successfully retrieved CheckedInReservationIds. No CheckedInReservationIds found.");
                    return new();
                }
                else
                {
                    _logger.LogInformation("[ReservationsController] Successfully retrieved CheckedInReservationIds.", checkedInReservationIds);
                    return checkedInReservationIds;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("[ReservationsController] Error while retrieving CheckedInReservationIds.", ex.Message);
                return new();
            }
        }

        [HttpGet("Today")]
        public List<PossibleReservation> GetAllReservationsForToday()
        {
            try
            {
                DateTime today = DateTime.Today;

#if DEBUG
                today = new(2022, 6, 30);
#endif

                var tblReservations = _context.TblReservations
                    .Where(r => r.ArrivalDate.Value.DayOfYear == today.DayOfYear &&
                                r.ArrivalDate.Value.Year == today.Year &&
                                r.StornoDate == null)
                    .ToList();

                List<PossibleReservation> possibilities = new();

                foreach (var res in tblReservations)
                {
                    PossibleReservation possibility = new()
                    {
                        IdReservation = res.Idreservation.ToString(),
                        ResText = res.ResText
                    };

                    possibilities.Add(possibility);
                }

                _logger.LogInformation("[ReservationsController] Successfully loaded AllReservationsForToday.", possibilities);
                return possibilities;
            }
            catch (Exception ex)
            {
                _logger.LogError("[ReservationsController] Error while loading AllReservationsForToday.", ex.Message);
                return new();
            }
        }

        [HttpGet("ByIdReservations/{idReservations}")]
        public ReservationDTO GetReservationByIdReservations(string idReservations)
        {
            try
            {
                string[] ids = idReservations.Split("&");

                ReservationDTO reservationFinal = new()
                {
                    Idreservations = new(),
                    ExternalResIds = new(),
                    IdRooms = new(),
                    ItemCodes = new(),
                    Guests = new(),
                    GuestCount = 0
                };

                List<string> reservationsToDeleteIds = new();

                try
                {
                    foreach (var reservation in ids)
                    {
                        reservationFinal.Idreservations.Add(reservation);

                        var tblReservationsExt = _context.TblReservationsExts.SingleOrDefault(r => r.Idreservation.ToString() == reservation);
                        if (tblReservationsExt != null && !string.IsNullOrEmpty(tblReservationsExt.ExternalResId)) 
                            reservationFinal.ExternalResIds.Add(tblReservationsExt.ExternalResId);

                        var tblReservation = _context.TblReservations.SingleOrDefault(r => r.Idreservation.ToString() == reservation);
                        if (tblReservation != null)
                        {
                            if (tblReservation.StornoDate != null)
                            {
                                reservationsToDeleteIds.Add(tblReservation.Idreservation.ToString().ToUpper());
                                continue;
                            }
                            reservationFinal.ArrivalDate = tblReservation.ArrivalDate;
                            reservationFinal.DepartureDate = tblReservation.DepartureDate;
                        }

                        var tblReservationPartial = _context.TblReservationsPartials.SingleOrDefault(r => r.Idreservation.ToString() == reservation);
                        if (tblReservationPartial != null)
                        {
                            reservationFinal.IdRooms.Add(tblReservationPartial.Idroom.ToString());
                        }

                        var tblReservationsGuestsItems = _context.TblReservationGuests.Where(r => r.Idreservation.ToString() == reservation).ToList();
                        if (tblReservationsGuestsItems != null)
                        {
                            foreach (var guest in tblReservationsGuestsItems)
                            {
                                reservationFinal.Guests.Add(new GuestDTO()
                                {
                                    Idguest = guest.Idguest.ToString(),
                                    Name1 = guest.Name1,
                                    Name2 = guest.Name2,
                                    Name3 = guest.Name3,
                                    Street = guest.Street,
                                    ZipCode = guest.ZipCode,
                                    CityName = guest.CityName,
                                    IdState = guest.Idstate.ToString(),
                                    StateName = _context.TblStates.SingleOrDefault(st => st.Idstate == guest.Idstate).StateName,
                                    Birthdate = guest.Birthdate,
                                    Email = guest.Email
                                });
                            }

                            if (reservationFinal.Guests.Count < 1)
                            {
                                reservationFinal.Guests.Add(new());
                            }
                        }
                    }

                    foreach (var roomId in reservationFinal.IdRooms)
                    {
                        var roomCode = _context.TblItems.SingleOrDefault(i => i.Iditem.ToString() == roomId).ItemCode;
                        reservationFinal.ItemCodes.Add(roomCode);
                    }

                    reservationFinal.Idreservations.RemoveAll(res => reservationsToDeleteIds.Contains(res));

                    reservationFinal.ExternalResIds = reservationFinal.ExternalResIds.Distinct().ToList();
                    reservationFinal.Guests = reservationFinal.Guests.OrderByDescending(g => g.Idguest).ToList();

                    foreach (var idReservation in reservationFinal.Idreservations)
                    {
                        var tblReservationItems = _context.TblReservationItems
                            .Where(r => r.Idreservation.ToString() == idReservation)
                            .ToList();

                        if (tblReservationItems.Count > 0)
                        {
                            foreach (var res in tblReservationItems)
                            {
                                reservationFinal.GuestCount += res.PersNo.Value;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("[ReservationsController] Error while loading content for ReservationByIdReservations.", ex.Message);
                }

                if (reservationFinal.Idreservations != null && reservationFinal.Idreservations.Any() &&
                    reservationFinal.ArrivalDate != null &&
                    reservationFinal.DepartureDate != null &&
                    reservationFinal.IdRooms != null && reservationFinal.IdRooms.Any() &&
                    reservationFinal.ItemCodes != null && reservationFinal.ItemCodes.Any() &&
                    reservationFinal.Guests != null && reservationFinal.Guests.Any())
                {
                    reservationFinal.IsComplete = true;
                }
                else
                {
                    reservationFinal.IsComplete = false;
                }

                _logger.LogInformation("[ReservationsController] Successfully loaded ReservationByIdReservations.", reservationFinal);
                return reservationFinal;
            }
            catch (Exception ex)
            {
                _logger.LogError("[ReservationsController] Error while loading ReservationByIdReservations.", ex.Message);
                return new();
            }
        }

        [HttpGet("ByExternalResId/{externalReservationId}")]
        public ReservationDTO GetReservationByExternalResId(string externalReservationId)
        {
            try
            {
                DateTime today = DateTime.Today;

#if DEBUG
                today = new(2022, 6, 30);
#endif

                ReservationDTO reservationFinal = new()
                {
                    Idreservations = new(),
                    ExternalResIds = new(),
                    IdRooms = new(),
                    ItemCodes = new(),
                    Guests = new(),
                    GuestCount = 0
                };

                var tblReservationsExt = _context.TblReservationsExts
                    .Where(r => r.ExternalResId == externalReservationId)
                    .ToList();

                List<string> reservationsToDeleteIds = new();

                try
                {
                    foreach (var reservation in tblReservationsExt)
                    {
                        reservationFinal.Idreservations.Add(reservation.Idreservation.ToString().ToLower());
                        reservationFinal.ExternalResIds.Add(externalReservationId);

                        var tblReservation = _context.TblReservations.SingleOrDefault(r => r.Idreservation.ToString() == reservation.Idreservation.ToString());
                        if (tblReservation != null)
                        {
                            if (tblReservation.StornoDate != null || !(tblReservation.ArrivalDate?.DayOfYear == today.DayOfYear && tblReservation.ArrivalDate.Value.Year == today.Year))
                            {
                                reservationsToDeleteIds.Add(tblReservation.Idreservation.ToString().ToLower());
                                continue;
                            }
                            reservationFinal.ArrivalDate = tblReservation.ArrivalDate;
                            reservationFinal.DepartureDate = tblReservation.DepartureDate;
                        }

                        var tblReservationPartial = _context.TblReservationsPartials.SingleOrDefault(r => r.Idreservation.ToString() == reservation.Idreservation.ToString());
                        if (tblReservationPartial != null)
                        {
                            reservationFinal.IdRooms.Add(tblReservationPartial.Idroom.ToString());
                        }

                        var tblReservationsGuestsItems = _context.TblReservationGuests.Where(r => r.Idreservation.ToString() == reservation.Idreservation.ToString()).ToList();
                        if (tblReservationsGuestsItems != null)
                        {
                            foreach (var guest in tblReservationsGuestsItems)
                            {
                                reservationFinal.Guests.Add(new GuestDTO()
                                {
                                    Idguest = guest.Idguest.ToString(),
                                    Name1 = guest.Name1,
                                    Name2 = guest.Name2,
                                    Name3 = guest.Name3,
                                    Street = guest.Street,
                                    ZipCode = guest.ZipCode,
                                    CityName = guest.CityName,
                                    IdState = guest.Idstate.ToString(),
                                    StateName = _context.TblStates.SingleOrDefault(st => st.Idstate == guest.Idstate).StateName,
                                    Birthdate = guest.Birthdate,
                                    Phone = _context.TblGuests2s.SingleOrDefault(p => p.Idguest == guest.Idguest).Phone,
                                    Email = _context.TblGuests2s.SingleOrDefault(em => em.Idguest == guest.Idguest).Email
                                });
                            }

                            if (reservationFinal.Guests.Count < 1)
                            {
                                reservationFinal.Guests.Add(new());
                            }
                        }
                    }

                    foreach (var roomId in reservationFinal.IdRooms)
                    {
                        var roomCode = _context.TblItems.SingleOrDefault(i => i.Iditem.ToString() == roomId).ItemCode;
                        reservationFinal.ItemCodes.Add(roomCode);
                    }

                    reservationFinal.Idreservations.RemoveAll(res => reservationsToDeleteIds.Contains(res));

                    reservationFinal.ExternalResIds = reservationFinal.ExternalResIds.Distinct().ToList();
                    reservationFinal.Guests = reservationFinal.Guests.OrderByDescending(g => g.Idguest).ToList();

                    foreach (var idReservation in reservationFinal.Idreservations)
                    {
                        var tblReservationItems = _context.TblReservationItems
                            .Where(r => r.Idreservation.ToString() == idReservation)
                            .ToList();

                        if (tblReservationItems.Count > 0)
                        {
                            foreach (var res in tblReservationItems)
                            {
                                reservationFinal.GuestCount += res.PersNo.Value;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("[ReservationsController] Error while loading content for ReservationByExternalResId", ex.Message);
                }

                if (reservationFinal.Idreservations != null && reservationFinal.Idreservations.Any() &&
                    reservationFinal.ExternalResIds != null && reservationFinal.ExternalResIds.Any() &&
                    reservationFinal.ArrivalDate != null &&
                    reservationFinal.DepartureDate != null &&
                    reservationFinal.IdRooms != null && reservationFinal.IdRooms.Any() &&
                    reservationFinal.ItemCodes != null && reservationFinal.ItemCodes.Any() &&
                    reservationFinal.Guests != null && reservationFinal.Guests.Any())
                {
                    reservationFinal.IsComplete = true;
                }
                else
                {
                    reservationFinal.IsComplete = false;
                }

                _logger.LogInformation("[ReservationsController] Successfully loaded ReservationByExternalResId.", reservationFinal);
                return reservationFinal;
            }
            catch (Exception ex)
            {
                _logger.LogError("[ReservationsController] Error while loading ReservationByExternalResId", ex.Message);
                return new();
            }
        }

        [HttpPost]
        public void PersistCheckedInReservationIds([FromBody] List<string> checkedInReservationIds)
        {
            try
            {
                var fileName = "checkedinReservations.json";

                System.IO.File.WriteAllText(fileName, JsonSerializer.Serialize(checkedInReservationIds));

                _logger.LogInformation("[ReservationsController] Successfully logged CheckedInReservationIds.", checkedInReservationIds);
            }
            catch (Exception ex)
            {
                _logger.LogError("[ReservationsController] Error while logging CheckedInReservationIds.", ex.Message);
            }
        }
    }
}
