using MvvmCross.Plugin.Messenger;
// ReSharper disable ConvertToPrimaryConstructor

namespace EvilGenius.MvxTabbedNavigation.Demo.Core.Model;

internal sealed class LoggedInMessage : MvxMessage
{
    public LoggedInMessage(object sender) : base(sender) { }
}