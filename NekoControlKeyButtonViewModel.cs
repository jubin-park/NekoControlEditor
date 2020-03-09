using System.ComponentModel;

namespace NekoControlEditor
{
    public class NekoControlKeyButtonViewModel : NekoControlViewModel
    {
        private EKeys mKey;
        [Category("키보드")]
        [DisplayName("키")]
        public EKeys Key
        {
            get
            {
                return mKey;
            }
            set
            {
                if (mKey != value)
                {
                    mKey = value;
                    notifyPropertyChanged("Key");
                }
            }
        }

        public NekoControlKeyButtonViewModel()
        {
            Key = EKeys.EMPTY;
        }
    }
}
