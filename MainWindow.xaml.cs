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

        private void xGridRender_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            xViewModelMain.SelectedNekoControlOrNull = null;
        }

        private void xButtonCreateNekoControlDPad4_Click(object sender, RoutedEventArgs e)
        {
            var dPad4 = new NekoControlDPad4ViewModel();
            xViewModelMain.NekoControls.Add(dPad4);
            xViewModelMain.SelectedNekoControlOrNull = dPad4;
        }

        private void xButtonCreateNekoControlDPad8_Click(object sender, RoutedEventArgs e)
        {
            var dPad8 = new NekoControlDPad8ViewModel();
            xViewModelMain.NekoControls.Add(dPad8);
            xViewModelMain.SelectedNekoControlOrNull = dPad8;
        }

        private void xButtonCreateNekoControlKeyButton_Click(object sender, RoutedEventArgs e)
        {
            var keyButton = new NekoControlKeyButtonViewModel();
            xViewModelMain.NekoControls.Add(keyButton);
            xViewModelMain.SelectedNekoControlOrNull = keyButton;
        }

        private void xButtonResizeResolution_Click(object sender, RoutedEventArgs e)
        {
            xGrid3x3.ColumnDefinitions[1].Width = new GridLength(800, GridUnitType.Pixel);
            xGrid3x3.RowDefinitions[1].Height = new GridLength(600, GridUnitType.Pixel);
        }

        private void xButtonCloneControl_Click(object sender, RoutedEventArgs e)
        {
            var control = xViewModelMain.SelectedNekoControlOrNull;
            if (control == null)
            {
                return;
            }
            if (control is NekoControlDPad8ViewModel)
            {
                var dPad8 = (NekoControlDPad8ViewModel)control;
                var clone = (NekoControlViewModel)dPad8.Clone();
                xViewModelMain.NekoControls.Add(clone);
                xViewModelMain.SelectedNekoControlOrNull = clone;
                xListBoxNekoControls.ScrollIntoView(clone);
            }
            else if (control is NekoControlDPad4ViewModel)
            {
                var dPad4 = (NekoControlDPad4ViewModel)control;
                var clone = (NekoControlViewModel)dPad4.Clone();
                xViewModelMain.NekoControls.Add(clone);
                xViewModelMain.SelectedNekoControlOrNull = clone;
                xListBoxNekoControls.ScrollIntoView(clone);
            }
            else if (control is NekoControlKeyButtonViewModel)
            {

            }
            else
            {
                Debug.Fail("Invalid Control Type");
            }
        }

        private void xButtonRemoveControl_Click(object sender, RoutedEventArgs e)
        {
            var control = xViewModelMain.SelectedNekoControlOrNull;
            if (control != null)
            {
                NekoControlViewModel.VariableNames.Remove(control.Name);
                xViewModelMain.NekoControls.Remove(control);
            }
        }

        private void xThumbDPad4_DragStarted(object sender, DragStartedEventArgs e)
        {
            Thumb thumb = (Thumb)sender;
            NekoControlViewModel control = (NekoControlViewModel)thumb.DataContext;
            xViewModelMain.SelectedNekoControlOrNull = control;
        }

        private void xThumbDPad4_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Thumb thumb = (Thumb)sender;
            NekoControlViewModel control = (NekoControlViewModel)thumb.DataContext;
            control.X += (int)e.HorizontalChange;
            control.Y += (int)e.VerticalChange;
        }

        private void xThumbDPad4_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            xWpfPropertyGrid.Refresh();
        }

        private void xThumbDPad4_MouseMove(object sender, MouseEventArgs e)
        {
            updateDPad4(xViewModelMain.SelectedNekoControlOrNull as NekoControlDPad4ViewModel, sender, e);
        }

        private void xThumbDPad4_MouseLeave(object sender, MouseEventArgs e)
        {
            Thumb thumb = (Thumb)sender;
            NekoControlDPad4ViewModel dPad4 = xViewModelMain.SelectedNekoControlOrNull as NekoControlDPad4ViewModel;
            if (dPad4 != null)
            {
                Point point = e.GetPosition(thumb);
                BitmapImage bitmapImage = NekoControlDPad4ViewModel.DefaultBitmapImage[(int)EDPadType.Default];
                Image controlImage = (Image)thumb.Template.FindName("xImageControl", thumb);
                controlImage.Source = bitmapImage;
                Image stickImage = (Image)thumb.Template.FindName("xImageStick", thumb);
                Canvas.SetLeft(stickImage, 0);
                Canvas.SetTop(stickImage, 0);
            }
        }

        private void xTextBlockRemoveControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            NekoControlViewModel control = (NekoControlViewModel)textBlock.DataContext;
            NekoControlViewModel.VariableNames.Remove(control.Name);
            xViewModelMain.NekoControls.Remove(control);
        }

        private void updateDPad4(NekoControlDPad4ViewModel dPad4, object sender, MouseEventArgs e)
        {
            if (dPad4 == null)
            {
                return;
            }
            Thumb thumb = (Thumb)sender;
            Image controlImage = (Image)thumb.Template.FindName("xImageControl", thumb);
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
            Image stickImage = (Image)thumb.Template.FindName("xImageStick", thumb);
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

        private void xThumbDPad8_DragStarted(object sender, DragStartedEventArgs e)
        {
            Thumb thumb = (Thumb)sender;
            NekoControlViewModel control = (NekoControlViewModel)thumb.DataContext;
            xViewModelMain.SelectedNekoControlOrNull = control;
        }

        private void xThumbDPad8_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Thumb thumb = (Thumb)sender;
            NekoControlViewModel control = (NekoControlViewModel)thumb.DataContext;
            control.X += (int)e.HorizontalChange;
            control.Y += (int)e.VerticalChange;
        }

        private void xThumbDPad8_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            xWpfPropertyGrid.Refresh();
        }

        private void xThumbDPad8_MouseMove(object sender, MouseEventArgs e)
        {
            updateDPad8(xViewModelMain.SelectedNekoControlOrNull as NekoControlDPad8ViewModel, sender, e);
        }

        private void xThumbDPad8_MouseLeave(object sender, MouseEventArgs e)
        {
            Thumb thumb = (Thumb)sender;
            NekoControlDPad8ViewModel dPad4 = xViewModelMain.SelectedNekoControlOrNull as NekoControlDPad8ViewModel;
            if (dPad4 != null)
            {
                Point point = e.GetPosition(thumb);
                BitmapImage bitmapImage = NekoControlDPad8ViewModel.DefaultBitmapImage[(int)EDPadType.Default];
                Image controlImage = (Image)thumb.Template.FindName("xImageControl", thumb);
                controlImage.Source = bitmapImage;
                Image stickImage = (Image)thumb.Template.FindName("xImageStick", thumb);
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
            Image controlImage = (Image)thumb.Template.FindName("xImageControl", thumb);
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
            Image stickImage = (Image)thumb.Template.FindName("xImageStick", thumb);
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