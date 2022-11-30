using AndroidX.Lifecycle;
using MvvmCross.Base;
using MvvmCross.ViewModels;

namespace EvilGenius.MvxTabbedNavigation.Platforms.Android.ViewModels
{
    public class NativeViewModelHolder : ViewModel, INativeViewModelHolder //Should we implement ILifecycleObserver here?
    {
        public IMvxViewModel? ViewModel { get; private set; }

        public NativeViewModelHolder(IMvxViewModel viewModel) => ViewModel = viewModel;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                ViewModel?.DisposeIfDisposable();
            
            ViewModel = null;
            
            base.Dispose(disposing);
        }
    }
}
