﻿using System;
using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace NekoControlEditor
{
    class NekoControlDPad4ViewModel : NekoControlViewModel, ICloneable
    {
        public static readonly BitmapImage[] DefaultBitmapImage =
        {
            new BitmapImage(new Uri("image/dpad_none.png", UriKind.Relative)),
            new BitmapImage(new Uri("image/dpad_lower_left.png", UriKind.Relative)),
            new BitmapImage(new Uri("image/dpad_down.png", UriKind.Relative)),
            new BitmapImage(new Uri("image/dpad_lower_right.png", UriKind.Relative)),
            new BitmapImage(new Uri("image/dpad_left.png", UriKind.Relative)),
            new BitmapImage(new Uri("image/dpad_right.png", UriKind.Relative)),
            new BitmapImage(new Uri("image/dpad_upper_left.png", UriKind.Relative)),
            new BitmapImage(new Uri("image/dpad_up.png", UriKind.Relative)),
            new BitmapImage(new Uri("image/dpad_upper_right.png", UriKind.Relative)),
            new BitmapImage(new Uri("image/dpad_stick.png", UriKind.Relative)),
        };

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
        [DisplayName("위 누름")]
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

        private ushort mStickMovableRadius;
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

        private static uint mCount = 0;

        public NekoControlDPad4ViewModel()
        {
            if (this is NekoControlDPad8ViewModel == false)
            {
                string name = "$dpad4_";
                while (VariableNames.Contains(name + mCount))
                {
                    ++mCount;
                }
                Name = name + mCount;
            }
            BitmapDefault = "image/dpad_none.png";
            BitmapDown = "";
            BitmapLeft = "";
            BitmapRight = "";
            BitmapUp = "";
            BitmapStick = "image/dpad_stick.png";
            StickMovableRadius = 16;
        }

        public NekoControlDPad4ViewModel(NekoControlDPad4ViewModel other)
            : base(other)
        {
            if (other is NekoControlDPad8ViewModel == false)
            {
                string name = other.Name;
                do
                {
                    name += "_copy";
                } while (VariableNames.Contains(name));
                Name = name;
            }
            BitmapDefault = other.BitmapDefault;
            BitmapDown = other.BitmapDown;
            BitmapLeft = other.BitmapLeft;
            BitmapRight = other.BitmapRight;
            BitmapUp = other.BitmapUp;
            BitmapStick = other.BitmapStick;
            StickMovableRadius = other.StickMovableRadius;
        }

        public object Clone()
        {
            return new NekoControlDPad4ViewModel(this);
        }
    }
}
