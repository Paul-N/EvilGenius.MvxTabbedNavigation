using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Views;
using NavigationDemo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NavigationDemo.Android
{
    [Application]
    public class Application : MvxAndroidApplication<Setup, App>
    {
        public Application(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            //CrossCurrentActivity.Current.Init(this);
        }
    }
}