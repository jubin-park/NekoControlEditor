using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace NekoControlEditor
{
    public class PropControl : INotifyPropertyChanged
    {
        private string mName;
        [Category("")]
        [DisplayName("변수 이름")]
        public string Name
        {
            get => mName;
            set
            {
                if (mName != value)
                {
                    mName = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        private EKeys mKey;
        [Category("키보드")]
        [DisplayName("키")]
        public EKeys Key
        {
            get => mKey;
            set
            {
                if (mKey != value)
                {
                    mKey = value;
                    NotifyPropertyChanged("Key");
                }
            }
        }

        private int mX;
        [Category("위치")]
        [DisplayName("X 좌표")]
        public int X
        {
            get => mX;
            set
            {
                if (mX != value)
                {
                    mX = value;
                    NotifyPropertyChanged("X");
                }
            }
        }

        private int mY;
        [Category("위치")]
        [DisplayName("Y 좌표")]
        public int Y
        {
            get => mY;
            set
            {
                if (mY != value)
                {
                    mY = value;
                    NotifyPropertyChanged("Y");
                }
            }
        }

        private int mZ;
        [Category("위치")]
        [DisplayName("Z 우선순위")]
        public int Z
        {
            get => mZ;
            set
            {
                if (mZ != value)
                {
                    mZ = value;
                    NotifyPropertyChanged("Z");
                }
            }
        }

        private byte mOpacity;
        [Category("속성")]
        [DisplayName("투명도")]
        public byte Opacity
        {
            get => mOpacity;
            set
            {
                if (mOpacity != value)
                {
                    mOpacity = value;
                    NotifyPropertyChanged("Opacity");
                }
            }
        }

        private bool mbVisible;
        [Category("속성")]
        [DisplayName("표시 여부")]
        public bool Visible
        {
            get => mbVisible;
            set
            {
                if (mbVisible != value)
                {
                    mbVisible = value;
                    NotifyPropertyChanged("Visible");
                }
            }
        }

        private bool mbRectTouchable;
        [Category("속성")]
        [DisplayName("투명영역 터치 여부")]
        public bool RectTouchable
        {
            get => mbRectTouchable;
            set
            {
                if (mbRectTouchable != value)
                {
                    mbRectTouchable = value;
                    NotifyPropertyChanged("RectTouchable");
                }
            }
        }

        private uint mWidth;
        [Category("크기")]
        [DisplayName("너비")]
        public uint Width
        {
            get => mWidth;
            set
            {
                mWidth = value;
            }
        }

        private uint mHeight;
        [Category("크기")]
        [DisplayName("높이")]
        public uint Height
        {
            get => mHeight;
            set
            {
                mHeight = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

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

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
