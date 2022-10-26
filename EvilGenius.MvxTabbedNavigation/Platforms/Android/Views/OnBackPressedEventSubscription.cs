﻿using MvvmCross.WeakSubscription;

namespace EvilGenius.MvxTabbedNavigation.Platforms.Android.Views
{
    internal class OnBackPressedEventSubscription : MvxWeakEventSubscription<EventSourceOnBackPressedCallback>
    {
        private WeakReference<EventSourceOnBackPressedCallback> _sourceReference;

        public OnBackPressedEventSubscription(EventSourceOnBackPressedCallback source, string sourceEventName, EventHandler targetEventHandler)
            : base(source, sourceEventName, targetEventHandler) => _sourceReference = new WeakReference<EventSourceOnBackPressedCallback>(source);

        protected override void Dispose(bool disposing)
        {
            if (_sourceReference.TryGetTarget(out EventSourceOnBackPressedCallback source))
                source.Remove();

            base.Dispose(disposing);
        }
    }
}
