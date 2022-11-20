using EvilGenius.MvxTabbedNavigation.Demo.Core.Services;
using EvilGenius.MvxTabbedNavigation.Demo.Core.ViewModels;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace EvilGenius.MvxTabbedNavigation.Demo.Core
{
    internal class AppStart : MvxAppStart, IMvxAppStart
    {
        private ICurrentUserService _currentUserService;

        public AppStart(ICurrentUserService currentUserService, IMvxApplication application, IMvxNavigationService navigationService)
            : base(application, navigationService) => _currentUserService = currentUserService;

        protected override Task NavigateToFirstViewModel(object hint = null)
        {
            //_currentUserService.IsOnboardingPassed = false;
            if (_currentUserService.IsOnboardingPassed)
                return NavigationService.Navigate<HomeViewModel>();
            else
                return NavigationService.Navigate<StartViewModel>();
        }
    }
}
