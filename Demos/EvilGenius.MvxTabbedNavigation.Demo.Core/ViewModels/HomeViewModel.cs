using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace EvilGenius.MvxTabbedNavigation.Demo.Core.ViewModels
{
    public sealed class HomeViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;
        private bool _areTabsShown;

        public HomeViewModel(IMvxNavigationService navigationService) 
            => this.navigationService = navigationService;

        public override void ViewAppeared()
        {
            if (!_areTabsShown)
            {
                navigationService.Navigate<Tab1ViewModel>();
                navigationService.Navigate<Tab2ViewModel>();
                navigationService.Navigate<Tab3ViewModel>();
                navigationService.Navigate<SecureDataTabViewModel>();
                navigationService.Navigate<PhoneViewModel>();
                _areTabsShown = true;
            }
        }
    }
}
