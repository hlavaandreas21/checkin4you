using checkin4you.Client.Services.Interfaces;
using checkin4you.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace checkin4you.Client.Pages
{
    public partial class WithReservationPage : ComponentBase
    {
        [Inject]
        IReservationService ReservationService { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        private IJSRuntime JSRuntime { get; set; } = default!;

        ReservationDTO Reservation { get; set; }

        private bool ReservationsLoaded { get; set; } = false;

        private bool ShowSpinner { get; set; } = false;

        private string DisplayNoneCssClass = "";

        public WithReservationPage() {}

        public WithReservationPage(IReservationService reservationService)
        {
            ReservationService = reservationService;
        }

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

                Reservation = await ReservationService.GetReservationByReservationIdAsync(value);

                ShowSpinner = false;

                await Task.Delay(500);

                ReservationsLoaded = true;

                if (Reservation.Idreservations != null && Reservation.Idreservations.Any()) DisplayNoneCssClass = "d-none";
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
                Birthdate = new(),
                Email = ""
            });

            await InvokeAsync(StateHasChanged);
        }

        private void Cancel()
        {
            NavigationManager.NavigateTo("/home");
        }
    }
}
