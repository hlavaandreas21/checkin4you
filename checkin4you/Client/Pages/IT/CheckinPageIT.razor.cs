using Microsoft.AspNetCore.Components;

namespace checkin4you.Client.Pages.IT
{
    public partial class CheckinPageIT
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        private void NavigateToWithReservationPage()
        {
            NavigationManager.NavigateTo("/it/checkin/withReservation");
        }

        private void NavigateToWithoutReservationPage()
        {
            NavigationManager.NavigateTo("/it/checkin/withoutReservation");
        }
    }
}
