using checkin4you.Client.Services.States;
using Microsoft.AspNetCore.Components;

namespace checkin4you.Client.Pages.EN
{
    public partial class CheckedInPageEN : IDisposable
    {
        [Inject]
        ReservationStateService ReservationStateService { get; set; } = default!;

        [Inject]
        NavigationManager NavigationManager { get; set; } = default!;

        private List<string>? Rooms { get; set; }

        protected override void OnInitialized()
        {
            Rooms = ReservationStateService.Rooms;
        }

        private void BackToHome()
        {
            NavigationManager.NavigateTo("/en/home");
        }

        public void Dispose()
        {
            ReservationStateService.SetRooms(null);
            GC.SuppressFinalize(this);
        }
    }
}
