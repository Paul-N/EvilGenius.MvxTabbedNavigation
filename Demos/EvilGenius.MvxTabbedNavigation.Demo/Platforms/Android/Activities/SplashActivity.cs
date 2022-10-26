using Android.App;
using Android.Content.PM;
using MvvmCross.Platforms.Android.Views;
#if SINGLE_PRJ
using AndroidResource = EvilGenius.MvxTabbedNavigation.Demo.Resource;
#endif
#if ANDROID_PRJ
using AndroidResource = EvilGenius.MvxTabbedNavigation.DemoMvx8.Android.Resource;
#endif

namespace EvilGenius.MvxTabbedNavigation.Demo.Platforms.Android.Activities
{
    [Activity(
        Label = "TabbedNavigation demo"
        , MainLauncher = true
        , Theme = "@style/AppTheme.Launcher"
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait)]
    internal sealed class SplashActivity : MvxSplashScreenActivity
    {
        public SplashActivity() : base(AndroidResource.Layout.activity_splash) { }
    }
}
