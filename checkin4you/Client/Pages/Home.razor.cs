
using Microsoft.AspNetCore.Components;

namespace checkin4you.Client.Pages
{
    public partial class Home
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public Home() {}

        public void NavigateToCheckin()
        {
            NavigationManager.NavigateTo("/checkin");
        }
    }
}
