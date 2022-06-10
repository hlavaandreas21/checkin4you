using checkin4you.Client.Services.States;
using Microsoft.AspNetCore.Components;

namespace checkin4you.Client.Pages
{
    public partial class CheckedInPage : IDisposable
    {
        [Inject]
        ReservationStateService ReservationStateService { get; set; } = default!;

        private List<string>? Rooms { get; set; }

        protected override void OnInitialized()
        {
            Rooms = ReservationStateService.Rooms;
        }

        public void Dispose()
        {
            ReservationStateService.SetRooms(null);
            GC.SuppressFinalize(this);
        }
    }
}
