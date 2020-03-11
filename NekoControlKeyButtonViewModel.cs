using System;
using System.Activities.Presentation.PropertyEditing;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Media.Imaging;

namespace NekoControlEditor
{
    public class NekoControlKeyButtonViewModel : NekoControlViewModel
    {
        #region DefaultBitmapImages
        public static readonly BitmapImage[] DefaultBitmapImages =
        {
            new BitmapImage(new Uri("image/UltimateDroidButton1.png", UriKind.Relative)),
            new BitmapImage(new Uri("image/UltimateDroidButton1Pressed.png", UriKind.Relative)),
        };
        #endregion

        #region BitmapImage Properties
        private BitmapImage mBitmapImageDefault;
        [Browsable(false)]
        public BitmapImage BitmapImageDefault
        {
            get
            {
                if (mBitmapImageDefault == null)
                {
                    return DefaultBitmapImages[0];
                }
                return mBitmapImageDefault;
            }
            set
            {
                mBitmapImageDefault = value;
                notifyPropertyChanged("BitmapImageDefault");
            }
        }

        private BitmapImage mBitmapImagePressed;
        [Browsable(false)]
        public BitmapImage BitmapImagePressed
        {
            get
            {
                if (mBitmapImagePressed == null)
                {
                    return DefaultBitmapImages[1];
                }
                return mBitmapImagePressed;
            }
            set
            {
                mBitmapImagePressed = value;
                notifyPropertyChanged("BitmapImagePressed");
            }
        }
        #endregion 

        #region BitmapPath Properties
        private string mBitmapPathDefault;
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
                        mBitmapImageDefault = bitmapImage;
                    }
                    catch (FileNotFoundException)
                    {
                        Debug.Fail("Failed to load picture file.", value);
                        mBitmapPathDefault = "";
                        mBitmapImageDefault = null;
                    }
                    notifyPropertyChanged("BitmapPathDefault");
                    notifyPropertyChanged("BitmapImageDefault");
                }
            }
        }

        private string mBitmapPathPressed;
        [Category("버튼 그래픽 파일")]
        [DisplayName("누름")]
        [Editor(typeof(PictureEditor), typeof(PropertyValueEditor))]
        public string BitmapPathPressed
        {
            get
            {
                return mBitmapPathPressed;
            }
            set
            {
                if (mBitmapPathPressed != value)
                {
                    try
                    {
                        var bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.UriSource = new Uri(value, UriKind.RelativeOrAbsolute);
                        bitmapImage.EndInit();
                        mBitmapPathPressed = value;
                        mBitmapImagePressed = bitmapImage;
                    }
                    catch (FileNotFoundException)
                    {
                        Debug.Fail("Failed to load picture file.", value);
                        mBitmapPathPressed = "";
                        mBitmapImagePressed = null;
                    }
                    notifyPropertyChanged("BitmapPathPressed");
                }
            }
        }
        #endregion

        #region Extra Properties
        public string Type
        {
            get
            {
                return "(키)";
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
        #endregion

        private static uint mCount = 0;

        public NekoControlKeyButtonViewModel()
        {
            string name = "$key_";
            while (VariableNames.Contains(name + mCount))
            {
                ++mCount;
            }
            Name = name + mCount;
            mKey = EKeys.EMPTY;
            mWidth = 48;
            mHeight = 48;
            mBitmapImageDefault = null;
            mBitmapImagePressed = null;
            mBitmapPathDefault = "image/UltimateDroidButton1.png";
            mBitmapPathPressed = "image/UltimateDroidButton1Pressed.png";
        }

        public NekoControlKeyButtonViewModel(NekoControlKeyButtonViewModel other)
            : base(other)
        {
            string name = other.Name;
            do
            {
                name += "_copy";
            } while (VariableNames.Contains(name));
            Name = name;
            mKey = other.mKey;
            mWidth = other.mWidth;
            mHeight = other.mHeight;
            mBitmapImageDefault = other.mBitmapImageDefault;
            mBitmapImagePressed = other.mBitmapImagePressed;
            mBitmapPathDefault = other.mBitmapPathDefault;
            mBitmapPathPressed = other.mBitmapPathPressed;
        }

        public object Clone()
        {
            return new NekoControlKeyButtonViewModel(this);
        }
    }
}
