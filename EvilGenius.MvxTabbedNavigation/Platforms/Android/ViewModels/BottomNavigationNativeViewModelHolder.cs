using EvilGenius.MvxTabbedNavigation.Platforms.Android.Views;
using MvvmCross.ViewModels;

namespace EvilGenius.MvxTabbedNavigation.Platforms.Android.ViewModels
{
    public class BottomNavigationNativeViewModelHolder : NativeViewModelHolder
    {
        public TabPresentationData TabPresentationData { get; private set; }

        public BottomNavigationNativeViewModelHolder(IMvxViewModel viewModel) : base(viewModel)
        {
            TabPresentationData = new TabPresentationData();
        }
    }
}
