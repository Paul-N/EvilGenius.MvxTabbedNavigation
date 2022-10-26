using Android.Content;
using Android.OS;
using Android.Views;
using EvilGenius.MvxTabbedNavigation.Platforms.Android.Presenters.Attributes;
using Google.Android.Material.BottomNavigation;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Views;
using MvvmCross.Platforms.Android.Views.Fragments;
using MvvmCross.ViewModels;
using System.Runtime;
using FragmentManager = AndroidX.Fragment.App.FragmentManager;

namespace EvilGenius.MvxTabbedNavigation.Platforms.Android.Views
{
    public abstract class BottomNavigationFragment : MvxFragment, ITabbedFragment
    {
        private BottomNavigationView _bottomNavigationView;

        private int _selectedTabIndex;

        private const string _tabsCountKey = "__tabsCount";

        private const string _selectedTabIndexKey = "__selectedTabIndex";

        private IList<TabPresentationAttribute> _tabPresentationAttributes = new List<TabPresentationAttribute>();

        public abstract int ContainerId { get; }

        public abstract int BottomNavigationViewId { get; }

        FragmentManager IFragmentHost.FragmentManager => ChildFragmentManager;


        public event EventHandler<MvxValueEventArgs<int>> TabSelected;

        public void AddTab(TabPresentationAttribute tabPresentationAttribute)
        {
            var navView = GetBottomNavView();
            IMenuItem menuItem;
            int itemId = View.GenerateViewId();

            if (!string.IsNullOrEmpty(tabPresentationAttribute.TabTitle))
                menuItem = navView.Menu.Add(IMenu.None, itemId, IMenu.None, tabPresentationAttribute.TabTitle);
            else if (tabPresentationAttribute.TabTitleResourceId != IMenu.None)
                menuItem = navView.Menu.Add(IMenu.None, itemId, IMenu.None, tabPresentationAttribute.TabTitleResourceId);
            else
                menuItem = navView.Menu.Add(IMenu.None, itemId, IMenu.None, string.Empty);

            if (!string.IsNullOrEmpty(tabPresentationAttribute.TabTitleCondensed))
                menuItem.SetTitleCondensed(tabPresentationAttribute.TabTitleCondensed);
            else if (tabPresentationAttribute.TabTitleCondensedResourceId != IMenu.None)
                menuItem.SetTitleCondensed(Context.Resources.GetString(tabPresentationAttribute.TabTitleCondensedResourceId));

            if (tabPresentationAttribute.IconResourceId != IMenu.None)
                menuItem.SetIcon(tabPresentationAttribute.IconResourceId);

            _tabPresentationAttributes.Add(tabPresentationAttribute);
        }

        public void SelectTabAt(int index)
        {
            var navView = GetBottomNavView();
            var item = navView.Menu.GetItem(index);
            if(item != null)
                navView.SelectedItemId = item!.ItemId;
            
            _selectedTabIndex = index;
        }

        public void RemoveTab(string tabId)
        {
            var attrToRemove = _tabPresentationAttributes.FirstOrDefault(a => a.TabId == tabId);

            if (attrToRemove != null)
            {
                var navView = GetBottomNavView();

                var index = _tabPresentationAttributes.IndexOf(attrToRemove);

                if (navView?.Menu?.GetItem(index) is IMenuItem menuItem)
                    navView?.Menu?.RemoveItem(menuItem.ItemId);
                
                _tabPresentationAttributes.Remove(attrToRemove);
            }
        }

        private BottomNavigationView GetBottomNavView()
        {
            if (_bottomNavigationView != null)
                return _bottomNavigationView;
            else
            {
                _bottomNavigationView = View?.FindViewById<BottomNavigationView>(BottomNavigationViewId);
                if (_bottomNavigationView != null)
                {
                    //_bottomNavigationView.ItemIconTintList = null;
                    _bottomNavigationView.ItemSelected += OnBottomBarNavigationItemSelected;
                }
                else throw new Exception($"Unable to find navigation view. Check {nameof(BottomNavigationViewId)}");

                return _bottomNavigationView;
            }
        }

        private void OnBottomBarNavigationItemSelected(object sender, BottomNavigationView.ItemSelectedEventArgs ea)
        {
            var navView = GetBottomNavView();

            for (int i = 0; i < navView.Menu.Size(); i++)
                if (navView.Menu.GetItem(i) == ea.P0)
                {
                    _selectedTabIndex = i;
                    TabSelected?.Invoke(sender, new MvxValueEventArgs<int>(i));
                }
        }

        public override void OnViewCreated(View view, Bundle? savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            if (savedInstanceState?.ContainsKey(_tabsCountKey) == true)
            {
                var tabsCount = savedInstanceState.GetInt(_tabsCountKey);

                _selectedTabIndex = savedInstanceState.GetInt(_selectedTabIndexKey);

                for (int i = 0; i < tabsCount; i++)
                {
                    var tabAttr = TabPresentationAttribute.ReadFromBundle(savedInstanceState, i);
                    AddTab(tabAttr);
                }

                SelectTabAt(_selectedTabIndex);
            }
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);

            outState.PutInt(_tabsCountKey, _tabPresentationAttributes.Count);

            outState.PutInt(_selectedTabIndexKey, _selectedTabIndex);

            int i = 0;
            foreach (var attr in _tabPresentationAttributes)
            {
                attr.WriteToBundle(outState, i);
                i++;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if(_bottomNavigationView != null)
                _bottomNavigationView.ItemSelected -= OnBottomBarNavigationItemSelected;
            
            _bottomNavigationView = null;
            
            base.Dispose(disposing);
        }
    }

    public abstract class BottomNavigationFragment<TViewModel> : BottomNavigationFragment, IMvxFragmentView<TViewModel>
        where TViewModel : class, IMvxViewModel
    {
        public new TViewModel ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public MvxFluentBindingDescriptionSet<IMvxFragmentView<TViewModel>, TViewModel> CreateBindingSet()
        {
            return this.CreateBindingSet<IMvxFragmentView<TViewModel>, TViewModel>();
        }
    }
}
