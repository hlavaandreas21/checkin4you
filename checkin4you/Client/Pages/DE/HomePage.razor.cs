using checkin4you.Client.Services.States;
using Microsoft.AspNetCore.Components;

namespace checkin4you.Client.Pages.DE
{
    public partial class HomePage
    {
        [Inject]
        private LanguageStateService LanguageStateService { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        protected override void OnInitialized()
        {
            LanguageStateService.SetCurrentLang(LanguageStateService.DefaultLang);
        }

        private void NavigateToCheckinPage()
        {
            NavigationManager.NavigateTo("/de/checkin");
        }
    }
}
