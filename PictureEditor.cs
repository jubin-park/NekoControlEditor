﻿using Microsoft.Win32;
using System;
using System.Activities.Presentation.PropertyEditing;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace NekoControlEditor
{
    class PictureEditor : DialogPropertyValueEditor
    {
        public PictureEditor()
        {
            string template = @"
                <DataTemplate
                    xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
                    xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
                    xmlns:pe='clr-namespace:System.Activities.Presentation.PropertyEditing;assembly=System.Activities.Presentation'>
                    <DockPanel LastChildFill='True'>
                        <pe:EditModeSwitchButton
                            TargetEditMode='Dialog'
                            Name='EditButton'
                            Content='...'
                            DockPanel.Dock='Right'
                            />
                        <TextBlock
                            Text='(이미지)'
                            Margin='2,0,0,0'
                            VerticalAlignment='Center'
                            />
                    </DockPanel>
                </DataTemplate>";

            using (var sr = new MemoryStream(Encoding.UTF8.GetBytes(template)))
            {
                InlineEditorTemplate = XamlReader.Load(sr) as DataTemplate;
            }
        }

        public override void ShowDialog(PropertyValue propertyValue, IInputElement commandSource)
        {
            if (MainWindow.LoadPictureFileDialog.ShowDialog() == true)
            {
                propertyValue.Value = string.Empty;
                propertyValue.Value = MainWindow.LoadPictureFileDialog.FileName;
                MainWindow.LoadPictureFileDialog.InitialDirectory = Path.GetDirectoryName(MainWindow.LoadPictureFileDialog.FileName);
            }
        }
    }
}
