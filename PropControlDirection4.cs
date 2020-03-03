using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace NekoControlEditor
{
    public class PropControlDirection4 : PropControl
    {
        [Category("버튼 그래픽 파일")]
        [DisplayName("기본")]
        public string BitmapDefault { get; set; }

        [Category("버튼 그래픽 파일")]
        [DisplayName("아래 누름")]
        public string BitmapDown { get; set; }

        [Category("버튼 그래픽 파일")]
        [DisplayName("왼쪽 누름")]
        public string BitmapLeft { get; set; }

        [Category("버튼 그래픽 파일")]
        [DisplayName("오른쪽 누름")]
        public string BitmapRight { get; set; }

        [Category("버튼 그래픽 파일")]
        [DisplayName("오른쪽 누름")]
        public string BitmapUp { get; set; }

        [Category("버튼 그래픽 파일")]
        [DisplayName("조이스틱")]
        public string BitmapStick { get; set; }

        [Category("조이스틱")]
        [DisplayName("최대 이동 반지름")]
        public ushort StickMovableRadius { get; set; }

        public PropControlDirection4()
        {
            BitmapDefault = "";
            BitmapDown = "";
            BitmapLeft = "";
            BitmapRight = "";
            BitmapUp = "";
            BitmapStick = "";
            StickMovableRadius = 16;
        }
    }
}
