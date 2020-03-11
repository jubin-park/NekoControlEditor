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
            NekoControlViewModel clone = null;
            if (control is NekoControlDPad8ViewModel)
            {
                var dPad8 = (NekoControlDPad8ViewModel)control;
                clone = (NekoControlViewModel)dPad8.Clone();
            }
            else if (control is NekoControlDPad4ViewModel)
            {
                var dPad4 = (NekoControlDPad4ViewModel)control;
                clone = (NekoControlViewModel)dPad4.Clone();
            }
            else if (control is NekoControlKeyButtonViewModel)
            {
                var keyButton = (NekoControlKeyButtonViewModel)control;
                clone = (NekoControlViewModel)keyButton.Clone();
            }
            else
            {
                Debug.Fail("Invalid Control Type");
            }
            xViewModelMain.NekoControls.Add(clone);
            xViewModelMain.SelectedNekoControlOrNull = clone;
            xListBoxNekoControls.ScrollIntoView(clone);
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

        private void xThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            var xThumb = (Thumb)sender;
            var control = (NekoControlViewModel)xThumb.DataContext;
            xViewModelMain.SelectedNekoControlOrNull = control;
        }

        private void xThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var xThumb = (Thumb)sender;
            var control = (NekoControlViewModel)xThumb.DataContext;
            control.X += (int)e.HorizontalChange;
            control.Y += (int)e.VerticalChange;
        }

        private void xThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            xWpfPropertyGrid.Refresh();
        }

        private void xThumbDPad4_MouseMove(object sender, MouseEventArgs e)
        {
            var xThumb = (Thumb)sender;
            var control = (NekoControlViewModel)xThumb.DataContext;
            var dPad4 = control as NekoControlDPad4ViewModel;
            if (dPad4 != null && dPad4 == xViewModelMain.SelectedNekoControlOrNull)
            {
                updateDPad4(dPad4, sender, e);
                updateDPad4Stick(dPad4, sender, e);
            }
        }

        private void xThumbDPad4_MouseLeave(object sender, MouseEventArgs e)
        {
            var xThumb = (Thumb)sender;
            var control = (NekoControlViewModel)xThumb.DataContext;
            var dPad4 = control as NekoControlDPad4ViewModel;
            if (dPad4 != null)
            {
                // bitmap
                var bitmapImage = NekoControlDPad4ViewModel.DefaultBitmapImage[(int)EDPadType.Default];
                // control
                var xImageControl = (Image)xThumb.Template.FindName("xImageControl", xThumb);
                xImageControl.Source = bitmapImage;
                // stick
                var xImageStick = (Image)xThumb.Template.FindName("xImageStick", xThumb);
                Canvas.SetLeft(xImageStick, 0);
                Canvas.SetTop(xImageStick, 0);
            }
        }

        private void xTextBlockRemoveControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var textBlock = (TextBlock)sender;
            var control = (NekoControlViewModel)textBlock.DataContext;
            NekoControlViewModel.VariableNames.Remove(control.Name);
            xViewModelMain.NekoControls.Remove(control);
        }

        private void updateDPad4(NekoControlDPad4ViewModel dPad4, object sender, MouseEventArgs e)
        {
            var xThumb = (Thumb)sender;
            var xImageControl = (Image)xThumb.Template.FindName("xImageControl", xThumb);
            var point = e.GetPosition(xThumb);
            var deltaPoint = new Point(dPad4.Width / 2 - point.X, dPad4.Height / 2 - point.Y);
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
            xImageControl.Source = bitmapImage;
        }

        private void updateDPad4Stick(NekoControlDPad4ViewModel dPad4, object sender, MouseEventArgs e)
        {
            var xThumb = (Thumb)sender;
            var point = e.GetPosition(xThumb);
            var deltaPoint = new Point(dPad4.Width / 2 - point.X, dPad4.Height / 2 - point.Y);
            double TwoPowerdist = deltaPoint.X * deltaPoint.X + deltaPoint.Y * deltaPoint.Y;
            double radius = dPad4.StickMovableRadius;
            var xImageStick = (Image)xThumb.Template.FindName("xImageStick", xThumb);
            if (TwoPowerdist < radius * radius)
            {
                Canvas.SetLeft(xImageStick, point.X - dPad4.Width / 2);
                Canvas.SetTop(xImageStick, point.Y - dPad4.Height / 2);
            }
            else
            {
                double angle = Math.Atan2(deltaPoint.Y, deltaPoint.X) + Math.PI;
                Canvas.SetLeft(xImageStick, radius * Math.Cos(angle));
                Canvas.SetTop(xImageStick, radius * Math.Sin(angle));
            }
        }

        private void xThumbDPad8_MouseMove(object sender, MouseEventArgs e)
        {
            var xThumb = (Thumb)sender;
            var control = (NekoControlViewModel)xThumb.DataContext;
            var dPad8 = control as NekoControlDPad8ViewModel;
            if (dPad8 != null && dPad8 == xViewModelMain.SelectedNekoControlOrNull)
            {
                updateDPad8(dPad8, sender, e);
                updateDPad8Stick(dPad8, sender, e);
            }
        }

        private void xThumbDPad8_MouseLeave(object sender, MouseEventArgs e)
        {
            var xThumb = (Thumb)sender;
            var control = (NekoControlViewModel)xThumb.DataContext;
            var dPad8 = control as NekoControlDPad8ViewModel;
            if (dPad8 != null)
            {
                // bitmap
                var bitmapImage = NekoControlDPad8ViewModel.DefaultBitmapImage[(int)EDPadType.Default];
                // control
                var xImageControl = (Image)xThumb.Template.FindName("xImageControl", xThumb);
                xImageControl.Source = bitmapImage;
                // stick
                var xImageStick = (Image)xThumb.Template.FindName("xImageStick", xThumb);
                Canvas.SetLeft(xImageStick, 0);
                Canvas.SetTop(xImageStick, 0);
            }
        }

        private void updateDPad8(NekoControlDPad8ViewModel dPad8, object sender, MouseEventArgs e)
        {
            var xThumb = (Thumb)sender;
            var xImageControl = (Image)xThumb.Template.FindName("xImageControl", xThumb);
            var point = e.GetPosition(xThumb);
            var deltaPoint = new Point(dPad8.Width / 2 - point.X, dPad8.Height / 2 - point.Y);
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
            xImageControl.Source = bitmapImage;
        }

        private void updateDPad8Stick(NekoControlDPad8ViewModel dPad8, object sender, MouseEventArgs e)
        {
            var xThumb = (Thumb)sender;
            var point = e.GetPosition(xThumb);
            var deltaPoint = new Point(dPad8.Width / 2 - point.X, dPad8.Height / 2 - point.Y);
            double TwoPowerdist = deltaPoint.X * deltaPoint.X + deltaPoint.Y * deltaPoint.Y;
            double radius = dPad8.StickMovableRadius;
            var xImageStick = (Image)xThumb.Template.FindName("xImageStick", xThumb);
            if (TwoPowerdist < radius * radius)
            {
                Canvas.SetLeft(xImageStick, point.X - dPad8.Width / 2);
                Canvas.SetTop(xImageStick, point.Y - dPad8.Height / 2);
            }
            else
            {
                double angle = Math.Atan2(deltaPoint.Y, deltaPoint.X) + Math.PI;
                Canvas.SetLeft(xImageStick, radius * Math.Cos(angle));
                Canvas.SetTop(xImageStick, radius * Math.Sin(angle));
            }
        }

        private void xThumbKeyButton_MouseMove(object sender, MouseEventArgs e)
        {
            var xThumb = (Thumb)sender;
            var control = (NekoControlViewModel)xThumb.DataContext;
            var keyButton = control as NekoControlKeyButtonViewModel;
            if (keyButton != null && keyButton == xViewModelMain.SelectedNekoControlOrNull)
            {
                updateKeyButton(keyButton, sender, e);
            }
        }

        private void xThumbKeyButton_MouseLeave(object sender, MouseEventArgs e)
        {
            var xThumb = (Thumb)sender;
            var control = (NekoControlViewModel)xThumb.DataContext;
            var keyButton = control as NekoControlKeyButtonViewModel;
            if (keyButton != null)
            {
                var bitmapImage = NekoControlKeyButtonViewModel.DefaultBitmapImage[0];
                var xImageControl = (Image)xThumb.Template.FindName("xImageControl", xThumb);
                xImageControl.Source = bitmapImage;
            }
        }

        private void updateKeyButton(NekoControlKeyButtonViewModel dPad4, object sender, MouseEventArgs e)
        {
            var xThumb = (Thumb)sender;
            var xImageControl = (Image)xThumb.Template.FindName("xImageControl", xThumb);
            BitmapImage bitmapImage = NekoControlKeyButtonViewModel.DefaultBitmapImage[1]; ;
            xImageControl.Source = bitmapImage;
        }
    }
}