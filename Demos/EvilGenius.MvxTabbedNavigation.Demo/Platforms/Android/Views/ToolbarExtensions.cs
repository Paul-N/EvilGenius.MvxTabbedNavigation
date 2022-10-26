using Android.Content.Res;
using AndroidX.Fragment.App;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;
using View = Android.Views.View;
#if SINGLE_PRJ
using AndroidResource = EvilGenius.MvxTabbedNavigation.Demo.Resource;
#endif
#if ANDROID_PRJ
using AndroidResource = EvilGenius.MvxTabbedNavigation.DemoMvx8.Android.Resource;
#endif

namespace EvilGenius.MvxTabbedNavigation.Demo.Platforms.Android.Views
{
    internal static class ToolbarExtensions
    {
        public static void SetToolbarBackButton(this Fragment fragment, View view)
        {
            if (fragment.ParentFragmentManager.BackStackEntryCount > 1)
            {
                var toolbar = view.FindViewById<Toolbar>(AndroidResource.Id.toolbar);
                TypedArray a = fragment.Activity.Theme.ObtainStyledAttributes(AndroidResource.Style.AppTheme, new int[] { AndroidResource.Attribute.homeAsUpIndicator });
                int attributeResourceId = a.GetResourceId(0, 0);
                a.Recycle();

                toolbar?.SetNavigationIcon(attributeResourceId);

                toolbar?.SetNavigationOnClickListener(new ToolbarBackPressHandler(fragment.ParentFragmentManager));

            }
        }
    }
}
