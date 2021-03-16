using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NavigationDemo.Core.Services
{
    public interface ILogService
    {
        int Info(/*string? tag,*/ string msg);

        void PrintDebug<T>(string mainStr, Action<T, StringBuilder> sbWriter, params T[] bundles);
    }
}