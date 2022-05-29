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
        public IEnumerable<TblReservation> GetAllReservations()
        {
            var reservations = _context.TblReservations.ToList();

            return _context.TblReservations.ToList();
        }

        [HttpGet("today")]
        public IEnumerable<ReservationDTO> GetAllReservationsForToday()
        {
            DateTime today = new();

            DateTime date = new(2022, 5, 20);

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

            var reservations = (from a in _context.TblReservations
                                join b in _context.TblReservationsExts on a.Idreservation equals b.Idreservation
                                join c in _context.TblReservationsPartials on a.Idreservation equals c.Idreservation
                                join d in _context.TblReservationGuests on a.Idreservation equals d.Idreservation
                                where a.ArrivalDate.Value.DayOfYear == date.DayOfYear && a.ArrivalDate.Value.Year == date.Year
                                select new ReservationDTO()
                                {
                                    Idreservation = a.Idreservation.ToString(),
                                    ExternalResId = b.ExternalResId,
                                    ArrivalDate = a.ArrivalDate.Value,
                                    DepartureDate = a.DepartureDate.Value,
                                    Idroom = c.Idroom.ToString(),
                                    Idguest = d.Idguest.ToString(),
                                    Name1 = d.Name1,
                                    Name2 = d.Name2,
                                    Name3 = d.Name3,
                                    Street = d.Street,
                                    ZipCode = d.ZipCode,
                                    CityName = d.CityName,
                                    Idstate = d.Idstate.ToString(),
                                    Birthdate = d.Birthdate.Value,
                                    Email = d.Email
                                })
                                .ToList();

            return null;
        }

    }
}
