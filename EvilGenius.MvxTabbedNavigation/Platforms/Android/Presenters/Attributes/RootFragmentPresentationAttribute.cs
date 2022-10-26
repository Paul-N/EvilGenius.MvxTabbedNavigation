using MvvmCross.Presenters.Attributes;

namespace EvilGenius.MvxTabbedNavigation.Platforms.Android.Presenters.Attributes
{
    public class RootFragmentPresentationAttribute : MvxBasePresentationAttribute
    {
        public string? Tag { get; set; }

        public Type HostActivityType { get; set; } = null!;
    }
}
