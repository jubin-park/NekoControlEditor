using System;
using System.Collections.Generic;
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
    /// ControlDirection4.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ControlDirection4 : UserControl
    {
        private uint mWidth;
        private uint mHeight;
        private Point mStartPoint;
        private bool mbMouseDown;

        public ControlDirection4()
        {
            InitializeComponent();
            BitmapImage bitmapImage = new BitmapImage(new Uri("image/dpad_none.png", UriKind.Relative));
            ControlImage.Source = bitmapImage;
            mWidth = (uint)bitmapImage.Width;
            mHeight = (uint)bitmapImage.Height;
            resize_control();
            mbMouseDown = false;
        }

        private void resize_control()
        {
            MainCanvas.Width = mWidth;
            MainCanvas.Height = mHeight;
            ControlImage.Width = mWidth;
            ControlImage.Height = mHeight;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                mStartPoint = e.GetPosition(null);
            }
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point ePoint = e.GetPosition(null);
                Point newPoint = this.PointToScreen(new Point(0, 0));
                double x = ePoint.X - mStartPoint.X;
                double y = ePoint.Y - mStartPoint.Y;
            }
        }

        private void MyGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mbMouseDown = false;
            Mouse.Capture(null);
        }
    }
}
