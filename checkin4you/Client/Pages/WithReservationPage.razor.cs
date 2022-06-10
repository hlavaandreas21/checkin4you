using checkin4you.Client.Services.Interfaces;
using checkin4you.Client.Services.States;
using checkin4you.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace checkin4you.Client.Pages
{
    public partial class WithReservationPage : ComponentBase
    {
        [Inject]
        HttpClient HttpClient { get; set; } = default!;

        [Inject]
        IReservationService ReservationService { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        private IJSRuntime JSRuntime { get; set; } = default!;

        [Inject]
        ReservationStateService ReservationStateService { get; set; } = default!;

        ReservationDTO? Reservation { get; set; }

        private bool ReservationsLoaded { get; set; } = false;

        private bool ShowSpinner { get; set; } = false;

        private bool ShowInvalidMessage { get; set; } = false;

        private string DisplayNoneCssClass = "";

        public WithReservationPage() { }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            await JSRuntime.InvokeVoidAsync("eval", $@"document.getElementById(""reservationNumber"").focus()");
        }

        private async Task OnReservationIdInput(string value)
        {
            Reservation = new ReservationDTO();
            ReservationsLoaded = false;

            if (!string.IsNullOrEmpty(value) && value.Length == 10)
            {
                ShowSpinner = true;

                await Task.Delay(500);

                Reservation = await ReservationService.GetReservationByExternalResIdAsync(value);
                var checkedInReservationIds = ReservationStateService.CheckedInReservationIds;

                if (!Reservation.IsComplete) Reservation = null;
                foreach (var idReservation in Reservation.Idreservations)
                {
                    if (checkedInReservationIds.Contains(idReservation)) Reservation = null;
                }

                ShowSpinner = false;

                await Task.Delay(500);

                ReservationsLoaded = true;

                if (Reservation != null) DisplayNoneCssClass = "d-none";
            }

            await InvokeAsync(StateHasChanged);
        }

        private async Task AddGuest()
        {
            Reservation.Guests.Add(new()
            {
                Idguest = "",
                Name1 = "",
                Name2 = "",
                Name3 = "",
                Street = "",
                ZipCode = "",
                CityName = "",
                IdState = "",
                StateName = "",
                Birthdate = null,
                Email = ""
            });

            await InvokeAsync(StateHasChanged);
        }

        private void Cancel()
        {
            NavigationManager.NavigateTo("/home");
        }

        private void TryCheckIn()
        {
            var allGuestsValid = IsMainGuestComplete(Reservation.Guests.First());

            foreach (var guest in Reservation.Guests.Skip(1))
            {
                var isComplete = IsAdditionalGuestComplete(guest);
                if (!isComplete) allGuestsValid = false;
            }

            if (allGuestsValid)
            {
                ReservationStateService.SetRooms(Reservation.ItemCodes);
                foreach (var idReservation in Reservation.Idreservations)
                {
                    ReservationStateService.AddCheckedInReservationId(idReservation);
                }
                HttpClient.PostAsJsonAsync<List<string>>("api/reservations", ReservationStateService.CheckedInReservationIds.TakeLast(500).ToList());
                NavigationManager.NavigateTo("/checkedIn");
            }
            else ShowInvalidMessage = true;
        }

        private static bool IsMainGuestComplete(GuestDTO guest)
        {
            if (!string.IsNullOrEmpty(guest.Name1) &&
                !string.IsNullOrEmpty(guest.Name2) &&
                !string.IsNullOrEmpty(guest.Street) &&
                !string.IsNullOrEmpty(guest.ZipCode) &&
                !string.IsNullOrEmpty(guest.CityName) &&
                !string.IsNullOrEmpty(guest.StateName) &&
                guest.Birthdate != null && guest.Birthdate > new DateTime() &&
                !string.IsNullOrEmpty(guest.Phone))
            {
                return true;
            }
            else return false;
        }

        private static bool IsAdditionalGuestComplete(GuestDTO guest)
        {
            if (!string.IsNullOrEmpty(guest.Name1) &&
                !string.IsNullOrEmpty(guest.Name2) &&
                guest.Birthdate != null && guest.Birthdate > new DateTime())
            {
                return true;
            }
            else return false;
        }
    }
}
