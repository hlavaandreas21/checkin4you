using checkin4you.Server.DataAccess;
using checkin4you.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace checkin4you.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly AidaX_kleiner_ItalienerContext _context;

        public ReservationsController(AidaX_kleiner_ItalienerContext context)
        {
            _context = context;
        } 
        
        [HttpGet("[action]")]
        public List<string> GetCheckedInReservationIds()
        {
            using StreamReader file = System.IO.File.OpenText("checkedInReservations.json");
            JsonSerializer serializer = new();

            List<string> checkedInReservationIds = (List<string>)serializer.Deserialize(file, typeof(List<string>));

            if (checkedInReservationIds == null)
            {
                return new List<string>();
            }
            else
            {
                return checkedInReservationIds;
            }
        }

        [HttpGet("today")]
        public IEnumerable<PossibleReservation> GetAllReservationsForToday()
        {
            //DateTime today = DateTime.Today;

            DateTime today = new(2021, 11, 3);


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

            return possibilities;
        }

        [HttpGet("byIdReservations/{idReservations}")]
        public ReservationDTO GetReservationByIdReservations(string idReservations)
        {
            string[] ids = idReservations.Split("&");

            ReservationDTO reservationFinal = new()
            {
                Idreservations = new(),
                ExternalResIds = new(),
                IdRooms = new(),
                ItemCodes = new(),
                Guests = new()
            };

            List<string> reservationsToDeleteIds = new();

            try
            {
                foreach (var reservation in ids)
                {
                    reservationFinal.Idreservations.Add(reservation);

                    var tblReservationsExt = _context.TblReservationsExts.SingleOrDefault(r => r.Idreservation.ToString() == reservation);
                    if (!string.IsNullOrEmpty(tblReservationsExt.ExternalResId)) reservationFinal.ExternalResIds.Add(tblReservationsExt.ExternalResId);

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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

            return reservationFinal;
        }

        [HttpGet("byExternalResId/{externalReservationId}")]
        public ReservationDTO GetReservationByReservationId(string externalReservationId)
        {
            //DateTime today = new();

            DateTime today = new(2021, 11, 3);

            ReservationDTO reservationFinal = new(){
                Idreservations = new(),
                ExternalResIds = new(),
                IdRooms = new(),
                ItemCodes = new(),
                Guests = new()
            };

            var tblReservationsExt = _context.TblReservationsExts
                .Where(r => r.ExternalResId == externalReservationId)
                .ToList();

            List<string> reservationsToDeleteIds = new();

            try
            {
                foreach (var reservation in tblReservationsExt)
                {
                    reservationFinal.Idreservations.Add(reservation.Idreservation.ToString());
                    reservationFinal.ExternalResIds.Add(externalReservationId);

                    var tblReservation = _context.TblReservations.SingleOrDefault(r => r.Idreservation.ToString() == reservation.Idreservation.ToString());
                    if (tblReservation != null)
                    {
                        if (tblReservation.StornoDate != null || !(tblReservation.ArrivalDate?.DayOfYear == today.DayOfYear && tblReservation.ArrivalDate.Value.Year == today.Year))
                        {
                            reservationsToDeleteIds.Add(tblReservation.Idreservation.ToString().ToUpper());
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

            return reservationFinal;
        }

        [HttpPost]
        public void PersistCheckedInReservationIds([FromBody] List<string> checkedInReservationIds)
        {
            string json = JsonConvert.SerializeObject(checkedInReservationIds);
            System.IO.File.WriteAllText("checkedInReservations.json", json);
        }
    }
}
