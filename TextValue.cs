using System.ComponentModel;

namespace NekoControlEditor
{
    public class TextValue<T> : INotifyPropertyChanged
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

        public TextValue(T value)
        {
            NowValue = value;
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
