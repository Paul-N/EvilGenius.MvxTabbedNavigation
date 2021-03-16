using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views;
using NavigationDemo.Core.Services;
using NavigationDemo.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Log = global::Android.Util.Log;

namespace NavigationDemo.Android.Activities
{
    [MvxActivityPresentation]
    [Activity(Label = "View for HomeViewModel")]
    public class HomeActivity : MvxActivity<HomeViewModel>
    {
        private ILogService _logService;

        public HomeActivity() : base() => _logService = Mvx.IoCProvider.Resolve<ILogService>();

        protected HomeActivity(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) => _logService = Mvx.IoCProvider.Resolve<ILogService>();

        protected override void OnCreate(Bundle bundle)
        {
            _logService.PrintDebug<BaseBundle>($"-> {nameof(HomeActivity)}.{nameof(OnCreate)}(Bundle {nameof(bundle)})", WriteBundleToStringBuilder, bundle);
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_home);
            _logService.PrintDebug<BaseBundle>($"<- {nameof(HomeActivity)}.{nameof(OnCreate)}(Bundle {nameof(bundle)})", WriteBundleToStringBuilder, bundle);
        }

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            _logService.PrintDebug<BaseBundle>($"-> {nameof(HomeActivity)}.{nameof(OnCreate)}(Bundle {nameof(savedInstanceState)}, PersistableBundle {nameof(persistentState)})", WriteBundleToStringBuilder, savedInstanceState, persistentState);
            base.OnCreate(savedInstanceState, persistentState);
            SetContentView(Resource.Layout.activity_home);
            _logService.PrintDebug<BaseBundle>($"<- {nameof(HomeActivity)}.{nameof(OnCreate)}(Bundle {nameof(savedInstanceState)}, PersistableBundle {nameof(persistentState)})", WriteBundleToStringBuilder, savedInstanceState, persistentState);
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            _logService.PrintDebug<BaseBundle>($"-> {nameof(HomeActivity)}.{nameof(OnSaveInstanceState)}(Bundle {nameof(outState)})", WriteBundleToStringBuilder, outState);
            base.OnSaveInstanceState(outState);
            _logService.PrintDebug<BaseBundle>($"<- {nameof(HomeActivity)}.{nameof(OnSaveInstanceState)}(Bundle {nameof(outState)})", WriteBundleToStringBuilder, outState);
        }

        public override void OnSaveInstanceState(Bundle outState, PersistableBundle outPersistentState)
        {
            _logService.PrintDebug<BaseBundle>($"-> {nameof(HomeActivity)}.{nameof(OnSaveInstanceState)}(Bundle {nameof(outState)}, PersistableBundle {nameof(outPersistentState)})", WriteBundleToStringBuilder,  outState, outPersistentState);
            base.OnSaveInstanceState(outState, outPersistentState);
            _logService.PrintDebug<BaseBundle>($"<- {nameof(HomeActivity)}.{nameof(OnSaveInstanceState)}(Bundle {nameof(outState)}, PersistableBundle {nameof(outPersistentState)})", WriteBundleToStringBuilder, outState, outPersistentState);
        }

        private void WriteBundleToStringBuilder(BaseBundle bundle, StringBuilder sb)
        {
            if (bundle?.IsEmpty == false)
            {
                if (bundle.KeySet() is ICollection<string> keySet && keySet.Any())
                {
                    sb.AppendLine($"bundle contain:");
                    foreach (var key in keySet)
                        sb.AppendLine($"{key}: {bundle.Get(key)}");
                }
            }
        }
    }
}