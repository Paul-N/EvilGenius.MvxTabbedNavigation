using MvvmCross.ViewModels;

namespace EvilGenius.MvxTabbedNavigation.Platforms.Android.ViewModels;

public interface INativeViewModelHolder
{
    public IMvxViewModel? ViewModel { get; }
}