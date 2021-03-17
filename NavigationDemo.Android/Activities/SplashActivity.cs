using Android.App;
using Android.Content.PM;
using MvvmCross.Platforms.Android.Views;

namespace NavigationDemo.Android.Activities
{
    [Activity(
        Label = "NavigationDemo"
        , MainLauncher = true
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : MvxSplashScreenActivity
    {
        public SplashActivity() : base(Resource.Layout.activity_splash) { }
    }
}