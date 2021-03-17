using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Lifecycle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilGenius.Mvx.TabNavigation.Android.AndroidViewModels
{
    public class ViewModelHolder : ViewModel
    {
        protected ViewModelHolder() : base() { }

        public ViewModelHolder(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer){ }
    }
}