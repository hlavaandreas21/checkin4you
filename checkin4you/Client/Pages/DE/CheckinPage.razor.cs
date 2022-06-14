using Microsoft.AspNetCore.Components;

namespace checkin4you.Client.Pages.DE
{
    public partial class CheckinPage
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        private void NavigateToWithReservationPage()
        {
            NavigationManager.NavigateTo("/de/checkin/withReservation");
        }

        private void NavigateToWithoutReservationPage()
        {
            NavigationManager.NavigateTo("/de/checkin/withoutReservation");
        }
    }
}
