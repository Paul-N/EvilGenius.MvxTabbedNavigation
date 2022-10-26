using MvvmCross.Plugin.Messenger;

namespace EvilGenius.MvxTabbedNavigation.Demo.Core.Model
{
    internal sealed class LoggedInMessage : MvxMessage
    {
        public LoggedInMessage(object sender) : base(sender) { }
    }
}
