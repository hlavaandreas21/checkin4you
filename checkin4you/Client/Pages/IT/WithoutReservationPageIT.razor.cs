using checkin4you.Client.Services.Interfaces;
using checkin4you.Client.Services.States;
using checkin4you.Shared.DTOs;
using checkin4you.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Text;

namespace checkin4you.Client.Pages.IT
{
    public partial class WithoutReservationPageIT
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

        List<PossibleReservation>? PossibleReservations { get; set; }

        private List<string>? ResTexts { get; set; }

        private bool ReservationsLoaded { get; set; } = false;

        private bool ShowSpinner { get; set; } = false;

        private bool ShowInvalidMessage { get; set; } = false;

        private string DisplayNoneCssClass = "";

        public WithoutReservationPageIT() { }

        protected override async Task OnInitializedAsync()
        {
            PossibleReservations = (await ReservationService.GetAllReservationsForTodayAsync()).ToList();
            var checkedInReservationIds = ReservationStateService.CheckedInReservationIds;
            PossibleReservations.RemoveAll(pr => checkedInReservationIds.Contains(pr.IdReservation));
            ResTexts = PossibleReservations.Select(pr => pr.ResText.ToLower()).ToList();
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            await JSRuntime.InvokeVoidAsync("eval", $@"document.getElementById(""guestName"").focus()");
        }

        private async Task OnGuestNameInput(string value)
        {
            if (value.Length > 4)
            {
                ReservationsLoaded = false;

                ShowSpinner = true;

                await Task.Delay(500);

                var matches = ResTexts.Where(rt => rt.Contains(value.ToLower())).Distinct().ToList();

                if (matches.Any() && matches.Count == 1)
                {
                    string idReservations = "";

                    var reservationsToLoad = PossibleReservations.Where(pr => pr.ResText.ToLower().Contains(matches.First())).Select(pr => pr.IdReservation).ToList();

                    foreach (var rtl in reservationsToLoad)
                    {
                        idReservations += rtl + "&";
                    }

                    idReservations = idReservations.Remove(idReservations.Length - 1, 1);

                    Reservation = await ReservationService.GetReservationByIdReservationsAsync(idReservations);
                    if (!Reservation.IsComplete) Reservation = null;
                }

                ShowSpinner = false;

                await Task.Delay(500);

                ReservationsLoaded = true;

                if (Reservation != null) DisplayNoneCssClass = "d-none";

                await InvokeAsync(StateHasChanged);
            }
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
            NavigationManager.NavigateTo("/it/home");
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

                MailRequest mailRequest = new()
                {
                    ToEmail = "hlavaandreas@gmail.com",
                    Subject = "Checkin " + Reservation.Guests.First().Name1 + " " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"),
                    Body = GenerateEmailBody(Reservation)
                };

                HttpClient.PostAsJsonAsync<MailRequest>("api/Email", mailRequest);
                NavigationManager.NavigateTo("/it/checkedIn");
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

        private static string GenerateEmailBody(ReservationDTO reservation)
        {
            StringBuilder sb = new();

            var commaSeperatedIdReservations = reservation.Idreservations.Select(idr => idr.ToString()).ToList();

            sb.AppendLine("Reservierungs-IDs: " + string.Join("; ", reservation.Idreservations));
            if (reservation.ExternalResIds.Any())
            {
                sb.AppendLine("Externe Buchungsnummern: " + string.Join("; ", reservation.ExternalResIds));
            }
            else
            {
                sb.AppendLine("Keine externe Reservierung!");
            }
            sb.AppendLine();
            sb.AppendLine("Ankunft: " + reservation.ArrivalDate?.ToString("dd.MM.yyyy"));
            sb.AppendLine("Abreise: " + reservation.DepartureDate?.ToString("dd.MM.yyyy"));
            sb.AppendLine("Anzahl der Nächte: " + (reservation.DepartureDate.Value.Date - reservation.ArrivalDate.Value.Date).Days);
            sb.AppendLine();
            sb.AppendLine("Zimmer: " + string.Join(", ", reservation.ItemCodes));
            sb.AppendLine();

            var mainGuest = reservation.Guests.First();
            sb.AppendLine("Hauptgast:");
            sb.AppendLine(mainGuest.Name1 + " " + mainGuest.Name2 + ", Geburtsdatum: " + mainGuest.Birthdate?.ToString("dd.MM.yyyy"));
            sb.AppendLine(mainGuest.Street + ", " + mainGuest.ZipCode + " " + mainGuest.CityName + ", " + mainGuest.StateName);
            sb.AppendLine("Telefonnummer: " + mainGuest.Phone);
            if (!string.IsNullOrEmpty(mainGuest.Email))
            {
                sb.AppendLine("E-Mail: " + mainGuest.Email);
            }
            sb.AppendLine();

            if (reservation.Guests.Count > 1)
            {
                sb.AppendLine("Mitreisende:");
                foreach (var guest in reservation.Guests.Skip(1))
                {
                    sb.AppendLine(guest.Name1 + " " + guest.Name2 + ", Geburtsdatum: " + guest.Birthdate?.ToString("dd.MM.yyyy"));
                }
                sb.AppendLine();
            }

            var taxCount = reservation.Guests.Count(g => GetCurrentAgeOfGuest(g) >= 12);

            sb.AppendLine("Anzahl der zu erhebenden Ortstaxen: " + taxCount);

            return sb.ToString();
        }

        private static int GetCurrentAgeOfGuest(GuestDTO guest)
        {
            int years = DateTime.Now.Year - guest.Birthdate.Value.Year;
            if (DateTime.Now.Month < guest.Birthdate.Value.Month || (DateTime.Now.Month == guest.Birthdate.Value.Month && DateTime.Now.Day < guest.Birthdate.Value.Day)) years--;
            return years;
        }
    }
}
