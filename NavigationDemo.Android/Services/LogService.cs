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
using Log = global::Android.Util.Log;

namespace NavigationDemo.Core.Services
{
    public class LogService : ILogService
    {
        public int Info(/*string? tag,*/ string msg)
        {
            return Log.Info("[NAVIGATIONDEMO_LOG]", msg);
        }

        public void PrintDebug<T>(string mainStr, Action<T, StringBuilder> sbWriter, params T[] bundles)
        {
            var sb = new StringBuilder();
            sb.AppendLine(mainStr);
            if (bundles != null && bundles.Any())
            {
                foreach (var bundle in bundles)
                    sbWriter(bundle, sb);
            }
            sb.AppendLine("---");
            Info(sb.ToString());
        }
    }
}