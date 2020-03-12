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
        private NekoControlViewModel mSelectedNekoControlOrNull = null;
        #endregion Data Members

        public ObservableCollection<NekoControlViewModel> NekoControls
        {
            get
            {
                return mNekoControls;
            }
        }

        public NekoControlViewModel SelectedNekoControlOrNull
        {
            get
            {
                return mSelectedNekoControlOrNull;
            }
            set
            {
                if (mSelectedNekoControlOrNull != value)
                {
                    if (mSelectedNekoControlOrNull != null)
                    {
                        mSelectedNekoControlOrNull.IsSelected = false;
                        mSelectedNekoControlOrNull.BorderColor = mSelectedNekoControlOrNull.Visible ? "#2980b9" : "DimGray";
                    }
                    if (value != null)
                    {
                        value.IsSelected = true;
                        value.BorderColor = "Red";
                    }
                    mSelectedNekoControlOrNull = value;
                    notifyPropertyChanged("SelectedNekoControlOrNull");
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
