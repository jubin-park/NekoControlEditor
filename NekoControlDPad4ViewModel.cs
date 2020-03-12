using System;
using System.Activities.Presentation.PropertyEditing;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Media.Imaging;

namespace NekoControlEditor
{
    class NekoControlDPad4ViewModel : NekoControlViewModel, ICloneable
    {
        #region DefaultBitmapImages
        public static readonly BitmapImage[] DefaultBitmapImages =
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
        #endregion

        #region BitmapImage Properties
        protected BitmapImage mBitmapImageDefault;
        [Browsable(false)]
        public BitmapImage BitmapImageDefault
        {
            get
            {
                if (mBitmapImageDefault == null)
                {
                    return DefaultBitmapImages[(int)EDPadType.Default];
                }
                return mBitmapImageDefault;
            }
            set
            {
                mBitmapImageDefault = value;
                notifyPropertyChanged("BitmapImageDefault");
            }
        }

        protected BitmapImage mBitmapImageDown;
        [Browsable(false)]
        public BitmapImage BitmapImageDown
        {
            get
            {
                if (mBitmapImageDown == null)
                {
                    return DefaultBitmapImages[(int)EDPadType.Down];
                }
                return mBitmapImageDown;
            }
            set
            {
                mBitmapImageDown = value;
                notifyPropertyChanged("BitmapImageDown");
            }
        }

        protected BitmapImage mBitmapImageLeft;
        [Browsable(false)]
        public BitmapImage BitmapImageLeft
        {
            get
            {
                if (mBitmapImageLeft == null)
                {
                    return DefaultBitmapImages[(int)EDPadType.Left];
                }
                return mBitmapImageLeft;
            }
            set
            {
                mBitmapImageLeft = value;
                notifyPropertyChanged("BitmapImageLeft");
            }
        }

        protected BitmapImage mBitmapImageRight;
        [Browsable(false)]
        public BitmapImage BitmapImageRight
        {
            get
            {
                if (mBitmapImageRight == null)
                {
                    return DefaultBitmapImages[(int)EDPadType.Right];
                }
                return mBitmapImageRight;
            }
            set
            {
                mBitmapImageRight = value;
                notifyPropertyChanged("BitmapImageRight");
            }
        }

        protected BitmapImage mBitmapImageUp;
        [Browsable(false)]
        public BitmapImage BitmapImageUp
        {
            get
            {
                if (mBitmapImageUp == null)
                {
                    return DefaultBitmapImages[(int)EDPadType.Up];
                }
                return mBitmapImageUp;
            }
            set
            {
                mBitmapImageUp = value;
                notifyPropertyChanged("BitmapImageUp");
            }
        }

        protected BitmapImage mBitmapImageStick;
        [Browsable(false)]
        public BitmapImage BitmapImageStick
        {
            get
            {
                if (mBitmapImageStick == null)
                {
                    return DefaultBitmapImages[(int)EDPadType.Stick];
                }
                return mBitmapImageStick;
            }
            set
            {
                mBitmapImageStick = value;
                notifyPropertyChanged("BitmapImageStick");
            }
        }
        #endregion

        #region BitmapPath Properties
        protected string mBitmapPathDefault;
        [Category("버튼 그래픽 파일")]
        [DisplayName("기본")]
        [Editor(typeof(PictureEditor), typeof(PropertyValueEditor))]
        public string BitmapPathDefault
        {
            get
            {
                return mBitmapPathDefault;
            }
            set
            {
                if (mBitmapPathDefault != value)
                {
                    try
                    {
                        var bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.UriSource = new Uri(value, UriKind.RelativeOrAbsolute);
                        bitmapImage.EndInit();
                        mBitmapPathDefault = value;
                        BitmapImageDefault = bitmapImage;
                    }
                    catch (FileNotFoundException)
                    {
                        Debug.Fail("Failed to load picture file.", value);
                        mBitmapPathDefault = string.Empty;
                        mBitmapImageDefault = null;
                    }
                    notifyPropertyChanged("BitmapPathDefault");
                    notifyPropertyChanged("BitmapImageDefault");
                }
            }
        }

        protected string mBitmapPathDown;
        [Category("버튼 그래픽 파일")]
        [DisplayName("아래 누름")]
        [Editor(typeof(PictureEditor), typeof(PropertyValueEditor))]
        public string BitmapPathDown
        {
            get
            {
                return mBitmapPathDown;
            }
            set
            {
                if (mBitmapPathDown != value)
                {
                    try
                    {
                        var bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.UriSource = new Uri(value, UriKind.RelativeOrAbsolute);
                        bitmapImage.EndInit();
                        mBitmapPathDown = value;
                        BitmapImageDown = bitmapImage;
                    }
                    catch (FileNotFoundException)
                    {
                        Debug.Fail("Failed to load picture file.", value);
                        mBitmapPathDown = string.Empty;
                        mBitmapImageDown = null;
                    }
                    notifyPropertyChanged("BitmapPathDown");
                }
            }
        }

