using System;
using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace NekoControlEditor
{
    public class NekoControlKeyButtonViewModel : NekoControlViewModel
    {
        public static readonly BitmapImage[] DefaultBitmapImage =
        {
            new BitmapImage(new Uri("image/UltimateDroidButton1.png", UriKind.Relative)),
            new BitmapImage(new Uri("image/UltimateDroidButton1Pressed.png", UriKind.Relative)),
        };

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

        private string mBitmapPressed;
        [Category("버튼 그래픽 파일")]
        [DisplayName("누름")]
        public string BitmapPressed
        {
            get
            {
                return mBitmapPressed;
            }
            set
            {
                if (mBitmapPressed != value)
                {
                    mBitmapPressed = value;
                    notifyPropertyChanged("BitmapPressed");
                }
            }
        }

        private static uint mCount = 0;

        public NekoControlKeyButtonViewModel()
        {
            string name = "$key_";
            while (VariableNames.Contains(name + mCount))
            {
                ++mCount;
            }
            mName = name + mCount;
            mKey = EKeys.EMPTY;
            mWidth = 32;
            mHeight = 32;
            mBitmapDefault = "image/UltimateDroidButton1.png";
            mBitmapPressed = "image/UltimateDroidButton1Pressed.png";
        }

        public NekoControlKeyButtonViewModel(NekoControlKeyButtonViewModel other)
            : base(other)
        {
            string name = other.Name;
            do
            {
                name += "_copy";
            } while (VariableNames.Contains(name));
            mName = name;
            mKey = other.mKey;
            mWidth = other.mWidth;
            mHeight = other.mHeight;
            mBitmapDefault = other.mBitmapDefault;
        }

        public object Clone()
        {
            return new NekoControlKeyButtonViewModel(this);
        }
    }
}
