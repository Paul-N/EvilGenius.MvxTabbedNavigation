using System.Diagnostics.CodeAnalysis;
using EvilGenius.MvxTabbedNavigation.Demo.Core.Services;
using EvilGenius.MvxTabbedNavigation.Demo.Core.ViewModels;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace EvilGenius.MvxTabbedNavigation.Demo.Core;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
internal class AppStart : MvxAppStart
{
    private readonly ICurrentUserService _currentUserService;

    public AppStart(ICurrentUserService currentUserService, IMvxApplication application, IMvxNavigationService navigationService)
        : base(application, navigationService) => _currentUserService = currentUserService;

    protected override Task NavigateToFirstViewModel(object hint = null)
    {
        //_currentUserService.IsOnboardingPassed = false;
        return _currentUserService.IsOnboardingPassed ? NavigationService.Navigate<HomeViewModel>() : NavigationService.Navigate<StartViewModel>();
    }
}