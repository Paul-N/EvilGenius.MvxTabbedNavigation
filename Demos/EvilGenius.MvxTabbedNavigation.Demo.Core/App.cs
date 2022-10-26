using EvilGenius.MvxTabbedNavigation.Demo.Core.ViewModels;
using MvvmCross.ViewModels;

namespace EvilGenius.MvxTabbedNavigation.Demo.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            RegisterAppStart<HomeViewModel>();
        }
    }
}
