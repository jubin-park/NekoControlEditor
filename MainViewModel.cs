using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;

namespace NekoControlEditor
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Data Members
        private ObservableCollection<NekoControlViewModel> mNekoControls = new ObservableCollection<NekoControlViewModel>();
        private NekoControlViewModel mSelectedNekoControl = null;
        #endregion Data Members

        public ObservableCollection<NekoControlViewModel> NekoControls
        {
            get
            {
                return mNekoControls;
            }
        }

        public NekoControlViewModel SelectedNekoControl
        {
            get
            {
                return mSelectedNekoControl;
            }
            set
            {
                if (mSelectedNekoControl != value)
                {
                    mSelectedNekoControl = value;
                    notifyPropertyChanged("SelectedNekoControl");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
        }

        private void notifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
