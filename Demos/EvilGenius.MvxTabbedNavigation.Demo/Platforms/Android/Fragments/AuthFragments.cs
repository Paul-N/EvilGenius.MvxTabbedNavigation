using Android.OS;
using Android.Runtime;
using Android.Views;
using EvilGenius.MvxTabbedNavigation.Demo.Core.Model;
using EvilGenius.MvxTabbedNavigation.Demo.Core.ViewModels;
using EvilGenius.MvxTabbedNavigation.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Views.Fragments;
using CoreResource = EvilGenius.MvxTabbedNavigation.Demo.Core.Resource;
using View = Android.Views.View;
using EvilGenius.MvxTabbedNavigation.Platforms.Android.Views;
using EvilGenius.MvxTabbedNavigation.Demo.Platforms.Android.Views;
#if SINGLE_PRJ
using AndroidResource = EvilGenius.MvxTabbedNavigation.Demo.Resource;
#endif
#if ANDROID_PRJ
using AndroidResource = EvilGenius.MvxTabbedNavigation.DemoMvx8.Android.Resource;
#endif

namespace EvilGenius.MvxTabbedNavigation.Demo.Platforms.Android.Fragments
{
    [TabPresentation(IconResourceId = AndroidResource.Drawable.ic_account, TabId = TabNames.TabLogin, TabTitle = CoreResource.Account)]
    [Register("org.evilgenius.tabbednavigation.fragments.PhoneFragment")]
    internal sealed class PhoneFragment : Fragment<PhoneViewModel> 
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this.EnsureBindingContextIsSet();
            
            var view = this.BindingInflate(AndroidResource.Layout.fragment_phone, null);

            view.SetupTitledTextView(AndroidResource.Id.lblTitle, this.Resources, CoreResource.EnterPhone);

            view.SetupTextField(AndroidResource.Id.phoneTxt, this.Resources);

            view.SetupTitledTextView(AndroidResource.Id.btnGo, this.Resources, CoreResource.GetCode);

            this.SetToolbarBackButton(view);

            return view;
        }
    }

    [Register("org.evilgenius.tabbednavigation.fragments.SmsCodeFragment")]
    internal sealed class SmsCodeFragment : Fragment<SmsCodeViewModel> 
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this.EnsureBindingContextIsSet();

            var view = this.BindingInflate(AndroidResource.Layout.fragment_sms_code, null);

            view.SetupTitledTextView(AndroidResource.Id.lblTitle, this.Resources, CoreResource.EnterSms);

            view.SetupTextField(AndroidResource.Id.smsTxt, this.Resources);

            view.SetupTitledTextView(AndroidResource.Id.btnGo, this.Resources, CoreResource.GetCode);

            this.SetToolbarBackButton(view);

            return view;
        }
    }

    [Register("org.evilgenius.tabbednavigation.fragments.AccountFragment")]
    internal sealed class AccountFragment : Fragment<AccountViewModel> 
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this.EnsureBindingContextIsSet();

            var view = this.BindingInflate(AndroidResource.Layout.fragment_account, null);

            view.SetSizeOf(AndroidResource.Id.lblAccount, this.Resources, CoreResource.TitleWidth, CoreResource._44px);

            this.SetToolbarBackButton(view);

            return view;
        }
    }
}