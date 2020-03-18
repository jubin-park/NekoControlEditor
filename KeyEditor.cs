using System.Activities.Presentation.Converters;
using System.Activities.Presentation.Model;
using System.Activities.Presentation.PropertyEditing;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace NekoControlEditor
{
    public class KeyEditor : DialogPropertyValueEditor
    {
        public KeyEditor()
        {
            string template = @"
                <DataTemplate
                    xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
                    xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
                    xmlns:pe='clr-namespace:System.Activities.Presentation.PropertyEditing;assembly=System.Activities.Presentation'>
                    <DockPanel LastChildFill='True'>
                        <pe:EditModeSwitchButton TargetEditMode='Dialog' Name='EditButton' 
                        DockPanel.Dock='Right'>...</pe:EditModeSwitchButton>
                        <TextBlock Text='{Binding Value.ValueToString}' Margin='2,0,0,0' VerticalAlignment='Center'/>
                    </DockPanel>
                </DataTemplate>";

            using (var sr = new MemoryStream(Encoding.UTF8.GetBytes(template)))
            {
                InlineEditorTemplate = XamlReader.Load(sr) as DataTemplate;
            }
        }

        public override void ShowDialog(PropertyValue propertyValue, IInputElement commandSource)
        {
            KeyIdentifierWindow window = new KeyIdentifierWindow((EKeysValue)propertyValue.Value);
            if (window.ShowDialog().Equals(true))
            {
                var ownerActivityConverter = new ModelPropertyEntryToOwnerActivityConverter();
                ModelItem activityItem = ownerActivityConverter.Convert(propertyValue.ParentProperty, typeof(ModelItem), false, null) as ModelItem;
                using (ModelEditingScope editingScope = activityItem.BeginEdit())
                {
                    propertyValue.Value = window.InputKey;
                    editingScope.Complete(); // commit the changes
                    var control = commandSource as Control;
                    var oldData = control.DataContext;
                    control.DataContext = null;
                    control.DataContext = oldData;
                }
            }
        }
    }
}
