using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NekoControlEditor
{
    class NekoControlDPad4ViewModel : NekoControlViewModel
    {
        private string mBitmapDefault;
        [Category("버튼 그래픽 파일")]
        [DisplayName("기본")]
        public string BitmapDefault
        {
            get
            {
                return mBitmapDefault;
            }
            set
            {
                if (mBitmapDefault != value)
                {
                    mBitmapDefault = value;
                    notifyPropertyChanged("BitmapDefault");
                }
            }
        }

        private string mBitmapDown;
        [Category("버튼 그래픽 파일")]
        [DisplayName("아래 누름")]
        public string BitmapDown
        {
            get
            {
                return mBitmapDown;
            }
            set
            {
                if (mBitmapDown != value)
                {
                    mBitmapDown = value;
                    notifyPropertyChanged("BitmapDown");
                }
            }
        }

        private string mBitmapLeft;
        [Category("버튼 그래픽 파일")]
        [DisplayName("왼쪽 누름")]
        public string BitmapLeft
        {
            get
            {
                return mBitmapLeft;
            }
            set
            {
                if (mBitmapLeft != value)
                {
                    mBitmapLeft = value;
                    notifyPropertyChanged("BitmapLeft");
                }
            }
        }

        private string mBitmapRight;
        [Category("버튼 그래픽 파일")]
        [DisplayName("오른쪽 누름")]
        public string BitmapRight
        {
            get
            {
                return mBitmapRight;
            }
            set
            {
                if (mBitmapRight != value)
                {
                    mBitmapRight = value;
                    notifyPropertyChanged("BitmapRight");
                }
            }
        }

        private string mBitmapUp;
        [Category("버튼 그래픽 파일")]
        [DisplayName("오른쪽 누름")]
        public string BitmapUp
        {
            get
            {
                return mBitmapUp;
            }
            set
            {
                if (mBitmapUp != value)
                {
                    mBitmapUp = value;
                    notifyPropertyChanged("BitmapUp");
                }
            }
        }

        private string mBitmapStick;
        [Category("버튼 그래픽 파일")]
        [DisplayName("조이스틱")]
        public string BitmapStick
        {
            get
            {
                return mBitmapStick;
            }
            set
            {
                if (mBitmapStick != value)
                {
                    mBitmapStick = value;
                    notifyPropertyChanged("BitmapStick");
                }
            }
        }

        private ushort mStickMovableRadius = 16;
        [Category("조이스틱")]
        [DisplayName("최대 이동 반지름")]
        public ushort StickMovableRadius
        {
            get
            {
                return mStickMovableRadius;
            }
            set
            {
                if (mStickMovableRadius != value)
                {
                    mStickMovableRadius = value;
                    notifyPropertyChanged("StickMovableRadius");
                }
            }
        }
        private static int mCount = 0;

        public NekoControlDPad4ViewModel()
        {
            Name = "$dpad4_" + (++mCount);
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
