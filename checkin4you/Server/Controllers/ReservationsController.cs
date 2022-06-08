using checkin4you.Server.DataAccess;
using checkin4you.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("today")]
        public IEnumerable<PossibleReservation> GetAllReservationsForToday()
        {
            //DateTime today = DateTime.Today;

            DateTime today = new(2021, 8, 7);


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

            return new ReservationDTO();
        }

        [HttpGet("byExternalResId/{externalReservationId}")]
        public ReservationDTO GetReservationByReservationId(string externalReservationId)
        {
            DateTime date = new(2021,8,7);

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

                    var tblReservation = _context.TblReservations.Single(r => r.Idreservation.ToString() == reservation.Idreservation.ToString());
                    if (tblReservation != null)
                    {
                        if (tblReservation.StornoDate != null || !(tblReservation.ArrivalDate?.DayOfYear == date.DayOfYear && tblReservation.ArrivalDate.Value.Year == date.Year))
                        {
                            reservationsToDeleteIds.Add(tblReservation.Idreservation.ToString().ToUpper());
                            continue;
                        }
                        reservationFinal.ArrivalDate = tblReservation.ArrivalDate;
                        reservationFinal.DepartureDate = tblReservation.DepartureDate;
                    }

                    var tblReservationPartial = _context.TblReservationsPartials.Single(r => r.Idreservation.ToString() == reservation.Idreservation.ToString());
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
                                StateName = "TODO",
                                Birthdate = guest.Birthdate,
                                Email = guest.Email
                            });
                        }
                    }
                }

                foreach (var roomId in reservationFinal.IdRooms)
                {
                    var roomCode = _context.TblItems.Single(i => i.Iditem.ToString() == roomId).ItemCode;
                    reservationFinal.ItemCodes.Add(roomCode);
                }

                reservationFinal.Idreservations.RemoveAll(res => reservationsToDeleteIds.Contains(res));

                reservationFinal.Guests.Reverse();
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
    }
}
