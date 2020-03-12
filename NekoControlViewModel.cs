using System;
using System.Activities.Presentation.PropertyEditing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Media.Imaging;

namespace NekoControlEditor
{
    public abstract class NekoControlViewModel : INotifyPropertyChanged
    {
        #region Properties
        protected string mName;
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
                if (value == null || value == string.Empty)
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

        protected int mX;
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

        protected int mY;
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

        protected int mZ;
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

        protected byte mOpacity;
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

        protected bool mbVisible;
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

        protected bool mbRectTouchable;
        [Category("속성")]
        [DisplayName("투명영역 터치 여부")]
        public bool IsRectTouchable
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
                    notifyPropertyChanged("IsRectTouchable");
                }
            }
        }

        protected uint mWidth;
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

        protected uint mHeight;
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

        protected string mBorderColor;
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

        protected bool mbSelected;
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
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public static HashSet<string> VariableNames = new HashSet<string>();

        protected NekoControlViewModel()
        {
            mX = 0;
            mY = 0;
            mZ = 0;
            mWidth = 128;
            mHeight = 128;
            mOpacity = 255;
            mbVisible = true;
            mbRectTouchable = false;
            mbSelected = false;
        }

        protected NekoControlViewModel(NekoControlViewModel other)
        {
            mX = other.mX;
            mY = other.mY;
            mZ = other.mZ;
            mWidth = other.mWidth;
            mHeight = other.mHeight;
            mOpacity = other.mOpacity;
            mbVisible = other.mbVisible;
            mbRectTouchable = other.mbRectTouchable;
            mbSelected = other.mbSelected;
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
