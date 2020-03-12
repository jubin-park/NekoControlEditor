using System.Activities.Presentation.PropertyEditing;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Markup;

namespace NekoControlEditor
{
    public class SliderEditor : ExtendedPropertyValueEditor
    {
        public SliderEditor()
        {
            string template1 = @"  
            <DataTemplate  
                xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'  
                xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'  
                xmlns:pe='clr-namespace:System.Activities.Presentation.PropertyEditing;assembly=System.Activities.Presentation'   
                xmlns:wpg='clr-namespace:PropertyGrid;assembly=PropertyGrid' >   
                <DockPanel LastChildFill='True'>  
                        <TextBox Text='{Binding Path=Value.NowValue, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}' Width='30' TextAlignment='Center' />  
                        <Slider x:Name='slider1' Value='{Binding Path=Value.NowValue, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}' Margin='2,0,0,0' Minimum='{Binding Value.Min}' Maximum='{Binding Value.Max}' />  
                </DockPanel>  
            </DataTemplate>";
            using (var sr = new MemoryStream(Encoding.UTF8.GetBytes(template1)))
            {
                InlineEditorTemplate = XamlReader.Load(sr) as DataTemplate;
            }
        }
    }

    public class SliderValue<T> : INotifyPropertyChanged
    {
        private T mValue;
        public T NowValue
        {
            get
            {
                return mValue;
            }
            set
            {
                mValue = value;
                notifyPropertyChanged("NowValue");
            }
        }
        public T Max { get; set; }
        public T Min { get; set; }
        public T Step { get; set; }

        public SliderValue(T value, T min, T max, T step)
        {
            NowValue = value;
            Min = min;
            Max = max;
            Step = step;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void notifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
