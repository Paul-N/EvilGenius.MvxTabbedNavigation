using FragmentX = AndroidX.Fragment.App.Fragment;
using MvvmCross.Base;

namespace EvilGenius.MvxTabbedNavigation.Platforms.Android
{
    public interface IAttachSource
    {
        event EventHandler<MvxValueEventArgs<(FragmentX, bool)>>? OnAttachedOrDetached;
    }
}