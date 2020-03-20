using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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
using Path = System.IO.Path;

namespace NekoControlEditor
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public static string WorkSpacePath = AppDomain.CurrentDomain.BaseDirectory;
        public static OpenFileDialog LoadPictureFileDialog = new OpenFileDialog();
        public event PropertyChangedEventHandler PropertyChanged;

        private static string NEKO_CONTROLS_PATH = "\\Graphics\\Nekocontrols\\";
        private static string FAILED_LOAD_CONTROL_MESSAGE = "Invalid Control Type";
        private static OpenFileDialog mOpenControlFileDialog = new OpenFileDialog();
        private static SaveFileDialog mSaveControlFileDialog = new SaveFileDialog();
        private static SaveFileDialog mSaveScriptFileDialog = new SaveFileDialog();

        public string mWorkFilePath = string.Empty;
        public string WorkFilePath
        {
            get
            {
                return mWorkFilePath;
            }
            set
            {
                if (mWorkFilePath != value)
                {
                    mWorkFilePath = value;
                    notifyPropertyChanged("WorkFilePath");
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            mOpenControlFileDialog.Title = "열기";
            mOpenControlFileDialog.Filter = "Neko Controls 파일 (*.nkcs)|*.nkcs";
            LoadPictureFileDialog.InitialDirectory = WorkSpacePath;

            mSaveControlFileDialog.Title = "저장";
            mSaveControlFileDialog.Filter = "Neko Controls 파일 (*.nkcs)|*.nkcs";
            mSaveControlFileDialog.InitialDirectory = WorkSpacePath;

            mSaveScriptFileDialog.Title = "스크립트로 저장";
            mSaveScriptFileDialog.Filter = "txt 파일 (*.txt)|*.txt";
            mSaveScriptFileDialog.InitialDirectory = WorkSpacePath;

            LoadPictureFileDialog.Title = "사진 선택";
            LoadPictureFileDialog.Filter = "그림 파일 (*.jpg, *.png)|*.jpg;*.png";
            LoadPictureFileDialog.DefaultExt = ".png";
            LoadPictureFileDialog.InitialDirectory = WorkSpacePath;
            if (Directory.Exists(WorkSpacePath + NEKO_CONTROLS_PATH))
            {
                LoadPictureFileDialog.InitialDirectory += NEKO_CONTROLS_PATH;
            }
        }

        #region Main Menu Events
        private void xButtonNew_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("기존 작업 파일이 사라집니다. 계속하시겠습니까?", "새로 만들기", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                xViewModelMain.NekoControls.Clear();
                NekoControlViewModel.VariableNames.Clear();
                xCheckerBoard.BackgroundColor = "Transparent";
                try
                {
                    mOpenControlFileDialog.InitialDirectory = Path.GetDirectoryName(WorkFilePath);
                    mSaveControlFileDialog.InitialDirectory = Path.GetDirectoryName(WorkFilePath);
                    mSaveScriptFileDialog.InitialDirectory = Path.GetDirectoryName(WorkFilePath);
                }
                catch (ArgumentException)
                {
                    mOpenControlFileDialog.InitialDirectory = string.Empty;
                    mSaveControlFileDialog.InitialDirectory = string.Empty;
                    mSaveScriptFileDialog.InitialDirectory = string.Empty;
                }
                LoadPictureFileDialog.InitialDirectory = WorkSpacePath;
                if (Directory.Exists(WorkSpacePath + NEKO_CONTROLS_PATH))
                {
                    LoadPictureFileDialog.InitialDirectory += NEKO_CONTROLS_PATH;
                }
                WorkFilePath = string.Empty;
            }
        }

        private void xButtonLoad_Click(object sender, RoutedEventArgs e)
        {
            mOpenControlFileDialog.FileName = System.IO.Path.GetFileNameWithoutExtension(WorkFilePath);
            if (mOpenControlFileDialog.ShowDialog() == false)
            {
                return;
            }
            if (MessageBoxResult.Yes != MessageBox.Show("기존 작업 파일이 사라집니다. 계속하시겠습니까?", "새로 만들기", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                return;
            }
            // 컨트롤 리스트 초기화
            xViewModelMain.NekoControls.Clear();
            NekoControlViewModel.VariableNames.Clear();
            // json 로드
            string json = File.ReadAllText(mOpenControlFileDialog.FileName);
            JObject jObjectMain = JObject.Parse(json);
            JObject jObjectConfig = JsonConvert.DeserializeObject<JObject>(jObjectMain["Config"].ToString());
            // config 적용
            xGrid3x3.ColumnDefinitions[2].Width = new GridLength(jObjectConfig["Width"].Value<uint>(), GridUnitType.Pixel);
            xGrid3x3.RowDefinitions[2].Height = new GridLength(jObjectConfig["Height"].Value<uint>(), GridUnitType.Pixel);
            xCheckerBoard.BackgroundColor = jObjectConfig["BackgroundColor"].ToString();
            // workspace 경로 적용
            WorkSpacePath = jObjectConfig["WorkSpacePath"].ToString();
            if (!Directory.Exists(WorkSpacePath))
            {
                MessageBox.Show("작업 폴더가 존재하지 않으므로 작업 폴더를 초기화합니다.", string.Empty, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                WorkSpacePath = AppDomain.CurrentDomain.BaseDirectory;
            }
            // 다이얼로그 경로 적용
            LoadPictureFileDialog.InitialDirectory = WorkSpacePath;
            if (Directory.Exists(WorkSpacePath + NEKO_CONTROLS_PATH))
            {
                LoadPictureFileDialog.InitialDirectory += NEKO_CONTROLS_PATH;
            }
            // workfile 경로 적용
            WorkFilePath = mOpenControlFileDialog.FileName;
            mOpenControlFileDialog.InitialDirectory = Path.GetDirectoryName(WorkFilePath);
            mSaveControlFileDialog.InitialDirectory = Path.GetDirectoryName(WorkFilePath);
            mSaveScriptFileDialog.InitialDirectory = Path.GetDirectoryName(WorkFilePath);
            // 컨트롤 로드
            JArray jArray = (JArray)jObjectMain["Controls"];
            foreach (JObject jObjectControl in jArray)
            {
                string type = jObjectControl["Type"].ToString();
                if (type == "DPad4")
                {
                    var dPad4 = new NekoControlDPad4ViewModel(jObjectControl);
                    xViewModelMain.NekoControls.Add(dPad4);
                    xViewModelMain.SelectedNekoControlOrNull = dPad4;
                }
                else if (type == "DPad8")
                {
                    var dPad8 = new NekoControlDPad8ViewModel(jObjectControl);
                    xViewModelMain.NekoControls.Add(dPad8);
                    xViewModelMain.SelectedNekoControlOrNull = dPad8;
                }
                else if (type == "KeyButton")
                {
                    var keyButton = new NekoControlKeyButtonViewModel(jObjectControl);
                    xViewModelMain.NekoControls.Add(keyButton);
                    xViewModelMain.SelectedNekoControlOrNull = keyButton;
                }
                else
                {
                    Debug.Fail(FAILED_LOAD_CONTROL_MESSAGE);
                    xViewModelMain.NekoControls.Clear();
                    NekoControlViewModel.VariableNames.Clear();
                    MessageBox.Show("파일이 손상되었습니다.", "오류", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            xViewModelMain.SelectedNekoControlOrNull = null;
        }

        private void xButtonSave_Click(object sender, RoutedEventArgs e)
        {
            mSaveControlFileDialog.FileName = Path.GetFileNameWithoutExtension(WorkFilePath);
            if (mSaveControlFileDialog.ShowDialog() == true)
            {
                // workfile 경로 적용
                WorkFilePath = mSaveControlFileDialog.FileName;
                mOpenControlFileDialog.InitialDirectory = Path.GetDirectoryName(WorkFilePath);
                mSaveControlFileDialog.InitialDirectory = Path.GetDirectoryName(WorkFilePath);
                mSaveScriptFileDialog.InitialDirectory = Path.GetDirectoryName(WorkFilePath);
                // config json 추가
                var jObjectConfig = new JObject();
                jObjectConfig.Add("Width", (uint)xGrid3x3.ColumnDefinitions[2].Width.Value);
                jObjectConfig.Add("Height", (uint)xGrid3x3.RowDefinitions[2].Height.Value);
                jObjectConfig.Add("BackgroundColor", xCheckerBoard.BackgroundColor);
                jObjectConfig.Add("WorkSpacePath", WorkSpacePath);
                // controls json 추가
                string json = JsonConvert.SerializeObject(new {Config = jObjectConfig, Controls = xViewModelMain.NekoControls }, Formatting.Indented);
                // 파일 쓰기
                File.WriteAllText(mSaveControlFileDialog.FileName, json);
            }
        }

        private void xButtonSaveAsScript_Click(object sender, RoutedEventArgs e)
        {
            mSaveScriptFileDialog.FileName = Path.GetFileNameWithoutExtension(WorkFilePath);
            if (mSaveScriptFileDialog.ShowDialog() == false)
            {
                return;
            }
            mSaveScriptFileDialog.InitialDirectory = Path.GetDirectoryName(mSaveScriptFileDialog.FileName);
            DateTime localDate = DateTime.Now;
            string scriptTitle = Path.GetFileNameWithoutExtension(mSaveScriptFileDialog.FileName);
            string scriptDate = $"{localDate.Year}.{String.Format("{0:D2}", localDate.Month)}.{String.Format("{0:D2}", localDate.Day)}";
            string script =
$@"=begin
  title  {scriptTitle}

  author neko-player-control-editor
  date   {scriptDate}
  syntax ruby
  pltfrm android (neko player)
=end
#===============================================================================
if !$NEKO_RUBY.nil?
#-------------------------------------------------------------------------------
class {scriptTitle} < NekoController_Template

";
            string scriptAttribute = string.Empty;
            string scriptCreate = string.Empty;
            var last = xViewModelMain.NekoControls.Last();
            foreach (var control in xViewModelMain.NekoControls)
            {
                scriptAttribute += $"  attr_reader(:{control.Name})" + '\n';
                if (control is NekoControlDPad8ViewModel)
                {
                    var dPad8 = (NekoControlDPad8ViewModel)control;
                    scriptCreate += dPad8.GetRubyScript(WorkSpacePath + NEKO_CONTROLS_PATH);
                }
                else if (control is NekoControlDPad4ViewModel)
                {
                    var dPad4 = (NekoControlDPad4ViewModel)control;
                    scriptCreate += dPad4.GetRubyScript(WorkSpacePath + NEKO_CONTROLS_PATH);
                }
                else if (control is NekoControlKeyButtonViewModel)
                {
                    var keyButton = (NekoControlKeyButtonViewModel)control;
                    scriptCreate += keyButton.GetRubyScript(WorkSpacePath + NEKO_CONTROLS_PATH);
                }
                else
                {
                    Debug.Fail(FAILED_LOAD_CONTROL_MESSAGE);
                }
                if (control != last)
                {
                    scriptCreate += '\n';
                }
            }
            script += scriptAttribute + '\n';
            script += "  def initialize" + '\n';
            script += "    super" + '\n';
            script += scriptCreate;
            script += "  end" + '\n';
            script +=
$@"end

NekoControllerManager.entity = {scriptTitle}.new

#-------------------------------------------------------------------------------
end
#===============================================================================";
            // 파일 쓰기
            File.WriteAllText(mSaveScriptFileDialog.FileName, script);
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

        private void xButtonShowConfigWindow_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ConfigWindow();
            dialog.ValueWidth = (uint)xGrid3x3.ColumnDefinitions[2].Width.Value;
            dialog.ValueHeight = (uint)xGrid3x3.RowDefinitions[2].Height.Value;
            dialog.ValueBackgroundColor = xCheckerBoard.BackgroundColor;
            dialog.ValueWorkSpacePath = WorkSpacePath;
            if (dialog.ShowDialog().Equals(true))
            {
                xGrid3x3.ColumnDefinitions[2].Width = new GridLength(dialog.ValueWidth, GridUnitType.Pixel);
                xGrid3x3.RowDefinitions[2].Height = new GridLength(dialog.ValueHeight, GridUnitType.Pixel);
                xCheckerBoard.BackgroundColor = dialog.ValueBackgroundColor;
                WorkSpacePath = dialog.ValueWorkSpacePath;
            }
        }
        #endregion

        #region Side Menu Events
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
                Debug.Fail(FAILED_LOAD_CONTROL_MESSAGE);
            }
            xViewModelMain.NekoControls.Add(clone);
            xViewModelMain.SelectedNekoControlOrNull = clone;
            xListBoxNekoControls.ScrollIntoView(clone);
        }

        private void xButtonActualResizeControl_Click(object sender, RoutedEventArgs e)
        {
            var control = xViewModelMain.SelectedNekoControlOrNull;
            if (control == null)
            {
                return;
            }
            if (control is NekoControlDPad8ViewModel)
            {
                var dPad8 = (NekoControlDPad8ViewModel)control;
                dPad8.Width = (uint)dPad8.BitmapImageDefault.PixelWidth;
                dPad8.Height = (uint)dPad8.BitmapImageDefault.PixelHeight;
            }
            else if (control is NekoControlDPad4ViewModel)
            {
                var dPad4 = (NekoControlDPad4ViewModel)control;
                dPad4.Width = (uint)dPad4.BitmapImageDefault.PixelWidth;
                dPad4.Height = (uint)dPad4.BitmapImageDefault.PixelHeight;
            }
            else if (control is NekoControlKeyButtonViewModel)
            {
                var keyButton = (NekoControlKeyButtonViewModel)control;
                keyButton.Width = (uint)keyButton.BitmapImageDefault.PixelWidth;
                keyButton.Height = (uint)keyButton.BitmapImageDefault.PixelHeight;
            }
            else
            {
                Debug.Fail(FAILED_LOAD_CONTROL_MESSAGE);
            }
            xWpfPropertyGrid.Refresh();
        }

        private void xButtonRemoveControl_Click(object sender, RoutedEventArgs e)
        {
            var control = xViewModelMain.SelectedNekoControlOrNull;
            if (control != null)
            {
                MessageBoxResult result = MessageBox.Show(control.Name + " 을 삭제하시겠습니까?", "삭제", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    NekoControlViewModel.VariableNames.Remove(control.Name);
                    xViewModelMain.NekoControls.Remove(control);
                }
            }
        }
        #endregion

        #region Grid Event
        private void xGridRender_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            xViewModelMain.SelectedNekoControlOrNull = null;
        }

        private void xGridRender_MouseMove(object sender, MouseEventArgs e)
        {
            var point = Mouse.GetPosition((Grid)sender);
            int x = (int)point.X;
            int y = (int)point.Y;
            xTextBlockStatusPointX.Text = "X: " + x;
            xTextBlockStatusPointY.Text = "Y: " + y;
        }

        private void xGridRender_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            xTextBlockStatusResolution.Text = e.NewSize.Width + " x " + e.NewSize.Height;
        }

        private void xRectangle3x3_MouseMove(object sender, MouseEventArgs e)
        {
            var point = e.GetPosition((Rectangle)sender);
            int x = (int)(point.X - xGrid3x3.ColumnDefinitions[0].ActualWidth);
            int y = (int)(point.Y - xGrid3x3.RowDefinitions[0].ActualHeight);
            xTextBlockStatusPointX.Text = "X: " + x;
            xTextBlockStatusPointY.Text = "Y: " + y;
        }

        private void xTextBlockRemoveControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            var textBlock = (TextBlock)sender;
            var control = (NekoControlViewModel)textBlock.DataContext;
            MessageBoxResult result = MessageBox.Show(control.Name + " 을 삭제하시겠습니까?", "삭제", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                NekoControlViewModel.VariableNames.Remove(control.Name);
                xViewModelMain.NekoControls.Remove(control);
            }
        }
        #endregion

        #region xThumb Common Events
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
        #endregion

        #region xThumb DPad4 Events
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
                // control
                var bitmapImage = dPad4.BitmapImageDefault;
                dPad4.ImageSourceControl = bitmapImage;
                // stick
                var xImageStick = (Image)xThumb.Template.FindName("xImageStick", xThumb);
                Canvas.SetLeft(xImageStick, 0);
                Canvas.SetTop(xImageStick, 0);
            }
        }

        void updateDPad4(NekoControlDPad4ViewModel dPad4, object sender, MouseEventArgs e)
        {
            var xThumb = (Thumb)sender;
            var point = e.GetPosition(xThumb);
            var deltaPoint = new Point(dPad4.Width / 2 - point.X, dPad4.Height / 2 - point.Y);
            double angle = Math.Atan2(deltaPoint.Y, deltaPoint.X) * (180 / Math.PI);
            BitmapImage bitmapImage = null;
            if (angle >= -135 && angle < -45)
            {
                bitmapImage = dPad4.BitmapImageDown;
            }
            else if (angle >= -45 && angle < 45)
            {
                bitmapImage = dPad4.BitmapImageLeft;
            }
            else if (angle >= 45 && angle < 135)
            {
                bitmapImage = dPad4.BitmapImageUp;
            }
            else// if ((angle >= -180 && angle < -135) || (angle >= 135 && angle < 180))
            {
                bitmapImage = dPad4.BitmapImageRight;
            }
            dPad4.ImageSourceControl = bitmapImage;
        }

        void updateDPad4Stick(NekoControlDPad4ViewModel dPad4, object sender, MouseEventArgs e)
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
        #endregion

        #region xThumb DPad8 Events
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
                // control
                var bitmapImage = dPad8.BitmapImageDefault;
                dPad8.ImageSourceControl = bitmapImage;
                // stick
                var xImageStick = (Image)xThumb.Template.FindName("xImageStick", xThumb);
                Canvas.SetLeft(xImageStick, 0);
                Canvas.SetTop(xImageStick, 0);
            }
        }

        void updateDPad8(NekoControlDPad8ViewModel dPad8, object sender, MouseEventArgs e)
        {
            var xThumb = (Thumb)sender;
            var point = e.GetPosition(xThumb);
            var deltaPoint = new Point(dPad8.Width / 2 - point.X, dPad8.Height / 2 - point.Y);
            double angle = Math.Atan2(deltaPoint.Y, deltaPoint.X) * (180 / Math.PI);
            BitmapImage bitmapImage = null;
            if (angle >= -67.5 && angle < -22.5)
            {
                bitmapImage = dPad8.BitmapImageLowerLeft;
            }
            else if (angle >= -112.5 && angle < -67.5)
            {
                bitmapImage = dPad8.BitmapImageDown;
            }
            else if (angle >= -157.5 && angle < -112.5)
            {
                bitmapImage = dPad8.BitmapImageLowerRight;
            }
            else if (angle >= -22.5 && angle < 22.5)
            {
                bitmapImage = dPad8.BitmapImageLeft;
            }
            else if (angle >= 22.5 && angle < 67.5)
            {
                bitmapImage = dPad8.BitmapImageUpperLeft;
            }
            else if (angle >= 67.5 && angle < 112.5)
            {
                bitmapImage = dPad8.BitmapImageUp;
            }
            else if (angle >= 112.5 && angle < 157.5)
            {
                bitmapImage = dPad8.BitmapImageUpperRight;
            }
            else// if ((angle >= -180.0 && angle < -157.5) || (angle >= 157.5 && angle < 180.0))
            {
                bitmapImage = dPad8.BitmapImageRight;
            }
            dPad8.ImageSourceControl = bitmapImage;
        }
       
        void updateDPad8Stick(NekoControlDPad8ViewModel dPad8, object sender, MouseEventArgs e)
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
        #endregion

        #region xThumb KeyButton Events
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
                var bitmapImage = keyButton.BitmapImageDefault;
                keyButton.ImageSourceControl = bitmapImage;
            }
        }

        void updateKeyButton(NekoControlKeyButtonViewModel keyButton, object sender, MouseEventArgs e)
        {
            BitmapImage bitmapImage = keyButton.BitmapImagePressed;
            keyButton.ImageSourceControl = bitmapImage;
        }
        #endregion

        #region notifyPropertyChanged Event
        private void notifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}