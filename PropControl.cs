using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NekoControlEditor
{
    class PropControl
    {
        public EKeys Key { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public byte Opacity { get; set; }
        public bool Visible { get; set; }
        public bool RectTouchable { get; set; }
        public uint Width { get; }
        public uint Height { get; }

        public PropControl()
        {
            Key = EKeys.EMPTY;
            X = 0;
            Y = 0;
            Z = 1;
            Opacity = 255;
            Visible = true;
            RectTouchable = false;
        }
    }
}
