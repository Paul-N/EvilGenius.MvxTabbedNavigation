using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using NavigationDemo.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NavigationDemo.Core.ViewModels
{
    public class HomeViewModel : MvxViewModel
    {
        private readonly ILogService _logService;
        public IMvxCommand ResetTextCommand => new MvxCommand(ResetText);

        public HomeViewModel(ILogService logService)
        {
            _logService = logService;
            PrintDebug($"-> {nameof(HomeViewModel)}.{nameof(HomeViewModel)}");
        }

        private void ResetText()
        {
            Text = "Hello MvvmCross";
        }

        private string _text = "Hello MvvmCross";

        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }

        protected override void ReloadFromBundle(IMvxBundle state)
        {
            _logService.PrintDebug($"-> {nameof(HomeViewModel)}.{nameof(ReloadFromBundle)}(IMvxBundle {nameof(state)})", WriteBundleToStringBuilder, state);
            base.ReloadFromBundle(state);
            _logService.PrintDebug($"<- {nameof(HomeViewModel)}.{nameof(ReloadFromBundle)}(IMvxBundle {nameof(state)})", WriteBundleToStringBuilder, state);
        }

        protected override void SaveStateToBundle(IMvxBundle bundle)
        {
            _logService.PrintDebug($"-> {nameof(HomeViewModel)}.{nameof(SaveStateToBundle)}(IMvxBundle {nameof(bundle)})", WriteBundleToStringBuilder, bundle);
            base.SaveStateToBundle(bundle);
            _logService.PrintDebug($"<- {nameof(HomeViewModel)}.{nameof(SaveStateToBundle)}(IMvxBundle {nameof(bundle)})", WriteBundleToStringBuilder, bundle);
        }

        private void PrintDebug(string mainStr)
        {
            var sb = new StringBuilder();
            sb.AppendLine(mainStr);
            sb.AppendLine("---");
            _logService.Info(sb.ToString());
        }

        private void WriteBundleToStringBuilder(IMvxBundle bundle, StringBuilder sb)
        {
            if (bundle?.Data?.Any() == true)
            {
                sb.AppendLine($"bundle contain:");
                foreach (var kvp in bundle.Data)
                    sb.AppendLine($"{kvp.Key}: {kvp.Value}");
            }
        }
    }
}