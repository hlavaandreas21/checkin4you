using Microsoft.AspNetCore.Components;

namespace checkin4you.Client.Services.States
{
    public class LanguageStateService
    {
        [Inject]
        NavigationManager NavigationManager { get; set; }

        public string DefaultLang { get; private set; } = "de";
        public string PreviousLang { get; private set; } = "";
        public string CurrentLang { get; private set; } = "";

        public LanguageStateService(NavigationManager nav)
        {
            NavigationManager = nav;
        }

        public void SetCurrentLang(string language)
        {
            PreviousLang = CurrentLang;

            CurrentLang = language;

            var uri = NavigationManager.Uri.ToString();

            if (uri.Contains("/" + CurrentLang))
            {
                return;
            }
            else if (uri == NavigationManager.BaseUri)
            {
                var newUri = uri + CurrentLang;
                NavigationManager.NavigateTo(newUri);
            }
            else if (uri.Contains("/" + PreviousLang) && PreviousLang != "")
            {
                var newUri = uri.Replace("/" + PreviousLang, "/" + CurrentLang);
                NavigationManager.NavigateTo(newUri);
            }
        }
    }
}
