using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NekoControlEditor
{
    /// <summary>
    /// CheckerBoard.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CheckerBoard : UserControl, INotifyPropertyChanged
    {
        public CheckerBoard()
        {
            InitializeComponent();
            DataContext = this;
        }

        public double mBorderWidth;
        public double BorderWidth
        {
            get
            {
                return mBorderWidth + 2;
            }
            set
            {
                if (mBorderWidth != value)
                {
                    mBorderWidth = value;
                    notifyPropertyChanged("BorderWidth");
                }
            }
        }

        public double mBorderHeight;
        public double BorderHeight
        {
            get
            {
                return mBorderHeight + 2;
            }
            set
            {
                if (mBorderHeight != value)
                {
                    mBorderHeight = value;
                    notifyPropertyChanged("BorderHeight");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            BorderWidth = ActualWidth;
            BorderHeight = ActualHeight;
        }

        private void notifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
