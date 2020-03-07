using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                if (mName != value)
                {
                    mName = value;
                    notifyPropertyChanged("Name");
                }
            }
        }

        private EKeys mKey;
        [Category("키보드")]
        [DisplayName("키")]
        public EKeys Key
        {
            get
            {
                return mKey;
            }
            set
            {
                if (mKey != value)
                {
                    mKey = value;
                    notifyPropertyChanged("Key");
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
                }
            }
        }

        private bool mbVisible = true;
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

        public event PropertyChangedEventHandler PropertyChanged;

        public NekoControlViewModel()
        {
            Key = EKeys.EMPTY;
            X = 0;
            Y = 0;
            Z = 0;
            Width = 64;
            Height = 64;
            Opacity = 255;
            Visible = true;
            RectTouchable = false;
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
