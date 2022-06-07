using Microsoft.AspNetCore.Components;

namespace checkin4you.Client.Pages
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
