using Android.OS;
using Android.Runtime;
using Android.Views;
using EvilGenius.MvxTabbedNavigation.Demo.Core.ViewModels;
using EvilGenius.MvxTabbedNavigation.Platforms.Android.Presenters.Attributes;
using EvilGenius.MvxTabbedNavigation.Platforms.Android.Views;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Views.Fragments;
using View = Android.Views.View;
using EvilGenius.MvxTabbedNavigation.Demo.Platforms.Android.Activities;
#if SINGLE_PRJ
using AndroidResource = EvilGenius.MvxTabbedNavigation.Demo.Resource;
#endif
#if ANDROID_PRJ
using AndroidResource = EvilGenius.MvxTabbedNavigation.DemoMvx8.Android.Resource;
#endif

namespace EvilGenius.MvxTabbedNavigation.Demo.Platforms.Android.Fragments
{
    [Register("org.evilgenius.tabbednavigation.fragments.HomeFragment")]
    [RootFragmentPresentation(HostActivityType = typeof(MainActivity))]
    public class HomeFragment : BottomNavigationFragment<HomeViewModel>
    {
        public override int ContainerId => AndroidResource.Id.containerTabs;

        public override int BottomNavigationViewId => AndroidResource.Id.navigation;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            //base.OnCreateView(inflater, container, savedInstanceState);
            this.EnsureBindingContextIsSet();
            var view = this.BindingInflate(AndroidResource.Layout.fragment_home, null);

            return view;
        }
    }
}