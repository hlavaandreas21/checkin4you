using Microsoft.AspNetCore.Components;

namespace checkin4you.Client.Pages.DE
{
    public partial class CheckinPage
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        private void NavigateToWithReservationPage()
        {
            NavigationManager.NavigateTo("/checkin/withReservation");
        }

        private void NavigateToWithoutReservationPage()
        {
            NavigationManager.NavigateTo("/checkin/withoutReservation");
        }
    }
}
