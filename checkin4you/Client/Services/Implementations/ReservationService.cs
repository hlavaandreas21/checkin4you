using checkin4you.Client.Services.Interfaces;
using checkin4you.Shared.DTOs;
using System.Collections.Generic;
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

        public async Task<IEnumerable<ReservationDTO>> GetAllReservationsForToday() 
            => await _httpClient.GetFromJsonAsync<IEnumerable<ReservationDTO>>("api/reservations/today");

        public async Task<ReservationDTO> GetReservationByReservationIdAsync(string reservationId) 
            => await _httpClient.GetFromJsonAsync<ReservationDTO>("api/reservations/" + reservationId);
    }
}
