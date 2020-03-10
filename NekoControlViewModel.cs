using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace NekoControlEditor
{
    public abstract class NekoControlViewModel : INotifyPropertyChanged
    {
        private string mName;
        [Category("")]
        [DisplayName("변수 이름")]
        [Description("가상 컨트롤의 변수 이름입니다.")]
        public string Name
        {
            get
            {
                return mName;
            }
            set
            {
                if (value == null || value == "")
                {
                    return;
                }
                if (mName != value)
                {
                    if (VariableNames.Contains(value))
                    {
                        return;
                    }
                    VariableNames.Remove(mName);
                    VariableNames.Add(value);
                    mName = value;
                    notifyPropertyChanged("Name");
                }
            }
        }

        private int mX;
        [Category("위치")]
        [DisplayName("X 좌표")]
        public int X
        {
            get
            {
                return mX;
            }
            set
            {
                if (mX != value)
                {
                    mX = value;
                    notifyPropertyChanged("X");
                }
            }
        }

        private int mY;
        [Category("위치")]
        [DisplayName("Y 좌표")]
        public int Y
        {
            get
            {
                return mY;
            }
            set
            {
                if (mY != value)
                {
                    mY = value;
                    notifyPropertyChanged("Y");
                }
            }
        }

        private int mZ;
        [Category("위치")]
        [DisplayName("Z 우선순위")]
        public int Z
        {
            get
            {
                return mZ;
            }
            set
            {
                if (mZ != value)
                {
                    mZ = value;
                    notifyPropertyChanged("Z");
                }
            }
        }

        private byte mOpacity;
        [Category("속성")]
        [DisplayName("투명도")]
        public byte Opacity
        {
            get
            {
                return mOpacity;
            }
            set
            {
                if (mOpacity != value)
                {
                    mOpacity = value;
                    notifyPropertyChanged("Opacity");
                    notifyPropertyChanged("RealOpacity");
                }
            }
        }

        public double RealOpacity
        {
            get
            {
                return mOpacity / 255.0;
            }
        }

        private bool mbVisible;
        [Category("속성")]
        [DisplayName("표시 여부")]
        public bool Visible
        {
            get
            {
                return mbVisible;
            }
            set
            {
                if (mbVisible != value)
                {
                    mbVisible = value;
                    notifyPropertyChanged("Visible");
                }
            }
        }

        private bool mbRectTouchable;
        [Category("속성")]
        [DisplayName("투명영역 터치 여부")]
        public bool RectTouchable
        {
            get
            {
                return mbRectTouchable;
            }
            set
            {
                if (mbRectTouchable != value)
                {
                    mbRectTouchable = value;
                    notifyPropertyChanged("RectTouchable");
                }
            }
        }

        private uint mWidth;
        [Category("크기")]
        [DisplayName("너비")]
        public uint Width
        {
            get
            {
                return mWidth;
            }
            set
            {
                if (mWidth != value)
                {
                    mWidth = value;
                    notifyPropertyChanged("Width");
                }
            }
        }

        private uint mHeight;
        [Category("크기")]
        [DisplayName("높이")]
        public uint Height
        {
            get
            {
                return mHeight;
            }
            set
            {
                if (mHeight != value)
                {
                    mHeight = value;
                    notifyPropertyChanged("Height");
                }
            }
        }

        private string mBorderColor;
        [ReadOnly(true), Browsable(false)]
        public string BorderColor
        {
            get
            {
                return mBorderColor;
            }
            set
            {
                if (mBorderColor != value)
                {
                    mBorderColor = value;
                    notifyPropertyChanged("BorderColor");
                }
            }
        }

        private bool mbSelected;
        [ReadOnly(true), Browsable(false)]
        public bool IsSelected
        {
            get
            {
                return mbSelected;
            }
            set
            {
                if (mbSelected != value)
                {
                    mbSelected = value;
                    notifyPropertyChanged("IsSelected");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static HashSet<string> VariableNames = new HashSet<string>();

        protected NekoControlViewModel()
        {
            X = 0;
            Y = 0;
            Z = 0;
            Width = 128;
            Height = 128;
            Opacity = 255;
            Visible = true;
            RectTouchable = false;
            IsSelected = false;
        }

        protected NekoControlViewModel(NekoControlViewModel other)
        {
            X = other.X;
            Y = other.Y;
            Z = other.Z;
            Width = other.Width;
            Height = other.Height;
            Opacity = other.Opacity;
            Visible = other.Visible;
            RectTouchable = other.RectTouchable;
            IsSelected = other.IsSelected;
        }

        protected void notifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
