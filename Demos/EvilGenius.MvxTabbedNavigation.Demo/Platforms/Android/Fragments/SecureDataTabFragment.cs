using Android.Runtime;
using EvilGenius.MvxTabbedNavigation.Demo.Core.Model;
using EvilGenius.MvxTabbedNavigation.Demo.Core.ViewModels;
using EvilGenius.MvxTabbedNavigation.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views.Fragments;
using CoreResource = EvilGenius.MvxTabbedNavigation.Demo.Core.Resource;
using EvilGenius.MvxTabbedNavigation.Platforms.Android.Views;
using Android.OS;
using Android.Views;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using View = Android.Views.View;
using EvilGenius.MvxTabbedNavigation.Demo.Platforms.Android.Views;
#if SINGLE_PRJ
using AndroidResource = EvilGenius.MvxTabbedNavigation.Demo.Resource;
#endif
#if ANDROID_PRJ
using AndroidResource = EvilGenius.MvxTabbedNavigation.DemoMvx8.Android.Resource;
#endif

namespace EvilGenius.MvxTabbedNavigation.Demo.Platforms.Android.Fragments
{
    [TabPresentation(IconResourceId = AndroidResource.Drawable.ic_lock, TabId = TabNames.TabSecure, TabTitle = CoreResource.SecureTab)]
    [Register("org.evilgenius.tabbednavigation.fragments.SecureDataTabFragment")]
    internal sealed class SecureDataTabFragment : Fragment<SecureDataTabViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this.EnsureBindingContextIsSet();

            var view = this.BindingInflate(AndroidResource.Layout.fragment_secure_data, null);

            view.SetSizeOf(AndroidResource.Id.lblTitle, Resources, CoreResource.TitleWidth, CoreResource._44px);

            view.SetupTitledTextView(AndroidResource.Id.btnGoAuth, Resources, CoreResource.GoLogin);

            this.SetToolbarBackButton(view);

            return view;
        }
    }
}