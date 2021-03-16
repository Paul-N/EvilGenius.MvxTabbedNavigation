using MvvmCross.IoC;
using MvvmCross.ViewModels;
using NavigationDemo.Core.ViewModels;

namespace NavigationDemo.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
    {
        CreatableTypes()
            .EndingWith("Service")
            .AsInterfaces()
            .RegisterAsLazySingleton();
        RegisterAppStart<HomeViewModel>();
    }
}
}