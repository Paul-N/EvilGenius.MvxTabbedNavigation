﻿using EvilGenius.MvxTabbedNavigation.Demo.Core.Services;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace EvilGenius.MvxTabbedNavigation.Demo.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.IoCProvider.ConstructAndRegisterSingleton<ICurrentUserService, CurrentUserService>();
            RegisterCustomAppStart<AppStart>();
        }
    }
}
