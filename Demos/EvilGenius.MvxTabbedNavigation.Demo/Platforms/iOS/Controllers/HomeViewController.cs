using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace EvilGenius.MvxTabbedNavigation.Demo.Platforms.iOS.Controllers
{
    [MvxRootPresentation(WrapInNavigationController = true)]
    public class HomeViewController : MvxTabBarViewController
    {
        public HomeViewController() { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            TabBar.BackgroundColor = UIColor.White;
        }
    }
}