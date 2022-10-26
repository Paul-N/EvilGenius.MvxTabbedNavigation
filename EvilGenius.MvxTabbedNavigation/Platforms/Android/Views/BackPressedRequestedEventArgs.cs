using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilGenius.MvxTabbedNavigation.Platforms.Android.Views
{
    public class BackPressedRequestedEventArgs : EventArgs
    {
        public BackPressedHadlingResult Result { get; set; }
    }
}
