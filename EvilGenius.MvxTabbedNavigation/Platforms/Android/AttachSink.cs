using MvvmCross.Base;
using FragmentX = AndroidX.Fragment.App.Fragment;

namespace EvilGenius.MvxTabbedNavigation.Platforms.Android
{
    public class AttachSink : IAttachSink, IAttachSource
    {
        public void OnAttachOrDetach(FragmentX fragment, bool isAttached) 
            => OnAttachedOrDetached?.Invoke(this, new MvxValueEventArgs<(FragmentX, bool)>((fragment, isAttached)));

        public event EventHandler<MvxValueEventArgs<(FragmentX, bool)>>? OnAttachedOrDetached;
    }
}
