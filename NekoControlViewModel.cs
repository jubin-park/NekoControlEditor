using System;
using System.Activities.Presentation.PropertyEditing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NekoControlEditor
{
    public class NekoControlViewModel : INotifyPropertyChanged
    {
        #region Properties
        public string Type
        {
            get
            {
                return null;
            }
        }

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
        [Browsable(false)]
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

        protected SliderProperty<byte> mSliderValueOpacity;
        [Category("속성")]
        [DisplayName("투명도")]
        [Editor(typeof(SliderEditor), typeof(PropertyValueEditor))]
        [JsonIgnore]
        public SliderProperty<byte> SliderValueOpacity
        {
            get
            {
                return mSliderValueOpacity;
            }
            set
            {
                if (mSliderValueOpacity != value)
                {
                    mSliderValueOpacity = value;
                    notifyPropertyChanged("SliderValueOpacity");
                }
            }
        }

        [JsonIgnore]
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
        [JsonIgnore]
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
        [JsonIgnore]
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

        protected ImageSource mImageSourceControl;
        [Browsable(false)]
        [JsonIgnore]
        public ImageSource ImageSourceControl
        {
            get
            {
                return mImageSourceControl;
            }
            set
            {
                mImageSourceControl = value;
                notifyPropertyChanged("ImageSourceControl");
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public static HashSet<string> VariableNames = new HashSet<string>();

        protected static string FAILED_LOAD_BITMAP_MESSAGE = "Failed to load picture file.";

        protected NekoControlViewModel()
        {
            mX = 0;
            mY = 0;
            mZ = 0;
            mWidth = 128;
            mHeight = 128;
            mOpacity = 255;
            mSliderValueOpacity = new SliderProperty<byte>(mOpacity, 0, 255, 5);
            mSliderValueOpacity.PropertyChanged += new PropertyChangedEventHandler(SliderPropertyChanged);
            mbVisible = true;
            mbRectTouchable = true;
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
            mSliderValueOpacity = new SliderProperty<byte>(other.mOpacity, 0, 255, 5);
            mSliderValueOpacity.PropertyChanged += new PropertyChangedEventHandler(SliderPropertyChanged);
            mbVisible = other.mbVisible;
            mbRectTouchable = other.mbRectTouchable;
            mbSelected = other.mbSelected;
        }

        protected NekoControlViewModel(JObject jObject)
        {
            Name = jObject["Name"].ToString();
            mX = jObject["X"].Value<int>();
            mY = jObject["Y"].Value<int>();
            mZ = jObject["Z"].Value<int>();
            mWidth = jObject["Width"].Value<uint>();
            mHeight = jObject["Height"].Value<uint>();
            mOpacity = jObject["Opacity"].Value<byte>();
            mSliderValueOpacity = new SliderProperty<byte>(mOpacity, 0, 255, 5);
            mSliderValueOpacity.PropertyChanged += new PropertyChangedEventHandler(SliderPropertyChanged);
            mbVisible = jObject["Visible"].Value<bool>();
            mbRectTouchable = jObject["IsRectTouchable"].Value<bool>();
            mbSelected = false;
        }
        protected void notifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void SliderPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e != null)
            {
                var slider = sender as SliderProperty<byte>;
                Opacity = slider.Value;
            }
        }

        protected string GetRelativePath(string strSrc, string strDelete)
        {
            return strSrc.Replace(strDelete, string.Empty).Replace('\\', '/');
        }

        protected BitmapImage CreateCacheBitmapImage(string path)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bitmapImage.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
            bitmapImage.EndInit();
            return bitmapImage;
        }
    }
}
