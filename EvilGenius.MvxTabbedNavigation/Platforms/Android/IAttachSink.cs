using FragmentX = AndroidX.Fragment.App.Fragment;

namespace EvilGenius.MvxTabbedNavigation.Platforms.Android
{
    public interface IAttachSink
    {
        void OnAttachOrDetach(FragmentX fragment, bool isAttached);
    }
}