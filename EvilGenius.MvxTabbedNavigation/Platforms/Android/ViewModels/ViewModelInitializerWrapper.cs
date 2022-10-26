using AndroidX.Lifecycle;
using AndroidX.Lifecycle.ViewModels;
using Java.Lang;
using Microsoft.Extensions.Logging;
using MvvmCross;
using MvvmCross.Logging;
using MvvmCross.ViewModels;

namespace EvilGenius.MvxTabbedNavigation.Platforms.Android.ViewModels
{
    public class ViewModelInitializerWrapper
    {
        MvxViewModelRequest _vmRequest;
        private Class _class;
        private Func<CreationExtras, ViewModel> _generatorFunc;

        public ViewModelInitializerWrapper(MvxViewModelRequest vmRequest)
        {
            _vmRequest = vmRequest;
            _class = Class.FromType(typeof(NativeViewModelHolder));
            _generatorFunc = new Func<CreationExtras, ViewModel>(Create);
        }

        public ViewModelInitializer GetInitializer() => new ViewModelInitializer(_class, _generatorFunc.Kotlinize());

        private ViewModel Create(CreationExtras creationExtras)
        {
            var vm = LoadViewModelFrom(_vmRequest);
            var nativeViewModel = new NativeViewModelHolder(vm);

            return nativeViewModel;
        }

        //Stolen from \Mvx9\MvvmCross\Platforms\Android\Views\Fragments\MvxFragmentExtensions.cs
        private IMvxViewModel LoadViewModelFrom(MvxViewModelRequest request/*, IMvxBundle savedState = null*/)
        {
            var loader = Mvx.IoCProvider.Resolve<IMvxViewModelLoader>();
            var viewModel = loader.LoadViewModel(request, /*, savedState*/null);
            if (viewModel == null)
            {
                MvxLogHost.Default?.Log(LogLevel.Warning, "ViewModel not loaded for {viewModelType}", request.ViewModelType.FullName);
            }
            return viewModel;
        }
    }
}
