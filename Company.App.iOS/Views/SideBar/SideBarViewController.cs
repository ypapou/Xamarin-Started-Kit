﻿using System;
using Company.App.Presentation.ViewModels.SideBar;
using FlexiMvvm.ViewModels;
using FlexiMvvm.Views;
using SidebarNavigation;
using UIKit;

namespace Company.App.Ios.Views.SideBar
{
    public class SideBarViewController : BindableViewController<SideBarViewModel>
    {
        [Weak]
        private static SideBarViewController _sideBarViewController;
        private UINavigationController _contentNavigationController;
        private SidebarController _sidebarController;

        public SideBarViewController()
        {
            _sideBarViewController = this;
        }

        public static SideBarViewController Current => _sideBarViewController;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _contentNavigationController = new UINavigationController();
            _sidebarController = new CustomSidebarController(this, _contentNavigationController, new SideBarMenuViewController(this))
            {
                MenuLocation = MenuLocations.Left
            };

            ViewModel.CloseMenuInteraction.RequestedWeakSubscribe(CloseMenuInteraction_Requested);
        }

        public void SetRootContent(ViewController viewController, SideBarMenuItem item)
        {
            _contentNavigationController.PopToRootViewController(false);
            _contentNavigationController.PushViewController(viewController, false);

            ViewModel.Selectedtem = item;
            CloseMenu();
        }

        public void CloseMenu()
        {
            _sidebarController.CloseMenu();
        }

        public void ToggleMenu()
        {
            _sidebarController.ToggleMenu();
        }

        private void CloseMenuInteraction_Requested(object sender, EventArgs e)
        {
            CloseMenu();
        }

        private class CustomSidebarController : SidebarController
        {
            public CustomSidebarController(
                UIViewController rootViewController,
                UIViewController contentViewController,
                UIViewController menuViewController)
                : base(rootViewController, contentViewController, menuViewController)
            {
            }

            protected override void AddMenuViewToSidebar()
            {
                base.AddMenuViewToSidebar();

                // SidebarController (v.2.0.0) doesn't add MenuAreaController as a child view controller.
                // It leads to several issues (due to MenuAreaController's life cycle methods are not called): safe area is not set at all.
                // The line below fixes such issues.
                AddChildViewController(MenuAreaController);
            }
        }
    }
}
