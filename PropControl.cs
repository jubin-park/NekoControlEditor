using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace NekoControlEditor
{
    public class PropControl
    {
        [Category("키보드")]
        [DisplayName("키")]
        public EKeys Key { get; set; }

        [Category("위치")]
        [DisplayName("X 좌표")]
        public int X { get; set; }

        [Category("위치")]
        [DisplayName("Y 좌표")]
        public int Y { get; set; }

        [Category("위치")]
        [DisplayName("Z 우선순위")]
        public int Z { get; set; }

        [Category("속성")]
        [DisplayName("투명도")]
        public byte Opacity { get; set; }

        [Category("속성")]
        [DisplayName("표시 여부")]
        public bool Visible { get; set; }

        [Category("속성")]
        [DisplayName("투명영역 터치 여부")]
        public bool RectTouchable { get; set; }

        [Category("크기")]
        [DisplayName("너비")]
        public uint Width { get; set; }

        [Category("크기")]
        [DisplayName("높이")]
        public uint Height { get; set; }

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
