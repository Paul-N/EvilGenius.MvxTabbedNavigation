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
        private readonly Type _nativeVMHolderType;
        private Class _class;
        private Func<CreationExtras, ViewModel> _generatorFunc;

        public ViewModelInitializerWrapper(MvxViewModelRequest vmRequest, Type nativeVMHolderType)
        {
            _vmRequest = vmRequest;
            _nativeVMHolderType = nativeVMHolderType;
            _class = Class.FromType(_nativeVMHolderType);
            _generatorFunc = new Func<CreationExtras, ViewModel>(Create);
        }

        public ViewModelInitializer GetInitializer() => new ViewModelInitializer(_class, _generatorFunc.Kotlinize());

        private ViewModel Create(CreationExtras creationExtras)
        {
            var vm = LoadViewModelFrom(_vmRequest);
            return  (ViewModel)Activator.CreateInstance(_nativeVMHolderType, vm);
        }

        //Stolen from \Mvx9\MvvmCross\Platforms\Android\Views\Fragments\MvxFragmentExtensions.cs
        private IMvxViewModel LoadViewModelFrom(MvxViewModelRequest request)
        {
            var loader = Mvx.IoCProvider.Resolve<IMvxViewModelLoader>();
            var viewModel = loader.LoadViewModel(request, null);
            if (viewModel == null)
            {
                MvxLogHost.Default?.Log(LogLevel.Warning, "ViewModel not loaded for {viewModelType}", request.ViewModelType.FullName);
            }
            return viewModel;
        }
    }
}
