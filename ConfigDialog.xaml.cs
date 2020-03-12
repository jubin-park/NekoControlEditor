﻿using System;
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
using System.Windows.Shapes;

namespace NekoControlEditor
{
    /// <summary>
    /// ConfigWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ConfigWindow : Window, INotifyPropertyChanged
    {        
        public uint mValueWidth;
        public uint ValueWidth
        {
            get
            {
                return mValueWidth;
            }
            set
            {
                if (mValueWidth != value)
                {
                    mValueWidth = value;
                    notifyPropertyChanged("ValueWidth");
                }
            }
        }

        public uint mValueHeight;
        public uint ValueHeight
        {
            get
            {
                return mValueHeight;
            }
            set
            {
                if (mValueHeight != value)
                {
                    mValueHeight = value;
                    notifyPropertyChanged("ValueHeight");
                }
            }
        }

        public string ValueBackgroundColor { get; set; }

        public ConfigWindow()
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void notifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void xButtonOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void xButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (ValueWidth == 640 && ValueHeight == 480)
            {
                xRadioButtonXP.IsChecked = true;
            }
            else if (ValueWidth == 544 && ValueHeight == 416)
            {
                xRadioButtonVXA.IsChecked = true;
            }
            else
            {
                xRadioButtonCustom.IsChecked = true;
            }
            xColorPicker.SelectedColor = (Color)ColorConverter.ConvertFromString(ValueBackgroundColor);
        }

        private void xColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (xColorPicker.SelectedColor.HasValue)
            {
                ValueBackgroundColor = xColorPicker.SelectedColor.ToString();
            }
        }

        private void xRadioButtonXP_Click(object sender, RoutedEventArgs e)
        {
            ValueWidth = 640;
            ValueHeight = 480;
        }

        private void xRadioButtonVXA_Click(object sender, RoutedEventArgs e)
        {
            ValueWidth = 544;
            ValueHeight = 416;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }
    }
}
