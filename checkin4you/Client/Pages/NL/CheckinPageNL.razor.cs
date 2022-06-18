using Microsoft.AspNetCore.Components;

namespace checkin4you.Client.Pages.NL
{
    public partial class CheckinPageNL
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        private void NavigateToWithReservationPage()
        {
            NavigationManager.NavigateTo("/nl/checkin/withReservation");
        }

        private void NavigateToWithoutReservationPage()
        {
            NavigationManager.NavigateTo("/nl/checkin/withoutReservation");
        }
    }
}
