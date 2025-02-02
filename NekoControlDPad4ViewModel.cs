﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Activities.Presentation.PropertyEditing;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Media;
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
        [JsonIgnore]
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
        [JsonIgnore]
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
        [JsonIgnore]
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
        [JsonIgnore]
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
        [JsonIgnore]
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
        [JsonIgnore]
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
                try
                {
                    BitmapImage bitmapImage = null;
                    if (value != string.Empty)
                    {
                        bitmapImage = CreateCacheBitmapImage(value);
                    }
                    mBitmapPathDefault = value;
                    mBitmapImageDefault = bitmapImage;
                }
                catch (FileNotFoundException)
                {
                    Debug.Fail(FAILED_LOAD_BITMAP_MESSAGE, value);
                    mBitmapPathDefault = string.Empty;
                    mBitmapImageDefault = null;
                }
                ImageSourceControl = BitmapImageDefault;
                notifyPropertyChanged("BitmapPathDefault");
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
                try
                {
                    BitmapImage bitmapImage = null;
                    if (value != string.Empty)
                    {
                        bitmapImage = CreateCacheBitmapImage(value);
                    }
                    mBitmapPathDown = value;
                    mBitmapImageDown = bitmapImage;
                }
                catch (FileNotFoundException)
                {
                    Debug.Fail(FAILED_LOAD_BITMAP_MESSAGE, value);
                    mBitmapPathDown = string.Empty;
                    mBitmapImageDown = null;
                }
                notifyPropertyChanged("BitmapPathDown");
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
                try
                {
                    BitmapImage bitmapImage = null;
                    if (value != string.Empty)
                    {
                        bitmapImage = CreateCacheBitmapImage(value);
                    }
                    mBitmapPathLeft = value;
                    BitmapImageLeft = bitmapImage;
                }
                catch (FileNotFoundException)
                {
                    Debug.Fail(FAILED_LOAD_BITMAP_MESSAGE, value);
                    mBitmapPathLeft = string.Empty;
                    mBitmapImageLeft = null;
                }
                notifyPropertyChanged("BitmapPathLeft");
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
                try
                {
                    BitmapImage bitmapImage = null;
                    if (value != string.Empty)
                    {
                        bitmapImage = CreateCacheBitmapImage(value);
                    }
                    mBitmapPathRight = value;
                    mBitmapImageRight = bitmapImage;
                }
                catch (FileNotFoundException)
                {
                    Debug.Fail(FAILED_LOAD_BITMAP_MESSAGE, value);
                    mBitmapPathRight = string.Empty;
                    mBitmapImageRight = null;
                }
                notifyPropertyChanged("BitmapPathRight");
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
                try
                {
                    BitmapImage bitmapImage = null;
                    if (value != string.Empty)
                    {
                        bitmapImage = CreateCacheBitmapImage(value);
                    }
                    mBitmapPathUp = value;
                    mBitmapImageUp = bitmapImage;
                }
                catch (FileNotFoundException)
                {
                    Debug.Fail(FAILED_LOAD_BITMAP_MESSAGE, value);
                    mBitmapPathUp = string.Empty;
                    mBitmapImageUp = null;
                }
                notifyPropertyChanged("BitmapPathUp");
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
                try
                {
                    BitmapImage bitmapImage = null;
                    if (value != string.Empty)
                    {
                        bitmapImage = CreateCacheBitmapImage(value);
                    }
                    mBitmapPathStick = value;
                    mBitmapImageStick = bitmapImage;
                }
                catch (FileNotFoundException)
                {
                    Debug.Fail(FAILED_LOAD_BITMAP_MESSAGE, value);
                    mBitmapPathStick = string.Empty;
                    mBitmapImageStick = null;
                }
                ImageSourceStick = BitmapImageStick;
                notifyPropertyChanged("BitmapPathStick");
            }
        }
        #endregion

        #region Extra Properties
        public new string Type
        {
            get
            {
                return "DPad4";
            }
        }

        [JsonIgnore]
        public string TypeName
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

        protected ImageSource mImageSourceStick;
        [Browsable(false)]
        [JsonIgnore]
        public ImageSource ImageSourceStick
        {
            get
            {
                return mImageSourceStick;
            }
            set
            {
                mImageSourceStick = value;
                notifyPropertyChanged("ImageSourceStick");
            }
        }
        #endregion

        private static uint mCount = 0;

        public NekoControlDPad4ViewModel()
        {
            if (this is NekoControlDPad8ViewModel == false)
            {
                string name = "dpad4_";
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
            mImageSourceControl = BitmapImageDefault; // must be property
            mImageSourceStick = BitmapImageStick; // must be property
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
            mImageSourceControl = other.mImageSourceControl;
            mImageSourceStick = other.mImageSourceStick;
        }

        public NekoControlDPad4ViewModel(JObject jObject)
            : base(jObject)
        {
            BitmapPathDefault = jObject["BitmapPathDefault"].ToString();
            BitmapPathDown = jObject["BitmapPathDown"].ToString();
            BitmapPathLeft = jObject["BitmapPathLeft"].ToString();
            BitmapPathRight = jObject["BitmapPathRight"].ToString();
            BitmapPathUp = jObject["BitmapPathUp"].ToString();
            BitmapPathStick = jObject["BitmapPathStick"].ToString();
            mStickMovableRadius = jObject["StickMovableRadius"].Value<ushort>();
            ImageSourceControl = BitmapImageDefault; // must be property
            ImageSourceStick = BitmapImageStick; // must be property
        }

        public object Clone()
        {
            return new NekoControlDPad4ViewModel(this);
        }

        public string GetRubyScript(string controlPath)
        {
            string scriptDefault = mBitmapPathDefault == string.Empty ? "nil" : $@"RPG::Cache.neko_control(""{GetRelativePath(mBitmapPathDefault, controlPath)}"")";
            string scriptDown = mBitmapPathDown == string.Empty ? "nil" : $@"RPG::Cache.neko_control(""{GetRelativePath(mBitmapPathDown, controlPath)}"")";
            string scriptLeft = mBitmapPathLeft == string.Empty ? "nil" : $@"RPG::Cache.neko_control(""{GetRelativePath(mBitmapPathLeft, controlPath)}"")";
            string scriptRight = mBitmapPathRight == string.Empty ? "nil" : $@"RPG::Cache.neko_control(""{GetRelativePath(mBitmapPathRight, controlPath)}"")";
            string scriptUp = mBitmapPathUp == string.Empty ? "nil" : $@"RPG::Cache.neko_control(""{GetRelativePath(mBitmapPathUp, controlPath)}"")";
            string scriptStick = mBitmapPathStick == string.Empty ? "nil" : $@"RPG::Cache.neko_control(""{GetRelativePath(mBitmapPathStick, controlPath)}"")";
            string script =
$@"    @{mName} = NekoControl_Direction4.new({mX}, {mY}, {mZ}, {mWidth}, {mHeight}, @viewport)
    @{mName}.set_image_default({scriptDefault})
    @{mName}.set_image_down({scriptDown})
    @{mName}.set_image_left({scriptLeft})
    @{mName}.set_image_right({scriptRight})
    @{mName}.set_image_up({scriptUp})
    @{mName}.set_image_stick({scriptStick})
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
