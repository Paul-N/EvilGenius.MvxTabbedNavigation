using MvvmCross.Platforms.Android.Views;

namespace EvilGenius.MvxTabbedNavigation.Platforms.Android.Views
{
    public static class FragmentExtensions
    {
        public static void AddEventListeners(this IMvxEventSourceFragment fragment)
        {
            if (fragment is IMvxFragmentView)
            {
                var _ = new AttachAwareFragmentAdapter(fragment);
            }
        }
    }
}
