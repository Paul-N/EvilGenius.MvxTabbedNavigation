using EvilGenius.MvxTabbedNavigation.Demo.Core.Model;
using EvilGenius.MvxTabbedNavigation.Presenters.Hints;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using System.Drawing;
using System.Windows.Input;

namespace EvilGenius.MvxTabbedNavigation.Demo.Core.ViewModels
{
    public sealed class PhoneViewModel : MvxViewModel, IHasColor
    {
        private IMvxNavigationService _navigationService;

        private string _phoneNumber;

        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }

        public ICommand NextCommand => new MvxCommand(() => _navigationService.Navigate<SmsCodeViewModel>());

        public Color Color { get; private set; }

        public PhoneViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            Color = Resource.GetRandomColor();
        }
    }

    public sealed class SmsCodeViewModel : MvxViewModel, IHasColor
    {
        private IMvxNavigationService _navigationService;
        private readonly IMvxMessenger _messenger;
        private string _smsCode;

        public string SmsCode
        {
            get => _smsCode;
            set => SetProperty(ref _smsCode, value);
        }

        public ICommand NextCommand => new MvxCommand(() =>
        {
            _messenger.Publish(new LoggedInMessage(this));
            _navigationService.ChangePresentation(new ClearStackPresentationHint());
            _navigationService.Navigate<AccountViewModel>();
        });

        public Color Color { get; private set; }

        public SmsCodeViewModel(IMvxNavigationService navigationService, IMvxMessenger messenger)
        {
            _navigationService = navigationService;
            _messenger = messenger;
            Color = Resource.GetRandomColor();
        }
    }

    public sealed class AccountViewModel : MvxViewModel, IHasColor
    {
        public string AccountInfo => Resource.AccountInfo;

        public Color Color { get; private set; }

        public AccountViewModel() => Color = Resource.GetRandomColor();
    }
}