        protected string mBitmapPathLeft;
        [Category("버튼 그래픽 파일")]
        [DisplayName("왼쪽 누름")]
        [Editor(typeof(PictureEditor), typeof(PropertyValueEditor))]
        public string BitmapPathLeft
        {
            get
            {
                return mBitmapPathLeft;
            }
            set
            {
                if (mBitmapPathLeft != value)
                {
                    try
                    {
                        var bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.UriSource = new Uri(value, UriKind.RelativeOrAbsolute);
                        bitmapImage.EndInit();
                        mBitmapPathLeft = value;
                        BitmapImageLeft = bitmapImage;
                    }
                    catch (FileNotFoundException)
                    {
                        Debug.Fail("Failed to load picture file.", value);
                        mBitmapPathLeft = string.Empty;
                        mBitmapImageLeft = null;
                    }
                    notifyPropertyChanged("BitmapPathLeft");
                    notifyPropertyChanged("BitmapImageLeft");
                }
            }
        }

        protected string mBitmapPathRight;
        [Category("버튼 그래픽 파일")]
        [DisplayName("오른쪽 누름")]
        [Editor(typeof(PictureEditor), typeof(PropertyValueEditor))]
        public string BitmapPathRight
        {
            get
            {
                return mBitmapPathRight;
            }
            set
            {
                if (mBitmapPathRight != value)
                {
                    try
                    {
                        var bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.UriSource = new Uri(value, UriKind.RelativeOrAbsolute);
                        bitmapImage.EndInit();
                        mBitmapPathRight = value;
                        BitmapImageRight = bitmapImage;
                    }
                    catch (FileNotFoundException)
                    {
                        Debug.Fail("Failed to load picture file.", value);
                        mBitmapPathRight = string.Empty;
                        mBitmapImageRight = null;
                    }
                    notifyPropertyChanged("BitmapPathRight");
                }
            }
        }

        protected string mBitmapPathUp;
        [Category("버튼 그래픽 파일")]
        [DisplayName("위 누름")]
        [Editor(typeof(PictureEditor), typeof(PropertyValueEditor))]
        public string BitmapPathUp
        {
            get
            {
                return mBitmapPathUp;
            }
            set
            {
                if (mBitmapPathUp != value)
                {
                    try
                    {
                        var bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.UriSource = new Uri(value, UriKind.RelativeOrAbsolute);
                        bitmapImage.EndInit();
                        mBitmapPathUp = value;
                        BitmapImageUp = bitmapImage;
                    }
                    catch (FileNotFoundException)
                    {
                        Debug.Fail("Failed to load picture file.", value);
                        mBitmapPathUp = string.Empty;
                        mBitmapImageUp = null;
                    }
                    notifyPropertyChanged("BitmapPathUp");
                }
            }
        }

        protected string mBitmapPathStick;
        [Category("버튼 그래픽 파일")]
        [DisplayName("조이스틱")]
        [Editor(typeof(PictureEditor), typeof(PropertyValueEditor))]
        public string BitmapPathStick
        {
            get
            {
                return mBitmapPathStick;
            }
            set
            {
                if (mBitmapPathStick != value)
                {
                    try
                    {
                        var bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.UriSource = new Uri(value, UriKind.RelativeOrAbsolute);
                        bitmapImage.EndInit();
                        mBitmapPathStick = value;
                        BitmapImageStick = bitmapImage;
                    }
                    catch (FileNotFoundException)
                    {
                        Debug.Fail("Failed to load picture file.", value);
                        mBitmapPathStick = string.Empty;
                        mBitmapImageStick = null;
                    }
                    notifyPropertyChanged("BitmapPathStick");
                }
            }
        }
        #endregion

        #region Extra Properties
        public string Type
        {
            get
            {
                return "(4방향)";
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
        #endregion

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
            mBitmapImageDefault = null;
            mBitmapImageDown = null;
            mBitmapImageLeft = null;
            mBitmapImageRight = null;
            mBitmapImageUp = null;
            mBitmapImageStick = null;
            mBitmapPathDefault = string.Empty;
            mBitmapPathDown = string.Empty;
            mBitmapPathLeft = string.Empty;
            mBitmapPathRight = string.Empty;
            mBitmapPathUp = string.Empty;
            mBitmapPathStick = string.Empty;
            mStickMovableRadius = 16;
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
            mBitmapImageDefault = other.mBitmapImageDefault;
            mBitmapImageDown = other.mBitmapImageDown;
            mBitmapImageLeft = other.mBitmapImageLeft;
            mBitmapImageRight = other.mBitmapImageRight;
            mBitmapImageUp = other.mBitmapImageUp;
            mBitmapImageStick = other.mBitmapImageStick;
            mBitmapPathDefault = other.mBitmapPathDefault;
            mBitmapPathDown = other.mBitmapPathDown;
            mBitmapPathLeft = other.mBitmapPathLeft;
            mBitmapPathRight = other.mBitmapPathRight;
            mBitmapPathUp = other.mBitmapPathUp;
            mBitmapPathStick = other.mBitmapPathStick;
            mStickMovableRadius = other.mStickMovableRadius;
        }

        public object Clone()
        {
            return new NekoControlDPad4ViewModel(this);
        }
    }
}
