using EvilGenius.MvxTabbedNavigation.Presenters.Attributes;
using EvilGenius.MvxTabbedNavigation.Presenters.Hints;
using MvvmCross.Platforms.Ios.Presenters;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.Presenters.Hints;
using MvvmCross.ViewModels;
using UIKit;

namespace EvilGenius.MvxTabbedNavigation.Platforms.iOS.Presenters
{
    public class TabbedViewPresenter : MvxIosViewPresenter
    {
        public TabbedViewPresenter(IUIApplicationDelegate applicationDelegate, UIWindow window) : base(applicationDelegate, window)
        {

        }

        public override void RegisterAttributeTypes()
        {
            base.RegisterAttributeTypes();

            AttributeTypesToActionsDictionary.Register<OverTopPresentationAttribute>(
                (viewType, attribute, request) =>
                {
                    var viewController = (UIViewController)this.CreateViewControllerFor(request);
                    return ShowOverTopViewController(viewController, attribute, request);
                },
                CloseOverTopViewController);
        }

        public override Task<bool> ChangePresentation(MvxPresentationHint hint)
        {
            return hint switch
            {
                ClearStackPresentationHint pagePresentationHint when ChangePagePresentation(pagePresentationHint) =>
                    Task.FromResult(true),
                MvxPopToRootPresentationHint popToRootPresentationHint when ChangePagePresentation(popToRootPresentationHint) =>
                    Task.FromResult(true),
                _ => base.ChangePresentation(hint)
            };
        }

        private bool ChangePagePresentation(ClearStackPresentationHint pagePresentationHint)
        {
            if (MasterNavigationController?.TopViewController is UITabBarController tabBarController
                && tabBarController.SelectedViewController is UINavigationController currentNavController)
            {
                currentNavController.ViewControllers = new UIViewController[0];
                return true;
            }
            else if (MasterNavigationController?.TopViewController is UINavigationController navController)
            {
                navController.ViewControllers = new UIViewController[0];
                return true;
            }

            return false;
        }

        private bool ChangePagePresentation(MvxPopToRootPresentationHint popToRootPresentationHint)
        {
            if (MasterNavigationController?.TopViewController is UITabBarController tabBarController
                && tabBarController.SelectedViewController is UINavigationController currentNavController)
            {
                currentNavController.PopToRootViewController(popToRootPresentationHint.Animated);
                return true;
            }
            else if (MasterNavigationController != null)
            {
                MasterNavigationController.PopToRootViewController(popToRootPresentationHint.Animated);
                return true;
            }

            return false;
        }

        protected virtual Task<bool> ShowOverTopViewController(
            UIViewController viewController,
            OverTopPresentationAttribute attribute,
            MvxViewModelRequest request)
        {
            ValidateArguments(viewController, attribute);

            if (MasterNavigationController != null)
            {
                PushViewControllerIntoStack(MasterNavigationController, viewController, ConvertToChildPresentationAttribute(attribute));
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        protected virtual Task<bool> CloseOverTopViewController(IMvxViewModel viewModel, OverTopPresentationAttribute attribute)
        {
            ValidateArguments(viewModel, attribute);

            // if the current root is a NavigationController, close it in the stack
            if (MasterNavigationController != null && TryCloseViewControllerInsideStack(MasterNavigationController, viewModel, ConvertToChildPresentationAttribute(attribute)))
                return Task.FromResult(true);

            return Task.FromResult(false);
        }

        private MvxChildPresentationAttribute ConvertToChildPresentationAttribute(OverTopPresentationAttribute attribute)
            => new MvxChildPresentationAttribute { ViewModelType = attribute.ViewModelType, ViewType = attribute.ViewType };

        private static void ValidateArguments(UIViewController viewController, MvxBasePresentationAttribute attribute)
        {
            if (viewController == null)
                throw new ArgumentNullException(nameof(viewController));

            if (attribute == null)
                throw new ArgumentNullException(nameof(attribute));
        }

        private static void ValidateArguments(IMvxViewModel viewModel, MvxBasePresentationAttribute attribute)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            if (attribute == null)
                throw new ArgumentNullException(nameof(attribute));
        }

        protected override Task<bool> ShowChildViewController(UIViewController viewController, MvxChildPresentationAttribute attribute, MvxViewModelRequest request)
        {
            if (MasterNavigationController?.ViewControllers?.Length >= 2)
            {
                PushViewControllerIntoStack(MasterNavigationController, viewController, attribute);
                return Task.FromResult(true);
            }

            return base.ShowChildViewController(viewController, attribute, request);
        }
    }
}
