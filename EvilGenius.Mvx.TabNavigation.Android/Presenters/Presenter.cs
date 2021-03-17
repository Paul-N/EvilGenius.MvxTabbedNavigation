using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Presenters;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EvilGenius.Mvx.TabNavigation.Android.Presenters
{
    public class Presenter : MvxAndroidViewPresenter
    {
        public Presenter(IEnumerable<Assembly> androidViewAssemblies) : base(androidViewAssemblies)
        {
        }

        protected override Task<bool> ShowActivity(Type view, MvxActivityPresentationAttribute attribute, MvxViewModelRequest request)
        {
            return base.ShowActivity(view, attribute, request);
        }
    }
}