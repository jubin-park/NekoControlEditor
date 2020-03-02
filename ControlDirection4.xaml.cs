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
        private Point mStartPoint;
        private Point mLastPoint;
        private bool mbMouseDown;

        public PropControlDirection4 Properties { get; set; }

        public ControlDirection4()
        {
            InitializeComponent();
            Properties = new PropControlDirection4();
            Properties.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Control_PropertyChanged);
            BitmapImage bitmapImage = new BitmapImage(new Uri("image/dpad_none.png", UriKind.Relative));
            ControlImage.Source = bitmapImage;
            Properties.Width = (uint)bitmapImage.PixelWidth;
            Properties.Height = (uint)bitmapImage.PixelHeight;
            resizeControl();
            mbMouseDown = false;
        }

        private void resizeControl()
        {
            MainCanvas.Width = Properties.Width;
            MainCanvas.Height = Properties.Height;
            ControlImage.Width = Properties.Width;
            ControlImage.Height = Properties.Height;
        }

        private void MainCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mbMouseDown = true;
            var draggableControl = (sender as Canvas);
            mStartPoint = e.GetPosition(this.Parent as UIElement);
            draggableControl.CaptureMouse();
        }

        private void MainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            var draggableControl = (sender as Canvas);
            if (mbMouseDown && draggableControl != null)
            {
                Point currentPosition = e.GetPosition(this.Parent as UIElement);
                var transform = (draggableControl.RenderTransform as TranslateTransform);
                if (transform == null)
                {
                    transform = new TranslateTransform();
                    draggableControl.RenderTransform = transform;
                }
                transform.X = currentPosition.X - mStartPoint.X;
                transform.Y = currentPosition.Y - mStartPoint.Y;
                if (mLastPoint.X > 0)
                {
                    transform.X += mLastPoint.X;
                    transform.Y += mLastPoint.Y;
                }
            }
        }

        private void MainCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mbMouseDown = false;
            var draggable = (sender as Canvas);
            var transform = (draggable.RenderTransform as TranslateTransform);
            if (transform != null)
            {
                mLastPoint = new Point(transform.X, transform.Y);
                Properties.X = (int)transform.X;
                Properties.Y = (int)transform.Y;
            }
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.PropertyGrid.SelectedObject = this.Properties;
            mainWindow.PropertyGrid.RefreshPropertyList();
            draggable.ReleaseMouseCapture();
        }

        private void RemoveTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.RenderCanvas.Children.Remove(this);
            mainWindow.PropertyGrid.SelectedObject = null;
        }

        void Control_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.PropertyGrid.RefreshPropertyList();
        }
    }
}
