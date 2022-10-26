using EvilGenius.MvxTabbedNavigation.Demo.Core.Model;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.Presenters.Hints;
using MvvmCross.ViewModels;
using System.Drawing;
using System.Windows.Input;

namespace EvilGenius.MvxTabbedNavigation.Demo.Core.ViewModels
{
    public sealed class SecureDataTabViewModel : MvxViewModel, IHasColor
    {
        private MvxSubscriptionToken _subscription;
        private readonly IMvxNavigationService _navigationService;

        private string _pleaseLoginString = "Please login to see secured string";

        private string _securedString = "Secure string, you see it";

        private string _stringValue;

        public string StringValue
        {
            get => _stringValue;
            set => SetProperty(ref _stringValue, value);
        }


        private bool _goAuthIsVisible;

        public bool GoAuthIsVisible
        {
            get => _goAuthIsVisible;
            set => SetProperty(ref _goAuthIsVisible, value);
        }

        public ICommand GoAuthCommand => new MvxCommand(() => _navigationService.ChangePresentation(new MvxPagePresentationHint(typeof(PhoneViewModel))));

        public Color Color { get; private set; }

        public SecureDataTabViewModel(IMvxMessenger messenger, IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            StringValue = _pleaseLoginString;
            GoAuthIsVisible = true;
            _subscription = messenger.Subscribe<LoggedInMessage>((m) => 
            { 
                StringValue = _securedString;
                GoAuthIsVisible = false;
            });
            Color = Resource.GetRandomColor();
        }
    }
}
