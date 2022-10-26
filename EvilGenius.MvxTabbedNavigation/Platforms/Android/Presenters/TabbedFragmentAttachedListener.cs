using MvvmCross.Base;
using EvilGenius.MvxTabbedNavigation.Platforms.Android.Views;
using AndroidX.Fragment.App;
using Fragment = AndroidX.Fragment.App.Fragment;
using FragmentManager = AndroidX.Fragment.App.FragmentManager;

namespace EvilGenius.MvxTabbedNavigation.Platforms.Android.Presenters
{
    internal class TabbedFragmentAttachedListener : Java.Lang.Object, IFragmentOnAttachListener
    {
        public event EventHandler<MvxValueEventArgs<ITabbedFragment>>? TabbedFragmentFragmentManagerReady;
        
        public void OnAttachFragment(FragmentManager fragmentManager, Fragment fragment)
        {
            if (fragment is ITabbedFragment tabbedFragment)
                TabbedFragmentFragmentManagerReady?.Raise(this, tabbedFragment);
        }
    }
}
