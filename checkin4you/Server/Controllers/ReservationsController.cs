using checkin4you.Server.DataAccess;
using checkin4you.Shared.DTOs;
using checkin4you.Shared.Models;
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

        [HttpGet]
        public IEnumerable<TblReservation> GetAllReservations() => _context.TblReservations.ToList();
        

        [HttpGet("today")]
        public IEnumerable<ReservationDTO> GetAllReservationsForToday()
        {
            DateTime today = new();

            DateTime date = new(2022, 6, 30);

            /*var reservations = _context.TblReservations
                .Where(r => r.ArrivalDate.Value.DayOfYear == today.DayOfYear)
                .ToList();*/

            /*var reservations = _context.TblReservations
                .Join(
                    _context.TblReservationsExts,
                    res => res.Idreservation,
                    resExt => resExt.Idreservation,
                    (res, resExt) => new { Reservation = res, ReservationExt = resExt })
                .Join(
                    _context.TblReservationsPartials,
                    res => res.Reservation.Idreservation,
                    resPart => resPart.Idreservation,
                    (res, resPart) => new { Reservation = res, ReservationPart = resPart })
                .Join(
                    _context.TblReservationGuests,
                    res => res.Reservation.Reservation.Idreservation,
                    resG => resG.Idreservation,
                    (res, resG) => new { Reservation = res, ReservationG = resG })
                .ToList();*/

            List<ReservationDTO> reservations = new();

            return reservations;
        }

        [HttpGet("{externalReservationId}")]
        public ReservationDTO GetReservationByReservationId(string externalReservationId)
        {
            DateTime date = new(2022, 1, 22);

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
                        if (tblReservation.StornoDate != null || !(tblReservation.ArrivalDate.Value.DayOfYear == date.DayOfYear && tblReservation.ArrivalDate.Value.Year == date.Year))
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

            return reservationFinal;
        }

    }
}
