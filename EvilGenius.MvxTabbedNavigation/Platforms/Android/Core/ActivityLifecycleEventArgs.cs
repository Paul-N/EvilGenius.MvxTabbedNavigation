﻿using Android.App;
using Android.OS;
using MvvmCross.Platforms.Android.Views;

namespace EvilGenius.MvxTabbedNavigation.Platforms.Android.Core
{
    public class ActivityLifecycleEventArgs : EventArgs
    {
        public Activity Activity { get; private set; }

        public MvxActivityState ActivityState { get; private set; }

        public Bundle? Bundle { get; private set; }

        public ActivityLifecycleEventArgs(Activity activity, MvxActivityState activityState, Bundle? bundle)
        {
            Activity = activity;
            ActivityState = activityState;
            Bundle = bundle;
        }
    }
}
