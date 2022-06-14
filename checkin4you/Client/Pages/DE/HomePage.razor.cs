using Microsoft.AspNetCore.Components;

namespace checkin4you.Client.Pages.DE
{
    public partial class HomePage
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        private void NavigateToCheckinPage()
        {
            NavigationManager.NavigateTo("/checkin");
        }
    }
}
