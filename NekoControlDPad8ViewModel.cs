using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace NekoControlEditor
{
    class NekoControlDPad8ViewModel : NekoControlDPad4ViewModel
    {
        private string mBitmapLowerLeft;
        [Category("버튼 그래픽 파일")]
        [DisplayName("왼쪽 아래 누름")]
        public string BitmapLowerLeft
        {
            get
            {
                return mBitmapLowerLeft;
            }
            set
            {
                if (mBitmapLowerLeft != value)
                {
                    mBitmapLowerLeft = value;
                    notifyPropertyChanged("BitmapLowerLeft");
                }
            }
        }

        private string mBitmapLowerRight;
        [Category("버튼 그래픽 파일")]
        [DisplayName("오른쪽 아래 누름")]
        public string BitmapLowerRight
        {
            get
            {
                return mBitmapLowerRight;
            }
            set
            {
                if (mBitmapLowerRight != value)
                {
                    mBitmapLowerRight = value;
                    notifyPropertyChanged("BitmapLowerRight");
                }
            }
        }

        private string mBitmapUpperLeft;
        [Category("버튼 그래픽 파일")]
        [DisplayName("왼쪽 위 누름")]
        public string BitmapUpperLeft
        {
            get
            {
                return mBitmapUpperLeft;
            }
            set
            {
                if (mBitmapUpperLeft != value)
                {
                    mBitmapUpperLeft = value;
                    notifyPropertyChanged("BitmapUpperLeft");
                }
            }
        }

        private string mBitmapUpperRight;
        [Category("버튼 그래픽 파일")]
        [DisplayName("오른쪽 위 누름")]
        public string BitmapUpperRight
        {
            get
            {
                return mBitmapUpperRight;
            }
            set
            {
                if (mBitmapUpperRight != value)
                {
                    mBitmapUpperRight = value;
                    notifyPropertyChanged("BitmapUpperRight");
                }
            }
        }

        private static uint mCount = 0;

        public NekoControlDPad8ViewModel()
        {
            string name = "$dpad8_";
            while (VariableNames.Contains(name + mCount))
            {
                ++mCount;
            }
            mName = name + mCount;
            mBitmapLowerLeft = "";
            mBitmapLowerRight = "";
            mBitmapUpperLeft = "";
            mBitmapUpperRight = "";
        }

        public NekoControlDPad8ViewModel(NekoControlDPad8ViewModel other)
            : base(other)
        {
            string name = other.Name;
            do
            {
                name += "_copy";
            } while (VariableNames.Contains(name));
            mName = name;
            mBitmapLowerLeft = other.mBitmapLowerLeft;
            mBitmapLowerRight = other.mBitmapLowerRight;
            mBitmapUpperLeft = other.mBitmapUpperLeft;
            mBitmapUpperRight = other.mBitmapUpperRight;
        }

        public new object Clone()
        {
            return new NekoControlDPad8ViewModel(this);
        }
    }
}
