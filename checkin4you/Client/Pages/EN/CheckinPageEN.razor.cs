using Microsoft.AspNetCore.Components;

namespace checkin4you.Client.Pages.EN
{
    public partial class CheckinPageEN
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        private void NavigateToWithReservationPage()
        {
            NavigationManager.NavigateTo("/en/checkin/withReservation");
        }

        private void NavigateToWithoutReservationPage()
        {
            NavigationManager.NavigateTo("/en/checkin/withoutReservation");
        }
    }
}
