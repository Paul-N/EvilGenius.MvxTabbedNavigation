using MvvmCross.Navigation.EventArguments;
using MvvmCross.ViewModels;

namespace EvilGenius.Mvx.TabNavigation.Core.ViewModels
{
    public class NullProviderViewModelLoader : IMvxViewModelLoader
    {
        public IMvxViewModel LoadViewModel(MvxViewModelRequest request, IMvxBundle savedState, IMvxNavigateEventArgs navigationArgs = null) => null;

        public IMvxViewModel LoadViewModel<TParameter>(MvxViewModelRequest request, TParameter param, IMvxBundle savedState, IMvxNavigateEventArgs navigationArgs = null) where TParameter : notnull
        {
            throw new System.NotImplementedException();
        }

        public IMvxViewModel ReloadViewModel(IMvxViewModel viewModel, MvxViewModelRequest request, IMvxBundle savedState, IMvxNavigateEventArgs navigationArgs = null)
            => null;

        public IMvxViewModel ReloadViewModel<TParameter>(IMvxViewModel<TParameter> viewModel, TParameter param, MvxViewModelRequest request, IMvxBundle savedState, IMvxNavigateEventArgs navigationArgs = null) where TParameter : notnull
        {
            throw new System.NotImplementedException();
        }
    }
}
