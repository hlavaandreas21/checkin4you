using checkin4you.Client.Services.Interfaces;
using checkin4you.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace checkin4you.Client.Pages
{
    public partial class WithoutReservationPage
    {
        [Inject]
        IReservationService ReservationService { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        private IJSRuntime JSRuntime { get; set; } = default!;

        ReservationDTO? Reservation { get; set; }

        IEnumerable<PossibleReservation> PossibleReservations { get; set; }

        private List<string> ResTexts { get; set; }

        private bool ReservationsLoaded { get; set; } = false;

        private bool ShowSpinner { get; set; } = false;

        private bool ShowInvalidMessage { get; set; } = false;

        private string DisplayNoneCssClass = "";

        public WithoutReservationPage() { }

        protected override async Task OnInitializedAsync()
        {
            PossibleReservations = await ReservationService.GetAllReservationsForTodayAsync();
            ResTexts = PossibleReservations.Select(pr => pr.ResText.ToLower()).ToList();
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            await JSRuntime.InvokeVoidAsync("eval", $@"document.getElementById(""guestName"").focus()");
        }

        private async Task OnGuestNameInput(string value)
        {
            ReservationsLoaded = false;

            var matches = ResTexts.Where(rt => rt == value.ToLower()).ToList();

            if (matches.Any())
            {
                string idReservations = "";

                var reservationsToLoad = PossibleReservations.Where(pr => pr.ResText.ToLower() == matches.First()).Select(pr => pr.IdReservation).ToList();

                foreach (var rtl in reservationsToLoad)
                {
                    idReservations += rtl + "&";
                }

                idReservations = idReservations.Remove(idReservations.Length - 1, 1);

                ShowSpinner = true;

                await Task.Delay(500);

                Reservation = await ReservationService.GetReservationByIdReservationsAsync(idReservations);
                if (!Reservation.IsComplete) Reservation = null;

                ShowSpinner = false;

                await Task.Delay(500);

                ReservationsLoaded = true;

                if (Reservation != null) DisplayNoneCssClass = "d-none";
            }
            else
            {
                return;
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

            if (allGuestsValid) NavigationManager.NavigateTo("/checkedIn");
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
                guest.Birthdate != null && guest.Birthdate > new DateTime())
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
