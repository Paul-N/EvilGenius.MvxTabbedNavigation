using Android.App;
using Android.OS;
using MvvmCross.Platforms.Android.Views;
using FragmentManagerX = AndroidX.Fragment.App.FragmentManager;
using EvilGenius.MvxTabbedNavigation.Platforms.Android.Views;
using System;
#if SINGLE_PRJ
using AndroidResource = EvilGenius.MvxTabbedNavigation.Demo.Resource;
#endif
#if ANDROID_PRJ
using AndroidResource = EvilGenius.MvxTabbedNavigation.DemoMvx8.Android.Resource;
#endif

namespace EvilGenius.MvxTabbedNavigation.Demo.Platforms.Android.Activities
{
    [Activity(Theme = "@style/AppTheme.NoActionBar")]
    public class MainActivity : MvxActivity, ISingleHostActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(AndroidResource.Layout.activity_main);
        }

        #region IHostViewContainer impl
        public int ContainerId => AndroidResource.Id.containerMaster;

        FragmentManagerX IFragmentHost.FragmentManager => this.SupportFragmentManager;

        public event EventHandler<BackPressedRequestedEventArgs> OnBackRequested;

        #endregion

        public override void OnBackPressed()
        {
            var ea = new BackPressedRequestedEventArgs { Result = BackPressedHadlingResult.NotHandled };
            
            OnBackRequested?.Invoke(this, ea);

            switch (ea.Result)
            {
                case BackPressedHadlingResult.NotHandled:
                    base.OnBackPressed();
                    break;
                case BackPressedHadlingResult.Handled:
                    //Do nothing
                    break;
                case BackPressedHadlingResult.FinishActivity:
                    Finish();
                    break;
                default:
                    base.OnBackPressed();
                    break;
            }
        }
    }
}

