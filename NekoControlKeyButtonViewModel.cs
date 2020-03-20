using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        [JsonIgnore]
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
        [JsonIgnore]
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
                        BitmapImage bitmapImage = null;
                        if (value != string.Empty)
                        {
                            bitmapImage = new BitmapImage();
                            bitmapImage.BeginInit();
                            bitmapImage.UriSource = new Uri(value, UriKind.RelativeOrAbsolute);
                            bitmapImage.EndInit();
                        }
                        mBitmapPathDefault = value;
                        mBitmapImageDefault = bitmapImage;
                    }
                    catch (FileNotFoundException)
                    {
                        mBitmapPathDefault = string.Empty;
                        mBitmapImageDefault = null;
                        Debug.Fail(FAILED_LOAD_BITMAP_MESSAGE, value);
                    }
                    ImageSourceControl = BitmapImageDefault;
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
                        BitmapImage bitmapImage = null;
                        if (value != string.Empty)
                        {
                            bitmapImage = new BitmapImage();
                            bitmapImage.BeginInit();
                            bitmapImage.UriSource = new Uri(value, UriKind.RelativeOrAbsolute);
                            bitmapImage.EndInit();
                        }
                        mBitmapPathPressed = value;
                        mBitmapImagePressed = bitmapImage;
                    }
                    catch (FileNotFoundException)
                    {
                        Debug.Fail(FAILED_LOAD_BITMAP_MESSAGE, value);
                        mBitmapPathPressed = string.Empty;
                        mBitmapImagePressed = null;
                    }
                    notifyPropertyChanged("BitmapPathPressed");
                }
            }
        }
        #endregion

        #region Extra Properties
        public new string Type
        {
            get
            {
                return "KeyButton";
            }
        }

        [JsonIgnore]
        public string TypeName
        {
            get
            {
                if (mInputKey.Value == EInput.NULL)
                {
                    return "(미지정)";
                }
                return $"({mInputKey.Value.ToString().Substring(3)})";
            }
        }

        private InputProperty mInputKey;
        [DisplayName("키")]
        [Editor(typeof(InputEditor), typeof(PropertyValueEditor))]
        public InputProperty InputKey
        {
            get
            {
                return mInputKey;
            }
            set
            {
                mInputKey = value;
                notifyPropertyChanged("InputKey");
                notifyPropertyChanged("TypeName");
            }
        }
        #endregion

        private static uint mCount = 0;

        public NekoControlKeyButtonViewModel()
        {
            mbRectTouchable = false;
            string name = "key_";
            while (VariableNames.Contains(name + mCount))
            {
                ++mCount;
            }
            Name = name + mCount;
            mInputKey = new InputProperty(EInput.NULL);
            mInputKey.PropertyChanged += new PropertyChangedEventHandler(eKeysPropertyChanged);
            mWidth = 48;
            mHeight = 48;
            mBitmapImageDefault = null;
            mBitmapImagePressed = null;
            mBitmapPathDefault = string.Empty;
            mBitmapPathPressed = string.Empty;
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
            mInputKey = new InputProperty(other.mInputKey.Value);
            mWidth = other.mWidth;
            mHeight = other.mHeight;
            mBitmapImageDefault = other.mBitmapImageDefault;
            mBitmapImagePressed = other.mBitmapImagePressed;
            mBitmapPathDefault = other.mBitmapPathDefault;
            mBitmapPathPressed = other.mBitmapPathPressed;
            mImageSourceControl = other.mImageSourceControl;
        }

        public NekoControlKeyButtonViewModel(JObject jObject)
            : base(jObject)
        {
            mInputKey = new InputProperty(jObject["InputKey"]["Value"].ToObject<EInput>());
            BitmapPathDefault = jObject["BitmapPathDefault"].ToString();
            BitmapPathPressed = jObject["BitmapPathPressed"].ToString();
            ImageSourceControl = BitmapImageDefault; // must be property
        }

        public object Clone()
        {
            return new NekoControlKeyButtonViewModel(this);
        }

        public string GetRubyScript(string controlPath)
        {
            string script =
$@"    @{mName} = ControlKeyButton.new(Input::{mInputKey.Value.ToString()}, {mX}, {mY}, {mZ}, {mWidth}, {mHeight}, @viewport)
    @{mName}.set_image_default(RPG::Cache.neko_control(""{GetRelativePath(mBitmapPathDefault, controlPath)}""))
    @{mName}.set_image_pressed(RPG::Cache.neko_control(""{GetRelativePath(mBitmapPathPressed, controlPath)}""))
";
            if (mOpacity < 255)
            {
                script += $"    @{mName}.opacity = {mOpacity}" + '\n';
            }
            if (mbVisible == false)
            {
                script += $"    @{mName}.visible = {mbVisible.ToString().ToLower()}" + '\n';
            }
            script += $"    @{mName}.rect_touchable = {mbRectTouchable.ToString().ToLower()}" + '\n';
            script += $"    @controls.add(@{mName})" + '\n';
            return script;
        }

        private void eKeysPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e != null)
            {
                var inputProperty = sender as InputProperty;
                InputKey = inputProperty;
            }
        }
    }
}
