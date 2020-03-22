using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Activities.Presentation.PropertyEditing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace NekoControlEditor
{
    class NekoControlDPad8ViewModel : NekoControlDPad4ViewModel
    {
        #region BitmapImage Properties
        private BitmapImage mBitmapImageLowerLeft;
        [Browsable(false)]
        [JsonIgnore]
        public BitmapImage BitmapImageLowerLeft
        {
            get
            {
                if (mBitmapImageLowerLeft == null)
                {
                    return DefaultBitmapImages[(int)EDPadType.LowerLeft];
                }
                return mBitmapImageLowerLeft;
            }
            set
            {
                mBitmapImageLowerLeft = value;
                notifyPropertyChanged("BitmapImageLowerLeft");
            }
        }

        private BitmapImage mBitmapImageLowerRight;
        [Browsable(false)]
        [JsonIgnore]
        public BitmapImage BitmapImageLowerRight
        {
            get
            {
                if (mBitmapImageLowerRight == null)
                {
                    return DefaultBitmapImages[(int)EDPadType.LowerRight];
                }
                return mBitmapImageLowerRight;
            }
            set
            {
                mBitmapImageLowerRight = value;
                notifyPropertyChanged("BitmapImageLowerRight");
            }
        }

        private BitmapImage mBitmapImageUpperLeft;
        [Browsable(false)]
        [JsonIgnore]
        public BitmapImage BitmapImageUpperLeft
        {
            get
            {
                if (mBitmapImageUpperLeft == null)
                {
                    return DefaultBitmapImages[(int)EDPadType.UpperLeft];
                }
                return mBitmapImageUpperLeft;
            }
            set
            {
                mBitmapImageUpperLeft = value;
                notifyPropertyChanged("BitmapImageUpperLeft");
            }
        }

        private BitmapImage mBitmapImageUpperRight;
        [Browsable(false)]
        [JsonIgnore]
        public BitmapImage BitmapImageUpperRight
        {
            get
            {
                if (mBitmapImageUpperRight == null)
                {
                    return DefaultBitmapImages[(int)EDPadType.UpperRight];
                }
                return mBitmapImageUpperRight;
            }
            set
            {
                mBitmapImageUpperRight = value;
                notifyPropertyChanged("BitmapImageUpperRight");
            }
        }
        #endregion

        #region BitmapPath Properties
        private string mBitmapPathUpperLeft;
        [Category("버튼 그래픽 파일")]
        [DisplayName("왼쪽 위 누름")]
        [Editor(typeof(PictureEditor), typeof(PropertyValueEditor))]
        public string BitmapPathUpperLeft
        {
            get
            {
                return mBitmapPathUpperLeft;
            }
            set
            {
                try
                {
                    BitmapImage bitmapImage = null;
                    if (value != string.Empty)
                    {
                        bitmapImage = CreateCacheBitmapImage(value);
                    }
                    mBitmapPathUpperLeft = value;
                    mBitmapImageUpperLeft = bitmapImage;
                }
                catch (FileNotFoundException)
                {
                    Debug.Fail(FAILED_LOAD_BITMAP_MESSAGE, value);
                    mBitmapPathUpperLeft = string.Empty;
                    mBitmapImageUpperLeft = null;
                }
                notifyPropertyChanged("BitmapPathUpperLeft");
            }
        }

        private string mBitmapPathLowerLeft;
        [Category("버튼 그래픽 파일")]
        [DisplayName("왼쪽 아래 누름")]
        [Editor(typeof(PictureEditor), typeof(PropertyValueEditor))]
        public string BitmapPathLowerLeft
        {
            get
            {
                return mBitmapPathLowerLeft;
            }
            set
            {
                try
                {
                    BitmapImage bitmapImage = null;
                    if (value != string.Empty)
                    {
                        bitmapImage = CreateCacheBitmapImage(value);
                    }
                    mBitmapPathLowerLeft = value;
                    mBitmapImageLowerLeft = bitmapImage;
                }
                catch (FileNotFoundException)
                {
                    Debug.Fail(FAILED_LOAD_BITMAP_MESSAGE, value);
                    mBitmapPathLowerLeft = string.Empty;
                    mBitmapImageLowerLeft = null;
                }
                notifyPropertyChanged("BitmapPathLowerLeft");
            }
        }

        private string mBitmapPathLowerRight;
        [Category("버튼 그래픽 파일")]
        [DisplayName("오른쪽 아래 누름")]
        [Editor(typeof(PictureEditor), typeof(PropertyValueEditor))]
        public string BitmapPathLowerRight
        {
            get
            {
                return mBitmapPathLowerRight;
            }
            set
            {
                try
                {
                    BitmapImage bitmapImage = null;
                    if (value != string.Empty)
                    {
                        bitmapImage = CreateCacheBitmapImage(value);
                    }
                    mBitmapPathLowerRight = value;
                    mBitmapImageLowerRight = bitmapImage;
                }
                catch (FileNotFoundException)
                {
                    Debug.Fail(FAILED_LOAD_BITMAP_MESSAGE, value);
                    mBitmapPathLowerRight = string.Empty;
                    mBitmapImageLowerRight = null;
                }
                notifyPropertyChanged("BitmapPathLowerRight");
            }
        }

        private string mBitmapPathUpperRight;
        [Category("버튼 그래픽 파일")]
        [DisplayName("오른쪽 위 누름")]
        [Editor(typeof(PictureEditor), typeof(PropertyValueEditor))]
        public string BitmapPathUpperRight
        {
            get
            {
                return mBitmapPathUpperRight;
            }
            set
            {
                try
                {
                    BitmapImage bitmapImage = null;
                    if (value != string.Empty)
                    {
                        bitmapImage = CreateCacheBitmapImage(value);
                    }
                    mBitmapPathUpperRight = value;
                    mBitmapImageUpperRight = bitmapImage;
                }
                catch (FileNotFoundException)
                {
                    Debug.Fail(FAILED_LOAD_BITMAP_MESSAGE, value);
                    mBitmapPathUpperRight = string.Empty;
                    mBitmapImageUpperRight = null;
                }
                notifyPropertyChanged("BitmapPathUpperRight");
            }
        }
        #endregion

        #region Extra Properties
        public new string Type
        {
            get
            {
                return "DPad8";
            }
        }

        [JsonIgnore]
        public new string TypeName
        {
            get
            {
                return "(8방향)";
            }
        }
        #endregion

        private static uint mCount = 0;

        public NekoControlDPad8ViewModel()
        {
            string name = "dpad8_";
            while (VariableNames.Contains(name + mCount))
            {
                ++mCount;
            }
            Name = name + mCount;
            mBitmapPathLowerLeft = string.Empty;
            mBitmapPathLowerRight = string.Empty;
            mBitmapPathUpperLeft = string.Empty;
            mBitmapPathUpperRight = string.Empty;
        }

        public NekoControlDPad8ViewModel(NekoControlDPad8ViewModel other)
            : base(other)
        {
            string name = other.Name;
            do
            {
                name += "_copy";
            } while (VariableNames.Contains(name));
            Name = name;
            mBitmapPathLowerLeft = other.mBitmapPathLowerLeft;
            mBitmapPathLowerRight = other.mBitmapPathLowerRight;
            mBitmapPathUpperLeft = other.mBitmapPathUpperLeft;
            mBitmapPathUpperRight = other.mBitmapPathUpperRight;
        }

        public NekoControlDPad8ViewModel(JObject jObject)
            : base(jObject)
        {
            BitmapPathLowerLeft = jObject["BitmapPathLowerLeft"].ToString();
            BitmapPathLowerRight = jObject["BitmapPathLowerRight"].ToString();
            BitmapPathUpperLeft = jObject["BitmapPathUpperLeft"].ToString();
            BitmapPathUpperRight = jObject["BitmapPathUpperRight"].ToString();
        }

        public new object Clone()
        {
            return new NekoControlDPad8ViewModel(this);
        }

        public new string GetRubyScript(string controlPath)
        {
            string script =
$@"    @{mName} = NekoControl_Direction8.new({mX}, {mY}, {mZ}, {mWidth}, {mHeight}, @viewport)
    @{mName}.set_image_default(RPG::Cache.neko_control(""{GetRelativePath(mBitmapPathDefault, controlPath)}""))
    @{mName}.set_image_lower_left(RPG::Cache.neko_control(""{GetRelativePath(mBitmapPathLowerLeft, controlPath)}""))
    @{mName}.set_image_down(RPG::Cache.neko_control(""{GetRelativePath(mBitmapPathDown, controlPath)}""))
    @{mName}.set_image_lower_right(RPG::Cache.neko_control(""{GetRelativePath(mBitmapPathLowerRight, controlPath)}""))
    @{mName}.set_image_left(RPG::Cache.neko_control(""{GetRelativePath(mBitmapPathLeft, controlPath)}""))
    @{mName}.set_image_right(RPG::Cache.neko_control(""{GetRelativePath(mBitmapPathRight, controlPath)}""))
    @{mName}.set_image_upper_left(RPG::Cache.neko_control(""{GetRelativePath(mBitmapPathUpperLeft, controlPath)}""))
    @{mName}.set_image_up(RPG::Cache.neko_control(""{GetRelativePath(mBitmapPathUp, controlPath)}""))
    @{mName}.set_image_upper_right(RPG::Cache.neko_control(""{GetRelativePath(mBitmapPathUpperRight, controlPath)}""))
    @{mName}.set_image_stick(RPG::Cache.neko_control(""{GetRelativePath(mBitmapPathStick, controlPath)}""))
";
            script += $"    @{mName}.stick_movable_radius = {StickMovableRadius}" + '\n';
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
    }
}
