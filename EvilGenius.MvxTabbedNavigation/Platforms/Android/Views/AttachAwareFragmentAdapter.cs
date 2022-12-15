using Android.Content;
using MvvmCross;
using MvvmCross.Base;
using MvvmCross.Platforms.Android.Views;
using MvvmCross.Platforms.Android.Views.Fragments;
using FragmentX = AndroidX.Fragment.App.Fragment;

namespace EvilGenius.MvxTabbedNavigation.Platforms.Android.Views
{
    internal class AttachAwareFragmentAdapter : MvxBindingFragmentAdapter
    {
        public AttachAwareFragmentAdapter(IMvxEventSourceFragment eventSource) : base(eventSource) { }

        protected override void HandleAttachCalled(object sender, MvxValueEventArgs<Context> e)
        {
            base.HandleAttachCalled(sender, e);
            if (Mvx.IoCProvider.TryResolve<IAttachSink>(out var sink) && sender is FragmentX fragment)
            {
                sink.OnAttachOrDetach(fragment, true);
            }
        }

        protected override void HandleDetachCalled(object sender, EventArgs e)
        {
            base.HandleDetachCalled(sender, e);
            if (Mvx.IoCProvider.TryResolve<IAttachSink>(out var sink) && sender is FragmentX fragment)
            {
                sink.OnAttachOrDetach(fragment, false);
            }
        }
    }
}
