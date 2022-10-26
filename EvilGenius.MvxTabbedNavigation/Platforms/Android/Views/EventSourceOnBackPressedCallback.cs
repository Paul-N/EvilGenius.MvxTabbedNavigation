using AndroidX.Activity;

namespace EvilGenius.MvxTabbedNavigation.Platforms.Android.Views
{
    internal class EventSourceOnBackPressedCallback : OnBackPressedCallback
    {
        public event EventHandler OnBackPressed = null!;

        public EventSourceOnBackPressedCallback(bool enabled) : base(enabled) { }

        public override void HandleOnBackPressed() => OnBackPressed?.Invoke(this, EventArgs.Empty);
    }
}
