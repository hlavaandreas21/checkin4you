using Microsoft.AspNetCore.Components;

namespace checkin4you.Client.Pages.IT
{
    public partial class HomePageIT
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        private void NavigateToCheckinPage()
        {
            NavigationManager.NavigateTo("/it/checkin");
        }
    }
}
