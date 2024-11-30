﻿using Android.OS;
using Android.Views;
using EvilGenius.MvxTabbedNavigation.Demo.Core.ViewModels;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Views.Fragments;
using CoreResource = EvilGenius.MvxTabbedNavigation.Demo.Core.Resource;
using View = Android.Views.View;
using EvilGenius.MvxTabbedNavigation.Platforms.Android.Views;
using EvilGenius.MvxTabbedNavigation.Demo.Platforms.Android.Views;
using AndroidResource = EvilGenius.MvxTabbedNavigation.Demo.Resource;
// ReSharper disable AccessToStaticMemberViaDerivedType

// ReSharper disable once CheckNamespace
namespace EvilGenius.MvxTabbedNavigation.Demo.Platforms.Android.Fragments;

internal class BaseFragment<TViewModel> : Fragment<TViewModel> where TViewModel : BaseViewModel
{
    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
    {
        this.EnsureBindingContextIsSet();

        var view = this.BindingInflate(AndroidResource.Layout.fragment_base, null);

        view.SetSizeOf(AndroidResource.Id.btnMinus, Resources, CoreResource._44px, CoreResource._44px);
        view.SetSizeOf(AndroidResource.Id.btnPlus, Resources, CoreResource._44px, CoreResource._44px);


        view.SetTextTo(AndroidResource.Id.btnNew, CoreResource.OpenNew);
        view.SetTextTo(AndroidResource.Id.btnOverTop, CoreResource.OpenOverTop);
        view.SetTextTo(AndroidResource.Id.btnPopToRoot, CoreResource.PopToRoot);
        view.SetTextTo(AndroidResource.Id.btnCloseSelf, CoreResource.Close);

        this.SetToolbarBackButton(view);
            
        return view;
    }
}