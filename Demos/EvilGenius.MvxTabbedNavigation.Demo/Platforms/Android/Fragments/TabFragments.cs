using Android.Runtime;
using EvilGenius.MvxTabbedNavigation.Demo.Core.Model;
using EvilGenius.MvxTabbedNavigation.Demo.Core.ViewModels;
using EvilGenius.MvxTabbedNavigation.Platforms.Android.Presenters.Attributes;
using EvilGenius.MvxTabbedNavigation.Presenters.Attributes;
using CoreResource = EvilGenius.MvxTabbedNavigation.Demo.Core.Resource;
#if SINGLE_PRJ
using AndroidResource = EvilGenius.MvxTabbedNavigation.Demo.Resource;
#endif
#if ANDROID_PRJ
using AndroidResource = EvilGenius.MvxTabbedNavigation.DemoMvx8.Android.Resource;
#endif

namespace EvilGenius.MvxTabbedNavigation.Demo.Platforms.Android.Fragments
{
    [TabPresentation(IconResourceId = AndroidResource.Drawable.ic_one, TabId = TabNames.TabOne, TabTitle = CoreResource.OneTab)]
    [Register("org.evilgenius.tabbednavigation.fragments.Tab1Fragment")]
    internal class Tab1Fragment : BaseFragment<Tab1ViewModel>  { }

    [TabPresentation(IconResourceId = AndroidResource.Drawable.ic_two, TabId = TabNames.TabTwo, TabTitle = CoreResource.TwoTab)]
    [Register("org.evilgenius.tabbednavigation.fragments.Tab2Fragment")]
    internal class Tab2Fragment : BaseFragment<Tab2ViewModel> { }

    [TabPresentation(IconResourceId = AndroidResource.Drawable.ic_three, TabId = TabNames.TabThree, TabTitle = CoreResource.ThreeTab)]
    [Register("org.evilgenius.tabbednavigation.fragments.Tab3Fragment")]
    internal class Tab3Fragment : BaseFragment<Tab3ViewModel> { }

    [Register("org.evilgenius.tabbednavigation.fragments.NewScreenFragment")]
    internal class NewScreenFragment : BaseFragment<NewScreenViewModel> { }

    [OverTopPresentation]
    [Register("org.evilgenius.tabbednavigation.fragments.OverTopFragment")]
    internal class OverTopFragment : BaseFragment<OverTopViewModel> { }
}