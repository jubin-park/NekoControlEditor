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
                        ImageSourceControl = bitmapImage;
                    }
                    catch (FileNotFoundException)
                    {
                        Debug.Fail("Failed to load picture file.", value);
                        mBitmapPathDefault = string.Empty;
                        mBitmapImageDefault = null;
                    }
                    notifyPropertyChanged("BitmapPathDefault");
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
                        mBitmapPathPressed = string.Empty;
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
                if (mKey.Value == EKeys.NULL)
                {
                    return "(미지정)";
                }
                return $"({mKey.Value.ToString().Substring(3)})";
            }
        }

        private EKeysValue mKey;
        [DisplayName("키")]
        [Editor(typeof(KeyEditor), typeof(PropertyValueEditor))]
        public EKeysValue Key
        {
            get
            {
                return mKey;
            }
            set
            {
                mKey = value;
                notifyPropertyChanged("Key");
                notifyPropertyChanged("Type");
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
            mKey = new EKeysValue(EKeys.NULL);
            mKey.PropertyChanged += new PropertyChangedEventHandler(eKeysPropertyChanged);
            mWidth = 48;
            mHeight = 48;
            mBitmapImageDefault = null;
            mBitmapImagePressed = null;
            mBitmapPathDefault = "image/UltimateDroidButton1.png";
            mBitmapPathPressed = "image/UltimateDroidButton1Pressed.png";
            mImageSourceControl = BitmapImageDefault; // must be property
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
            mKey = new EKeysValue(other.mKey.Value);
            mWidth = other.mWidth;
            mHeight = other.mHeight;
            mBitmapImageDefault = other.mBitmapImageDefault;
            mBitmapImagePressed = other.mBitmapImagePressed;
            mBitmapPathDefault = other.mBitmapPathDefault;
            mBitmapPathPressed = other.mBitmapPathPressed;
            mImageSourceControl = other.mImageSourceControl;
        }

        public object Clone()
        {
            return new NekoControlKeyButtonViewModel(this);
        }

        private void eKeysPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e != null)
            {
                var eKeysValue = sender as EKeysValue;
                Key = eKeysValue;
            }
        }
    }
}
