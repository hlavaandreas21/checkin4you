using checkin4you.Shared.DTOs;

namespace checkin4you.Client.Services.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDTO>> GetAllReservationsForToday();
        Task<ReservationDTO> GetReservationByReservationIdAsync(string reservationId);
    }
}
