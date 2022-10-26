using Android.Content.Res;
using Android.Util;
using Android.Widget;
using System;
using View = Android.Views.View;
using CoreResource = EvilGenius.MvxTabbedNavigation.Demo.Core.Resource;
using Button = Android.Widget.Button;

namespace EvilGenius.MvxTabbedNavigation.Demo.Platforms.Android
{
    internal static class ViewEx
    {
        public static void SetSizeOf(this View view, int viewIdToSize, Resources res, int dpWidth, int dpHeight)
        {
            var v = view.FindViewById(viewIdToSize);
            v.LayoutParameters.Width = Convert.ToInt32(TypedValue.ApplyDimension(ComplexUnitType.Dip, dpWidth, res.DisplayMetrics));
            v.LayoutParameters.Height = Convert.ToInt32(TypedValue.ApplyDimension(ComplexUnitType.Dip, dpHeight, res.DisplayMetrics));
        }

        public static void SetTextTo(this View view, int textViewId, string title) 
            => view.FindViewById<TextView>(textViewId).Text = title;

        public static void SetupTitledTextView(this View view, int viewId, Resources res, string title)
        {
            var tv = view.FindViewById<TextView>(viewId);
            tv.Text = title;
            tv.Set200x44Size(res);
        }

        public static void SetupTextField(this View view, int viewId, Resources res)
        {
            var tv = view.FindViewById<EditText>(viewId);
            tv.Set200x44Size(res);
        }

        private static void Set200x44Size(this View v, Resources res)
        {
            v.LayoutParameters.Width = Convert.ToInt32(TypedValue.ApplyDimension(ComplexUnitType.Dip, CoreResource.TitleWidth, res.DisplayMetrics));
            v.LayoutParameters.Height = Convert.ToInt32(TypedValue.ApplyDimension(ComplexUnitType.Dip, CoreResource._44px, res.DisplayMetrics));
        }
    }
}