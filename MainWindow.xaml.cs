using System;
using System.Collections.Generic;
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

        private void CreateControlDirection4Button_Click(object sender, RoutedEventArgs e)
        {
            NekoControlDPad4ViewModel nekoDPad4 = new NekoControlDPad4ViewModel();
            MainViewModel.NekoControls.Add(nekoDPad4);
        }

        private void ResolutionButton_Click(object sender, RoutedEventArgs e)
        {
            RenderGrid.ColumnDefinitions[1].Width = new GridLength(600, GridUnitType.Pixel);
            RenderGrid.RowDefinitions[1].Height = new GridLength(400, GridUnitType.Pixel);
        }

        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            Thumb thumb = (Thumb)sender;
            NekoControlViewModel nekoControlViewModel = (NekoControlViewModel)thumb.DataContext;
            nekoControlViewModel.X += (int)e.HorizontalChange;
            nekoControlViewModel.Y += (int)e.VerticalChange;
        }
    }
}