using Microsoft.AspNetCore.Components;

namespace checkin4you.Client.Pages.NL
{
    public partial class HomePageNL
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        private void NavigateToCheckinPage()
        {
            NavigationManager.NavigateTo("/nl/checkin");
        }
    }
}
