using Android.App;
using Android.Runtime;
using EvilGenius.MvxTabbedNavigation.Demo.Core;
using MvvmCross.Platforms.Android.Views;
using System;

namespace EvilGenius.MvxTabbedNavigation.Demo.Platforms.Android
{
    [Application]
    public class Application : MvxAndroidApplication<Setup, App>
    {
        public Application() : base() { }

        public Application(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { }
    }
}