using EvilGenius.MvxTabbedNavigation.Platforms.Android.Presenters.Attributes;
using MvvmCross.Base;

namespace EvilGenius.MvxTabbedNavigation.Platforms.Android.Views;

public interface ITabbedFragment : IFragmentHost
{
    void AddTab(TabPresentationAttribute tabPresentationAttribute);

    event EventHandler<MvxValueEventArgs<int>> TabSelected;

    void SelectTabAt(int index);

    void RemoveTab(string tabId);

    //void UpdateCurrentTabTitle(FragNavTabPresentationAttribute tabPresentationAttribute, int tabIndex);
}