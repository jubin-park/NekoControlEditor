using System.ComponentModel;

namespace NekoControlEditor
{
    public class EKeysValue : INotifyPropertyChanged
    {
        private EKeys mValue;
        public EKeys Value
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

        public string ValueToString
        {
            get
            {
                if (mValue == EKeys.NULL)
                {
                    return "(없음)";
                }
                return mValue.ToString();
            }
        }

        public EKeysValue(EKeys value)
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
