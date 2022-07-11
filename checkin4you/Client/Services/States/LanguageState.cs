using Microsoft.AspNetCore.Components;

namespace checkin4you.Client.Services.States
{
    public class LanguageStateService
    {
        [Inject]
        NavigationManager NavigationManager { get; set; }

        public string DefaultLang { get; private set; } = "de";

        private readonly IConfiguration Config;

        public string PreviousLang { get; private set; } = "";
        public string CurrentLang { get; private set; } = "";

        public LanguageStateService(
            NavigationManager nav,
            IConfiguration defaultConfig)
        {
            NavigationManager = nav;
            Config = defaultConfig;
        }

        public void SetCurrentLang(string language)
        {
            PreviousLang = CurrentLang;

            CurrentLang = language;

            var uri = NavigationManager.Uri.ToString();

            if (uri.Contains("/" + CurrentLang + "/"))
            {
                return;
            }
            else if (uri == NavigationManager.BaseUri)
            {
                var newUri = uri + CurrentLang;
                NavigationManager.NavigateTo(newUri);
            }
            else if (uri.Contains("/" + PreviousLang))
            {
                var newUri = uri.Replace("/" + PreviousLang, "/" + CurrentLang);
                NavigationManager.NavigateTo(newUri);
            }


        }
    }
}
