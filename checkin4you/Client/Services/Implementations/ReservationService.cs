using checkin4you.Client.Services.Interfaces;
using checkin4you.Shared.DTOs;
using System.Net.Http.Json;

namespace checkin4you.Client.Services.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly HttpClient _httpClient;

        public ReservationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<PossibleReservation>> GetAllReservationsForTodayAsync()
            => await _httpClient.GetFromJsonAsync<IEnumerable<PossibleReservation>>("api/reservations/today");

        public async Task<ReservationDTO> GetReservationByIdReservationsAsync(string idReservations)
            => await _httpClient.GetFromJsonAsync<ReservationDTO>("api/reservations/ByIdReservations/" + idReservations);

        public async Task<ReservationDTO> GetReservationByExternalResIdAsync(string reservationId)
            => await _httpClient.GetFromJsonAsync<ReservationDTO>("api/reservations/ByExternalResId/" + reservationId);
    }
}
