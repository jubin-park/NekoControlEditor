using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NekoControlEditor
{
    class PropControlDirection4 : PropControl
    {
        public string BitmapDefault { get; set; }
        public string BitmapDown { get; set; }
        public string BitmapLeft { get; set; }
        public string BitmapRight { get; set; }
        public string BitmapUp { get; set; }
        public string BitmapStick { get; set; }
        public ushort StickMovableRadius { get; set; }
    }
}
