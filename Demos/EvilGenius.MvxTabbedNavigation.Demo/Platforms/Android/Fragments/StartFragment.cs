using Android.OS;
using Android.Views;
using EvilGenius.MvxTabbedNavigation.Platforms.Android.Views;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Views.Fragments;
using EvilGenius.MvxTabbedNavigation.Demo.Core.ViewModels;
using CoreResource = EvilGenius.MvxTabbedNavigation.Demo.Core.Resource;
using View = Android.Views.View;
using Android.Runtime;
using EvilGenius.MvxTabbedNavigation.Demo.Platforms.Android.Activities;
using EvilGenius.MvxTabbedNavigation.Platforms.Android.Presenters.Attributes;
#if SINGLE_PRJ
using AndroidResource = EvilGenius.MvxTabbedNavigation.Demo.Resource;
#endif
#if ANDROID_PRJ
using AndroidResource = EvilGenius.MvxTabbedNavigation.DemoMvx8.Android.Resource;
#endif

namespace EvilGenius.MvxTabbedNavigation.Demo.Platforms.Android.Fragments
{
    [Register("org.evilgenius.tabbednavigation.fragments.StartFragment")]
    [RootFragmentPresentation(HostActivityType = typeof(MainActivity))]
    internal sealed class StartFragment : FragmentWithViewModel<StartViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this.EnsureBindingContextIsSet();

            var view = this.BindingInflate(AndroidResource.Layout.fragment_start, null);

            view.SetupTitledTextView(AndroidResource.Id.txtHello, this.Resources, CoreResource.Thanks);

            view.SetTextTo(AndroidResource.Id.btnStart, CoreResource.Start);

            return view;
        }
    }
}
