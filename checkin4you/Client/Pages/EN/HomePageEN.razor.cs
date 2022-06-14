using Microsoft.AspNetCore.Components;

namespace checkin4you.Client.Pages.EN
{
    public partial class HomePageEN
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        private void NavigateToCheckinPage()
        {
            NavigationManager.NavigateTo("/en/checkin");
        }
    }
}
