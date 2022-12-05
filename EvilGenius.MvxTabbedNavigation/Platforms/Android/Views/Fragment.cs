using Android.OS;
using Android.Runtime;
using AndroidX.Lifecycle;
using EvilGenius.MvxTabbedNavigation.Platforms.Android.ViewModels;
using MvvmCross;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters;
using MvvmCross.Platforms.Android.Views;
using MvvmCross.Platforms.Android.Views.Fragments;
using MvvmCross.Platforms.Android.Views.Fragments.EventSource;
using MvvmCross.ViewModels;
using JavaClass = Java.Lang.Class;

namespace EvilGenius.MvxTabbedNavigation.Platforms.Android.Views
{
    [Register("org.evilgenius.mvxtabbednavigation.platforms.android.views.Fragment")]
    public class Fragment : MvxEventSourceFragment, IMvxFragmentView
    {
        protected INativeViewModelHolder _viewModelHolder;

        public string UniqueImmutableCacheTag => string.Empty; //we don't need this

        private IMvxBindingContext? _bindingContext;

        public IMvxBindingContext? BindingContext
        {
            get => _bindingContext;
            set
            {
                _bindingContext = value;
                if(_bindingContext != null)
                    _bindingContext.DataContext = _viewModelHolder?.ViewModel;
            }
        }

        public object? DataContext
        {
            get => _viewModelHolder?.ViewModel;
            set
            {
                //What should we do here? View model is constructed and assigned inside the fragment. Throw an exception?
#if DEBUG
                if (System.Diagnostics.Debugger.IsAttached)
                    System.Diagnostics.Debugger.Break();
#endif
            }
        }

        public virtual IMvxViewModel? ViewModel
        {
            get => _viewModelHolder?.ViewModel;
            set
            {
                //What should we do here? View model is constructed and assigned inside the fragment. Throw an exception?
#if DEBUG
                if (System.Diagnostics.Debugger.IsAttached)
                    System.Diagnostics.Debugger.Break();
#endif
            }
        }

        protected Fragment(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { }

        protected Fragment() => this.AddEventListeners();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (Arguments != null 
                && Arguments.GetString(MvxAndroidViewPresenter.ViewModelRequestBundleKey) is string serializedRequest
                && Mvx.IoCProvider?.TryResolve<IMvxNavigationSerializer>(out IMvxNavigationSerializer navigationSerializer) == true
                && navigationSerializer.Serializer is IMvxTextSerializer textSerializer)
            {
                var vmRequest = textSerializer.DeserializeObject<MvxViewModelRequest>(serializedRequest);
                var vmInitializerWrapper = new ViewModelInitializerWrapper(vmRequest, ViewholderHolderType);
                var clazz = JavaClass.FromType(ViewholderHolderType);
                var myViewModel = new ViewModelProvider(this, ViewModelProvider.Factory.Companion.From(vmInitializerWrapper.GetInitializer())).Get(clazz);
                _viewModelHolder = myViewModel as INativeViewModelHolder;
                OnViewModelSet();
                ViewModel?.ViewCreated();
            }
        }

        public virtual void OnViewModelSet() { }

        public override void OnDestroy()
        {
            base.OnDestroy();
            ViewModel?.ViewDestroy(viewFinishing: IsRemoving || Activity == null || Activity.IsFinishing);
        }

        public override void OnStart()
        {
            base.OnStart();
            ViewModel?.ViewAppearing();
        }

        public override void OnResume()
        {
            base.OnResume();
            ViewModel?.ViewAppeared();
        }

        public override void OnPause()
        {
            base.OnPause();
            ViewModel?.ViewDisappearing();
        }

        public override void OnStop()
        {
            base.OnStop();
            ViewModel?.ViewDisappeared();
        }

        protected virtual Type ViewholderHolderType => typeof(NativeViewModelHolder);
    }

    public abstract class Fragment<TViewModel> : Fragment, IMvxFragmentView<TViewModel>
        where TViewModel : class, IMvxViewModel
    {
        protected Fragment() { }

        protected Fragment(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { }

        public new TViewModel? ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public MvxFluentBindingDescriptionSet<IMvxFragmentView<TViewModel>, TViewModel> CreateBindingSet() 
            => this.CreateBindingSet<IMvxFragmentView<TViewModel>, TViewModel>();
    }
}
