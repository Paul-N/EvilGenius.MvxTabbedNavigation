﻿using Android.App;
using Android.Content.PM;
using MvvmCross.Platforms.Android.Views;
using AndroidResource = EvilGenius.MvxTabbedNavigation.Demo.Resource;

// ReSharper disable once CheckNamespace
namespace EvilGenius.MvxTabbedNavigation.Demo.Platforms.Android.Activities;

[Activity(
    Label = "TabbedNavigation demo"
    , MainLauncher = true
    , Theme = "@style/AppTheme.Launcher"
    , NoHistory = true
    , ScreenOrientation = ScreenOrientation.Portrait)]
internal sealed class SplashActivity : MvxStartActivity
{
    // ReSharper disable once AccessToStaticMemberViaDerivedType
    // ReSharper disable once ConvertToPrimaryConstructor
    public SplashActivity() : base(AndroidResource.Layout.activity_splash) { }
}