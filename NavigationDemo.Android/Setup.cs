using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.IoC;
using MvvmCross.Navigation;
using MvvmCross.Platforms.Android.Core;
using NavigationDemo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilGenius.Mvx.TabNavigation.Core.ViewModels;
using MvvmCross.Platforms.Android.Presenters;
using EvilGenius.Mvx.TabNavigation.Android.Presenters;
using EvilGenius.Mvx.TabNavigation.Core.Navigation;

namespace NavigationDemo.Android
{
    public class Setup : MvxAndroidSetup<App>
    {
        protected override IMvxNavigationService CreateNavigationService()
        {
            return base.CreateNavigationService();
        }

        protected override void RegisterDefaultSetupDependencies(IMvxIoCProvider iocProvider)
        {
            base.RegisterDefaultSetupDependencies(iocProvider);
            //RegisterLogProvider(iocProvider);
            //iocProvider.LazyConstructAndRegisterSingleton<IMvxSettings, MvxSettings>();
            //iocProvider.LazyConstructAndRegisterSingleton<IMvxStringToTypeParser, MvxStringToTypeParser>();
            //iocProvider.RegisterSingleton<IMvxPluginManager>(() => new MvxPluginManager(GetPluginConfiguration));
            //iocProvider.RegisterSingleton(CreateApp);
            //iocProvider.LazyConstructAndRegisterSingleton<IMvxViewModelLoader, MvxViewModelLoader>();
            
            iocProvider.LazyConstructAndRegisterSingleton<IMvxNavigationService>(() => new NavigationService(null, new NullProviderViewModelLoader()));
            
            //iocProvider.RegisterSingleton(() => new MvxViewModelByNameLookup());
            //iocProvider.LazyConstructAndRegisterSingleton<IMvxViewModelByNameLookup, MvxViewModelByNameLookup>(nameLookup => nameLookup);
            //iocProvider.LazyConstructAndRegisterSingleton<IMvxViewModelByNameRegistry, MvxViewModelByNameLookup>(nameLookup => nameLookup);
            //iocProvider.LazyConstructAndRegisterSingleton<IMvxViewModelTypeFinder, MvxViewModelViewTypeFinder>();
            //iocProvider.LazyConstructAndRegisterSingleton<IMvxTypeToTypeLookupBuilder, MvxViewModelViewLookupBuilder>();
            //iocProvider.LazyConstructAndRegisterSingleton<IMvxCommandCollectionBuilder, MvxCommandCollectionBuilder>();
            //iocProvider.LazyConstructAndRegisterSingleton<IMvxNavigationSerializer, MvxStringDictionaryNavigationSerializer>();
            //iocProvider.LazyConstructAndRegisterSingleton<IMvxChildViewModelCache, MvxChildViewModelCache>();

            //iocProvider.RegisterType<IMvxCommandHelper, MvxWeakCommandHelper>();
        }

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            return new Presenter(AndroidViewAssemblies);
        }
    }
}