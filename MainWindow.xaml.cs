using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainViewModel.SelectedNekoControlOrNull = null;
        }

        private void CreateNekoControlDPad4Button_Click(object sender, RoutedEventArgs e)
        {
            var dPad4 = new NekoControlDPad4ViewModel();
            MainViewModel.NekoControls.Add(dPad4);
            MainViewModel.SelectedNekoControlOrNull = dPad4;
        }

        private void CreateNekoControlDPad8Button_Click(object sender, RoutedEventArgs e)
        {
            var dPad8 = new NekoControlDPad8ViewModel();
            MainViewModel.NekoControls.Add(dPad8);
            MainViewModel.SelectedNekoControlOrNull = dPad8;
        }

        private void CreateNekoControlKeyButtonButton_Click(object sender, RoutedEventArgs e)
        {
            var keyButton = new NekoControlKeyButtonViewModel();
            MainViewModel.NekoControls.Add(keyButton);
            MainViewModel.SelectedNekoControlOrNull = keyButton;
        }

        private void ResolutionButton_Click(object sender, RoutedEventArgs e)
        {
            RenderGrid.ColumnDefinitions[1].Width = new GridLength(600, GridUnitType.Pixel);
            RenderGrid.RowDefinitions[1].Height = new GridLength(400, GridUnitType.Pixel);
        }

        private void xButtonCloneControl_Click(object sender, RoutedEventArgs e)
        {

        }

        private void xButtonRemoveControl_Click(object sender, RoutedEventArgs e)
        {
            var select = MainViewModel.SelectedNekoControlOrNull;
            if (select != null)
            {
                MainViewModel.NekoControls.Remove(select);
            }
        }

        private void DPad4Thumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            Thumb thumb = (Thumb)sender;
            NekoControlViewModel nekoControlViewModel = (NekoControlViewModel)thumb.DataContext;
            MainViewModel.SelectedNekoControlOrNull = nekoControlViewModel;
        }

        private void DPad4Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Thumb thumb = (Thumb)sender;
            NekoControlViewModel nekoControlViewModel = (NekoControlViewModel)thumb.DataContext;
            nekoControlViewModel.X += (int)e.HorizontalChange;
            nekoControlViewModel.Y += (int)e.VerticalChange;
        }

        private void DPad4Thumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            PropertyGrid.Refresh();
        }

        private void DPad4Thumb_MouseMove(object sender, MouseEventArgs e)
        {
            updateDPad4(MainViewModel.SelectedNekoControlOrNull as NekoControlDPad4ViewModel, sender, e);
        }

        private void DPad4Thumb_MouseLeave(object sender, MouseEventArgs e)
        {
            Thumb thumb = (Thumb)sender;
            NekoControlDPad4ViewModel dPad4 = MainViewModel.SelectedNekoControlOrNull as NekoControlDPad4ViewModel;
            if (dPad4 != null)
            {
                Point point = e.GetPosition(thumb);
                BitmapImage bitmapImage = NekoControlDPad4ViewModel.DefaultBitmapImage[(int)EDPadType.Default];
                Image controlImage = (Image)thumb.Template.FindName("ControlImage", thumb);
                controlImage.Source = bitmapImage;
                Image stickImage = (Image)thumb.Template.FindName("StickImage", thumb);
                Canvas.SetLeft(stickImage, 0);
                Canvas.SetTop(stickImage, 0);
            }
        }

        private void RemoveControlTextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            NekoControlViewModel nekoControlViewModel = (NekoControlViewModel)textBlock.DataContext;
            int index = MainViewModel.NekoControls.IndexOf(nekoControlViewModel) + 1;
            if (index > 0 && index < MainViewModel.NekoControls.Count)
            {
                MainViewModel.SelectedNekoControlOrNull = MainViewModel.NekoControls[index];
            }
            MainViewModel.NekoControls.Remove(nekoControlViewModel);
        }

        private void updateDPad4(NekoControlDPad4ViewModel dPad4, object sender, MouseEventArgs e)
        {
            if (dPad4 == null)
            {
                return;
            }
            Thumb thumb = (Thumb)sender;
            Image controlImage = (Image)thumb.Template.FindName("ControlImage", thumb);
            Point point = e.GetPosition(thumb);
            Point deltaPoint = new Point(dPad4.Width / 2 - point.X, dPad4.Height / 2 - point.Y);
            double angle = Math.Atan2(deltaPoint.Y, deltaPoint.X) * (180 / Math.PI);
            BitmapImage bitmapImage = null;
            if (angle >= -135 && angle < -45)
            {
                bitmapImage = NekoControlDPad4ViewModel.DefaultBitmapImage[(int)EDPadType.Down];
            }
            else if (angle >= -45 && angle < 45)
            {
                bitmapImage = NekoControlDPad4ViewModel.DefaultBitmapImage[(int)EDPadType.Left];
            }
            else if (angle >= 45 && angle < 135)
            {
                bitmapImage = NekoControlDPad4ViewModel.DefaultBitmapImage[(int)EDPadType.Up];
            }
            else// if ((angle >= -180 && angle < -135) || (angle >= 135 && angle < 180))
            {
                bitmapImage = NekoControlDPad4ViewModel.DefaultBitmapImage[(int)EDPadType.Right];
            }
            controlImage.Source = bitmapImage;
        }

        private void updateDPad4Stick(NekoControlDPad4ViewModel dPad4, object sender, MouseEventArgs e)
        {
            if (dPad4 == null)
            {
                return;
            }
            Thumb thumb = (Thumb)sender;
            Point point = e.GetPosition(thumb);
            Point deltaPoint = new Point(dPad4.Width / 2 - point.X, dPad4.Height / 2 - point.Y);
            double TwoPowerdist = deltaPoint.X * deltaPoint.X + deltaPoint.Y * deltaPoint.Y;
            double radius = dPad4.StickMovableRadius;
            Image stickImage = (Image)thumb.Template.FindName("StickImage", thumb);
            if (TwoPowerdist < radius * radius)
            {
                Canvas.SetLeft(stickImage, point.X - dPad4.Width / 2);
                Canvas.SetTop(stickImage, point.Y - dPad4.Height / 2);
            }
            else
            {
                double angle = Math.Atan2(deltaPoint.Y, deltaPoint.X) + Math.PI;
                Canvas.SetLeft(stickImage, radius * Math.Cos(angle));
                Canvas.SetTop(stickImage, radius * Math.Sin(angle));
            }
        }

        private void DPad8Thumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            Thumb thumb = (Thumb)sender;
            NekoControlViewModel nekoControlViewModel = (NekoControlViewModel)thumb.DataContext;
            MainViewModel.SelectedNekoControlOrNull = nekoControlViewModel;
        }

        private void DPad8Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Thumb thumb = (Thumb)sender;
            NekoControlViewModel nekoControlViewModel = (NekoControlViewModel)thumb.DataContext;
            nekoControlViewModel.X += (int)e.HorizontalChange;
            nekoControlViewModel.Y += (int)e.VerticalChange;
        }

        private void DPad8Thumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            PropertyGrid.Refresh();
        }

        private void DPad8Thumb_MouseMove(object sender, MouseEventArgs e)
        {
            updateDPad8(MainViewModel.SelectedNekoControlOrNull as NekoControlDPad8ViewModel, sender, e);
        }

        private void DPad8Thumb_MouseLeave(object sender, MouseEventArgs e)
        {
            Thumb thumb = (Thumb)sender;
            NekoControlDPad8ViewModel dPad4 = MainViewModel.SelectedNekoControlOrNull as NekoControlDPad8ViewModel;
            if (dPad4 != null)
            {
                Point point = e.GetPosition(thumb);
                BitmapImage bitmapImage = NekoControlDPad8ViewModel.DefaultBitmapImage[(int)EDPadType.Default];
                Image controlImage = (Image)thumb.Template.FindName("ControlImage", thumb);
                controlImage.Source = bitmapImage;
                Image stickImage = (Image)thumb.Template.FindName("StickImage", thumb);
                Canvas.SetLeft(stickImage, 0);
                Canvas.SetTop(stickImage, 0);
            }
        }

        private void updateDPad8(NekoControlDPad8ViewModel dPad8, object sender, MouseEventArgs e)
        {
            if (dPad8 == null)
            {
                return;
            }
            Thumb thumb = (Thumb)sender;
            Image controlImage = (Image)thumb.Template.FindName("ControlImage", thumb);
            Point point = e.GetPosition(thumb);
            Point deltaPoint = new Point(dPad8.Width / 2 - point.X, dPad8.Height / 2 - point.Y);
            double angle = Math.Atan2(deltaPoint.Y, deltaPoint.X) * (180 / Math.PI);
            BitmapImage bitmapImage = null;
            if (angle >= -67.5 && angle < -22.5)
            {
                bitmapImage = NekoControlDPad8ViewModel.DefaultBitmapImage[(int)EDPadType.LowerLeft];
            }
            else if (angle >= -112.5 && angle < -67.5)
            {
                bitmapImage = NekoControlDPad8ViewModel.DefaultBitmapImage[(int)EDPadType.Down];
            }
            else if (angle >= -157.5 && angle < -112.5)
            {
                bitmapImage = NekoControlDPad8ViewModel.DefaultBitmapImage[(int)EDPadType.LowerRight];
            }
            else if (angle >= -22.5 && angle < 22.5)
            {
                bitmapImage = NekoControlDPad8ViewModel.DefaultBitmapImage[(int)EDPadType.Left];
            }
            else if (angle >= 22.5 && angle < 67.5)
            {
                bitmapImage = NekoControlDPad8ViewModel.DefaultBitmapImage[(int)EDPadType.UpperLeft];
            }
            else if (angle >= 67.5 && angle < 112.5)
            {
                bitmapImage = NekoControlDPad8ViewModel.DefaultBitmapImage[(int)EDPadType.Up];
            }
            else if (angle >= 112.5 && angle < 157.5)
            {
                bitmapImage = NekoControlDPad8ViewModel.DefaultBitmapImage[(int)EDPadType.UpperRight];
            }
            else// if ((angle >= -180.0 && angle < -157.5) || (angle >= 157.5 && angle < 180.0))
            {
                bitmapImage = NekoControlDPad8ViewModel.DefaultBitmapImage[(int)EDPadType.Right];
            }
            controlImage.Source = bitmapImage;
        }

        private void updateDPad8Stick(NekoControlDPad8ViewModel dPad8, object sender, MouseEventArgs e)
        {
            if (dPad8 == null)
            {
                return;
            }
            Thumb thumb = (Thumb)sender;
            Point point = e.GetPosition(thumb);
            Point deltaPoint = new Point(dPad8.Width / 2 - point.X, dPad8.Height / 2 - point.Y);
            double TwoPowerdist = deltaPoint.X * deltaPoint.X + deltaPoint.Y * deltaPoint.Y;
            double radius = dPad8.StickMovableRadius;
            Image stickImage = (Image)thumb.Template.FindName("StickImage", thumb);
            if (TwoPowerdist < radius * radius)
            {
                Canvas.SetLeft(stickImage, point.X - dPad8.Width / 2);
                Canvas.SetTop(stickImage, point.Y - dPad8.Height / 2);
            }
            else
            {
                double angle = Math.Atan2(deltaPoint.Y, deltaPoint.X) + Math.PI;
                Canvas.SetLeft(stickImage, radius * Math.Cos(angle));
                Canvas.SetTop(stickImage, radius * Math.Sin(angle));
            }
        }
    }
}