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

        private static uint mCount = 0;

        public NekoControlKeyButtonViewModel()
        {
            string name = "$key_";
            while (VariableNames.Contains(name + mCount))
            {
                ++mCount;
            }
            Name = name + mCount;
            Key = EKeys.EMPTY;
            Width = 32;
            Height = 32;
            BitmapDefault = "image/UltimateDroidButton1.png";
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
            Key = other.Key;
            Width = other.Width;
            Height = other.Height;
            BitmapDefault = other.BitmapDefault;
        }

        public object Clone()
        {
            return new NekoControlKeyButtonViewModel(this);
        }
    }
}
