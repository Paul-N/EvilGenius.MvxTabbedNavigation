#if NET6_0_OR_GREATER
using Microsoft.Maui.Storage;
#elif NETSTANDARD2_0_OR_GREATER
using Xamarin.Essentials;
#endif

namespace EvilGenius.MvxTabbedNavigation.Demo.Core.Services
{
    internal class CurrentUserService : ICurrentUserService
    {
        private string _isLoggedInKey = "__isLoggedIn";
        private string _isOnboardingPassedKey = "__isOnboardingPassed";


        public bool IsLoggedIn 
        {
            get => Preferences.Get(_isLoggedInKey, false);
            set => Preferences.Set(_isLoggedInKey, value);
        }
        
        public bool IsOnboardingPassed 
        {
            get => Preferences.Get(_isOnboardingPassedKey, false);
            set => Preferences.Set(_isOnboardingPassedKey, value);
        }
    }
}
