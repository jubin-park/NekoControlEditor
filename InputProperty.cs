﻿using Newtonsoft.Json;
using System.ComponentModel;

namespace NekoControlEditor
{
    public class InputProperty : INotifyPropertyChanged
    {
        private EInput mValue;
        public EInput Value
        {
            get
            {
                return mValue;
            }
            set
            {
                mValue = value;
                notifyPropertyChanged("Value");
                notifyPropertyChanged("ValueToString");
            }
        }

        [JsonIgnore]
        public string ValueToString
        {
            get
            {
                if (mValue == EInput.NULL)
                {
                    return "(없음)";
                }
                return mValue.ToString();
            }
        }

        public InputProperty(EInput value)
        {
            Value = value;
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
