using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NekoControlEditor
{
    /// <summary>
    /// KeyIdentifierWindow.xaml에 대한 상호 작용 논리
    /// </summary>

    // https://docs.microsoft.com/ko-kr/dotnet/api/system.windows.input.key?view=netframework-4.8

    public partial class KeyIdentifierWindow : Window, INotifyPropertyChanged
    {
        public Dictionary<Key, EKeys> InputKBTable = new Dictionary<Key, EKeys>()
        {
            { Key.Escape, EKeys.KB_ESCAPE },
            { Key.F1, EKeys.KB_F1 },
            { Key.F2, EKeys.KB_F2 },
            { Key.F3, EKeys.KB_F3 },
            { Key.F4, EKeys.KB_F4 },
            { Key.F5, EKeys.KB_F5 },
            { Key.F6, EKeys.KB_F6 },
            { Key.F7, EKeys.KB_F7 },
            { Key.F8, EKeys.KB_F8 },
            { Key.F9, EKeys.KB_F9 },
            { Key.F10, EKeys.KB_F10 }, // systemKey
            { Key.F11, EKeys.KB_F11 },
            { Key.F12, EKeys.KB_F12 },
            { Key.F13, EKeys.KB_F13 },
            { Key.F14, EKeys.KB_F14 },
            { Key.F15, EKeys.KB_F15 },

            { Key.D0, EKeys.KB_0 },
            { Key.D1, EKeys.KB_1 },
            { Key.D2, EKeys.KB_2 },
            { Key.D3, EKeys.KB_3 },
            { Key.D4, EKeys.KB_4 },
            { Key.D5, EKeys.KB_5 },
            { Key.D6, EKeys.KB_6 },
            { Key.D7, EKeys.KB_7 },
            { Key.D8, EKeys.KB_8 },
            { Key.D9, EKeys.KB_9 },

            { Key.Oem3, EKeys.KB_TILDE },
            { Key.Tab, EKeys.KB_TAB },
            { Key.Capital, EKeys.KB_CAPSLOCK },
            { Key.LeftShift, EKeys.KB_LSHIFT },
            { Key.LeftCtrl, EKeys.KB_LCTRL },
            { Key.LeftAlt, EKeys.KB_LALT }, // systemKey
            { Key.Back, EKeys.KB_BACKSPACE },
            { Key.Return, EKeys.KB_ENTER },
            { Key.RightShift, EKeys.KB_RSHIFT },
            { Key.RightAlt, EKeys.KB_RALT },
            { Key.RightCtrl, EKeys.KB_RCTRL },
            { Key.KanaMode, EKeys.KB_RALT },
            { Key.HanjaMode, EKeys.KB_RCTRL },

            { Key.OemMinus, EKeys.KB_MINUS },
            { Key.OemPlus, EKeys.KB_EQUALS },
            { Key.Oem5, EKeys.KB_BACKSLASH },
            { Key.OemOpenBrackets, EKeys.KB_LEFTBRACKET },
            { Key.Oem6, EKeys.KB_RIGHTBRACKET },
            { Key.Oem1, EKeys.KB_SEMICOLON },
            { Key.OemQuotes, EKeys.KB_APOSTROPHE },
            { Key.OemComma, EKeys.KB_COMMA },
            { Key.OemPeriod, EKeys.KB_PERIOD },
            { Key.OemQuestion, EKeys.KB_SLASH },
            { Key.Space, EKeys.KB_SPACE },

            { Key.A, EKeys.KB_A },
            { Key.B, EKeys.KB_B },
            { Key.C, EKeys.KB_C },
            { Key.D, EKeys.KB_D },
            { Key.E, EKeys.KB_E },
            { Key.F, EKeys.KB_F },
            { Key.G, EKeys.KB_G },
            { Key.H, EKeys.KB_H },
            { Key.I, EKeys.KB_I },
            { Key.J, EKeys.KB_J },
            { Key.K, EKeys.KB_K },
            { Key.L, EKeys.KB_L },
            { Key.M, EKeys.KB_M },
            { Key.N, EKeys.KB_N },
            { Key.O, EKeys.KB_O },
            { Key.P, EKeys.KB_P },
            { Key.Q, EKeys.KB_Q },
            { Key.R, EKeys.KB_R },
            { Key.S, EKeys.KB_S },
            { Key.T, EKeys.KB_T },
            { Key.U, EKeys.KB_U },
            { Key.V, EKeys.KB_V },
            { Key.W, EKeys.KB_W },
            { Key.X, EKeys.KB_X },
            { Key.Y, EKeys.KB_Y },
            { Key.Z, EKeys.KB_Z },


            { Key.Snapshot, EKeys.KB_PRINTSCREEN },
            { Key.Scroll, EKeys.KB_SCROLLLOCK },
            { Key.Pause, EKeys.KB_PAUSEBREAK },
            { Key.Insert, EKeys.KB_INSERT },
            { Key.Home, EKeys.KB_HOME },
            { Key.PageUp, EKeys.KB_PAGEUP },
            { Key.Delete, EKeys.KB_DELETE },
            { Key.End, EKeys.KB_END },
            { Key.Next, EKeys.KB_PAGEDOWN },


            { Key.Up, EKeys.KB_UP },
            { Key.Left, EKeys.KB_LEFT },
            { Key.Down, EKeys.KB_DOWN },
            { Key.Right, EKeys.RIGHT },


            { Key.NumPad0, EKeys.KB_KEYPAD_0 },
            { Key.NumPad1, EKeys.KB_KEYPAD_1 },
            { Key.NumPad2, EKeys.KB_KEYPAD_2 },
            { Key.NumPad3, EKeys.KB_KEYPAD_3 },
            { Key.NumPad4, EKeys.KB_KEYPAD_4 },
            { Key.NumPad5, EKeys.KB_KEYPAD_5 },
            { Key.NumPad6, EKeys.KB_KEYPAD_6 },
            { Key.NumPad7, EKeys.KB_KEYPAD_7 },
            { Key.NumPad8, EKeys.KB_KEYPAD_8 },
            { Key.NumPad9, EKeys.KB_KEYPAD_9 },

            { Key.NumLock, EKeys.KB_NUMLOCK },
            { Key.Divide, EKeys.KB_KEYPAD_DIVIDE },
            { Key.Multiply, EKeys.KB_KEYPAD_MULTIPLY },
            { Key.Subtract, EKeys.KB_KEYPAD_MINUS },
            { Key.Add, EKeys.KB_KEYPAD_PLUS },
            //{ Key.Enter, EKeys.KB_KEYPAD_ENTER }, // can be a bug
            { Key.Decimal, EKeys.KB_KEYPAD_PERIOD },

            // { Key., EKeys. },
        };

        public EKeys mInputKey;
        public EKeys InputKey
        {
            get
            {
                return mInputKey;
            }
            set
            {
                if (mInputKey != value)
                {
                    mInputKey = value;
                    notifyPropertyChanged("KeyName");
                    notifyPropertyChanged("KeyInfo");
                }
            }
        }

        public string KeyName
        {
            get
            {
                if (mInputKey == EKeys.NULL)
                {
                    return "(없음)";
                }
                return mInputKey.ToString().Substring(3);
            }
        }

        public string KeyInfo
        {
            get
            {
                if (mInputKey == EKeys.NULL)
                {
                    return "지원하지 않는 키입니다.";
                }
                return mInputKey.ToString();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void notifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public KeyIdentifierWindow(EKeys key)
        {
            InitializeComponent();
            DataContext = this;
            InputKey = key;
            Activate();
            Focus();
        }

        void xButtonOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void xButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void xWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (InputKBTable.ContainsKey(e.Key))
            {
                InputKey = InputKBTable[e.Key];
            }
            else
            {
                if (InputKBTable.ContainsKey(e.SystemKey))
                {
                    InputKey = InputKBTable[e.SystemKey];
                }
                else
                {
                    InputKey = EKeys.NULL;
                }
            }
        }

        private void xWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.SystemKey == Key.F10)
            {
                // 버튼 넘어가기 방지 Tab
                // F10 누르고 F9 누르면 안눌러지는 버그 방지
                e.Handled = true;
            }
        }
    }
}