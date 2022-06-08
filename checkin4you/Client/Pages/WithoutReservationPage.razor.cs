using checkin4you.Client.Services.Interfaces;
using checkin4you.Shared.DTOs;
using Microsoft.AspNetCore.Components;

namespace checkin4you.Client.Pages
{
    public partial class WithoutReservationPage
    {
        [Inject]
        IReservationService ReservationService { get; set; } = default!;

        IEnumerable<ReservationDTO> TodaysReservations { get; set; }

        protected override async Task OnInitializedAsync()
        {
            TodaysReservations = await ReservationService.GetAllReservationsForToday();
        }
    }
}
