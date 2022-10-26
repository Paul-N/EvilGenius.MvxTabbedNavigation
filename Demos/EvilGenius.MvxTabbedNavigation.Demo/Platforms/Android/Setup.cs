using Android.App;
using EvilGenius.MvxTabbedNavigation.Demo.Core;
using EvilGenius.MvxTabbedNavigation.Platforms.Android.Core;
using EvilGenius.MvxTabbedNavigation.Platforms.Android.Presenters;
using Microsoft.Extensions.Logging;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Platforms.Android.Core;
using MvvmCross.Platforms.Android.Presenters;
using MvvmCross.Platforms.Android.Views;
using MvvmCross.Plugin.Color;
using MvvmCross.Plugin.Messenger;
using Serilog;
using Serilog.Extensions.Logging;
using System.Reflection;
using Log = Serilog.Log;

namespace EvilGenius.MvxTabbedNavigation.Demo.Platforms.Android
{
    public class Setup : MvxAndroidSetup<App>
    {
        protected override ILoggerFactory CreateLogFactory()
        {
            // serilog configuration
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Debug()
                .CreateLogger();

            return new SerilogLoggerFactory();
        }

        protected override ILoggerProvider CreateLogProvider() => new SerilogLoggerProvider();

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            var listener = Mvx.IoCProvider.Resolve<IActivityLifecycleListener>();
            return new TabViewPresenter(AndroidViewAssemblies, listener);
        }

#if SINGLE_PRJ //The plugin asseblies are skipped in net6.0. Why?
        public override IEnumerable<Assembly> GetPluginAssemblies() 
            => base.GetPluginAssemblies().MergeWith(typeof(IMvxMessenger), typeof(MvxNativeColorValueConverter));
#endif

        protected override void InitializeFirstChance(IMvxIoCProvider iocProvider)
        {
            var activityLifecycleListener = new ActivityLifecycleListener();
            MvxAndroidApplication.Instance.RegisterActivityLifecycleCallbacks(activityLifecycleListener);

            iocProvider.RegisterSingleton<IActivityLifecycleListener>(activityLifecycleListener);

            base.InitializeFirstChance(iocProvider);
        }
    }
}