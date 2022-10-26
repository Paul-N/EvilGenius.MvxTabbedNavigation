using Android.OS;
using Android.Views;
using MvvmCross.Presenters.Attributes;

namespace EvilGenius.MvxTabbedNavigation.Platforms.Android.Presenters.Attributes
{
    public class TabPresentationAttribute : MvxBasePresentationAttribute
    {
        public string TabId { get; set; } = null!;

        public string? TabTitle { get; set; }

        public string? TabTitleCondensed { get; set; }

        public int TabTitleResourceId { get; set; }

        public int TabTitleCondensedResourceId { get; set; }

        public int IconResourceId { get; set; }

        public void WriteToBundle(Bundle bundle, int index)
        {
            WriteString(bundle, nameof(TabId), TabId, index);

            WriteString(bundle, nameof(TabTitle), TabTitle, index);

            WriteString(bundle, nameof(TabTitleCondensed), TabTitleCondensed, index);

            WriteInt(bundle, nameof(TabTitleResourceId), TabTitleResourceId, index);

            WriteInt(bundle, nameof(TabTitleCondensedResourceId), TabTitleCondensedResourceId, index);

            WriteInt(bundle, nameof(IconResourceId), IconResourceId, index);
        }

        private void WriteString(Bundle bundle, string name, string? value, int index)
        {
            if (value != null)
                bundle.PutString(GetKey(name, index), value);
        }

        private void WriteInt(Bundle bundle, string name, int value, int index)
        {
            if (value != IMenu.None)
                bundle.PutInt(GetKey(name, index), value);
        }

        public static TabPresentationAttribute ReadFromBundle(Bundle bundle, int index)
        {
            var tpAttr = new TabPresentationAttribute();

            tpAttr.TabId = ReadString(bundle, nameof(TabId), index)!;

            tpAttr.TabTitle = ReadString(bundle, nameof(TabTitle), index);

            tpAttr.TabTitleCondensed = ReadString(bundle, nameof(TabTitleCondensed), index);

            tpAttr.TabTitleResourceId = ReadInt(bundle, nameof(TabTitleResourceId), index);

            tpAttr.TabTitleCondensedResourceId = ReadInt(bundle, nameof(TabTitleCondensedResourceId), index);

            tpAttr.IconResourceId = ReadInt(bundle, nameof(IconResourceId), index);

            return tpAttr;
        }

        private static string? ReadString(Bundle bundle, string name, int index)
        {
            var key = GetKey(name, index);
            return bundle.ContainsKey(key) ? bundle.GetString(key) : null;
        }

        private static int ReadInt(Bundle bundle, string name, int index)
        {
            var key = GetKey(name, index);
            return bundle.ContainsKey(key) ? bundle.GetInt(key) : IMenu.None;
        }

        private static string GetKey(string key, int index) => $"__{nameof(TabPresentationAttribute)}__{key}__{index}";
    }
}
