using checkin4you.Client.Services.Interfaces;
using checkin4you.Shared.DTOs;
using System.Net.Http.Json;

namespace checkin4you.Client.Services.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly ILogger<ReservationService> _logger;
        private readonly HttpClient _httpClient;

        public ReservationService(
            ILogger<ReservationService> logger,
            HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<List<PossibleReservation>> GetAllReservationsForTodayAsync()
        {
            try
            {
                var possibleReservations = await _httpClient.GetFromJsonAsync<List<PossibleReservation>>("api/reservations/Today");

                _logger.LogInformation("[ReservationService] Successfully retrieved PossibleReservations.", possibleReservations);
                return possibleReservations;
            }
            catch (Exception ex)
            {
                _logger.LogError("[ReservationService] Error while loading PossibleReservations.", ex.Message);
                return new();
            }
        }

        public async Task<ReservationDTO> GetReservationByIdReservationsAsync(string idReservations)
        {
            try
            {
                var reservation = await _httpClient.GetFromJsonAsync<ReservationDTO>("api/reservations/ByIdReservations/" + idReservations);

                _logger.LogInformation("[ReservationService] Successfully retrieved Reservation by IdReservations.", reservation);
                return reservation;
            }
            catch (Exception ex)
            {
                _logger.LogError("[ReservationService] Error while loading Reservation by IdReservation.", ex.Message);
                return new();
            }
        }

        public async Task<ReservationDTO> GetReservationByExternalResIdAsync(string reservationId)
        {
            try
            {
                var reservation = await _httpClient.GetFromJsonAsync<ReservationDTO>("api/reservations/ByExternalResId/" + reservationId);

                _logger.LogInformation("[ReservationService] Successfully retrieved Reservation by ExternalResId.", reservation);
                return reservation;
            }
            catch (Exception ex)
            {
                _logger.LogError("[ReservationService] Error while loading Reservation by ExternalResId.", ex.Message);
                return new();
            }
        }
    }
}
