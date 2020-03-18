﻿using System.ComponentModel;

namespace NekoControlEditor
{
    public class SliderValue<T> : INotifyPropertyChanged
    {
        private T mValue;
        public T Value
        {
            get
            {
                return mValue;
            }
            set
            {
                mValue = value;
                notifyPropertyChanged("Value");
            }
        }
        public T Max { get; set; }
        public T Min { get; set; }
        public T Step { get; set; }

        public SliderValue(T value, T min, T max, T step)
        {
            Value = value;
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
