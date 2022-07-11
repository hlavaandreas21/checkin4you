using checkin4you.Shared.DTOs;

namespace checkin4you.Client.Services.Interfaces
{
    public interface IReservationService
    {
        Task<List<PossibleReservation>> GetAllReservationsForTodayAsync();
        Task<ReservationDTO> GetReservationByIdReservationsAsync(string idReservations);
        Task<ReservationDTO> GetReservationByExternalResIdAsync(string externalResId);
    }
}
