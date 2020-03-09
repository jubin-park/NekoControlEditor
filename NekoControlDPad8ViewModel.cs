using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace NekoControlEditor
{
    class NekoControlDPad8ViewModel : NekoControlDPad4ViewModel
    {
        private static int mCount = 0;

        public NekoControlDPad8ViewModel()
        {
            Name = "$dpad8_" + (++mCount);
            BitmapDefault = "image/dpad_none.png";
            BitmapDown = "";
            BitmapLeft = "";
            BitmapRight = "";
            BitmapUp = "";
            BitmapStick = "image/dpad_stick.png";
            StickMovableRadius = 16;
        }
    }
}
