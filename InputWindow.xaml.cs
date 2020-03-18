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
    /// InputWindow.xaml에 대한 상호 작용 논리
    /// </summary>

    // https://docs.microsoft.com/ko-kr/dotnet/api/system.windows.input.key?view=netframework-4.8

    public partial class InputWindow : Window, INotifyPropertyChanged
    {
        public Dictionary<Key, EInput> InputKBTable = new Dictionary<Key, EInput>()
        {
            { Key.Escape, EInput.KB_ESCAPE },
            { Key.F1, EInput.KB_F1 },
            { Key.F2, EInput.KB_F2 },
            { Key.F3, EInput.KB_F3 },
            { Key.F4, EInput.KB_F4 },
            { Key.F5, EInput.KB_F5 },
            { Key.F6, EInput.KB_F6 },
            { Key.F7, EInput.KB_F7 },
            { Key.F8, EInput.KB_F8 },
            { Key.F9, EInput.KB_F9 },
            { Key.F10, EInput.KB_F10 }, // systemKey
            { Key.F11, EInput.KB_F11 },
            { Key.F12, EInput.KB_F12 },
            { Key.F13, EInput.KB_F13 },
            { Key.F14, EInput.KB_F14 },
            { Key.F15, EInput.KB_F15 },

            { Key.D0, EInput.KB_0 },
            { Key.D1, EInput.KB_1 },
            { Key.D2, EInput.KB_2 },
            { Key.D3, EInput.KB_3 },
            { Key.D4, EInput.KB_4 },
            { Key.D5, EInput.KB_5 },
            { Key.D6, EInput.KB_6 },
            { Key.D7, EInput.KB_7 },
            { Key.D8, EInput.KB_8 },
            { Key.D9, EInput.KB_9 },

            { Key.Oem3, EInput.KB_TILDE },
            { Key.Tab, EInput.KB_TAB },
            { Key.Capital, EInput.KB_CAPSLOCK },
            { Key.LeftShift, EInput.KB_LSHIFT },
            { Key.LeftCtrl, EInput.KB_LCTRL },
            { Key.LeftAlt, EInput.KB_LALT }, // systemKey
            { Key.Back, EInput.KB_BACKSPACE },
            { Key.Return, EInput.KB_ENTER },
            { Key.RightShift, EInput.KB_RSHIFT },
            { Key.RightAlt, EInput.KB_RALT },
            { Key.RightCtrl, EInput.KB_RCTRL },
            { Key.KanaMode, EInput.KB_RALT },
            { Key.HanjaMode, EInput.KB_RCTRL },

            { Key.OemMinus, EInput.KB_MINUS },
            { Key.OemPlus, EInput.KB_EQUALS },
            { Key.Oem5, EInput.KB_BACKSLASH },
            { Key.OemOpenBrackets, EInput.KB_LEFTBRACKET },
            { Key.Oem6, EInput.KB_RIGHTBRACKET },
            { Key.Oem1, EInput.KB_SEMICOLON },
            { Key.OemQuotes, EInput.KB_APOSTROPHE },
            { Key.OemComma, EInput.KB_COMMA },
            { Key.OemPeriod, EInput.KB_PERIOD },
            { Key.OemQuestion, EInput.KB_SLASH },
            { Key.Space, EInput.KB_SPACE },

            { Key.A, EInput.KB_A },
            { Key.B, EInput.KB_B },
            { Key.C, EInput.KB_C },
            { Key.D, EInput.KB_D },
            { Key.E, EInput.KB_E },
            { Key.F, EInput.KB_F },
            { Key.G, EInput.KB_G },
            { Key.H, EInput.KB_H },
            { Key.I, EInput.KB_I },
            { Key.J, EInput.KB_J },
            { Key.K, EInput.KB_K },
            { Key.L, EInput.KB_L },
            { Key.M, EInput.KB_M },
            { Key.N, EInput.KB_N },
            { Key.O, EInput.KB_O },
            { Key.P, EInput.KB_P },
            { Key.Q, EInput.KB_Q },
            { Key.R, EInput.KB_R },
            { Key.S, EInput.KB_S },
            { Key.T, EInput.KB_T },
            { Key.U, EInput.KB_U },
            { Key.V, EInput.KB_V },
            { Key.W, EInput.KB_W },
            { Key.X, EInput.KB_X },
            { Key.Y, EInput.KB_Y },
            { Key.Z, EInput.KB_Z },


            { Key.Snapshot, EInput.KB_PRINTSCREEN },
            { Key.Scroll, EInput.KB_SCROLLLOCK },
            { Key.Pause, EInput.KB_PAUSEBREAK },
            { Key.Insert, EInput.KB_INSERT },
            { Key.Home, EInput.KB_HOME },
            { Key.PageUp, EInput.KB_PAGEUP },
            { Key.Delete, EInput.KB_DELETE },
            { Key.End, EInput.KB_END },
            { Key.Next, EInput.KB_PAGEDOWN },


            { Key.Up, EInput.KB_UP },
            { Key.Left, EInput.KB_LEFT },
            { Key.Down, EInput.KB_DOWN },
            { Key.Right, EInput.RIGHT },


            { Key.NumPad0, EInput.KB_KEYPAD_0 },
            { Key.NumPad1, EInput.KB_KEYPAD_1 },
            { Key.NumPad2, EInput.KB_KEYPAD_2 },
            { Key.NumPad3, EInput.KB_KEYPAD_3 },
            { Key.NumPad4, EInput.KB_KEYPAD_4 },
            { Key.NumPad5, EInput.KB_KEYPAD_5 },
            { Key.NumPad6, EInput.KB_KEYPAD_6 },
            { Key.NumPad7, EInput.KB_KEYPAD_7 },
            { Key.NumPad8, EInput.KB_KEYPAD_8 },
            { Key.NumPad9, EInput.KB_KEYPAD_9 },

            { Key.NumLock, EInput.KB_NUMLOCK },
            { Key.Divide, EInput.KB_KEYPAD_DIVIDE },
            { Key.Multiply, EInput.KB_KEYPAD_MULTIPLY },
            { Key.Subtract, EInput.KB_KEYPAD_MINUS },
            { Key.Add, EInput.KB_KEYPAD_PLUS },
            //{ Key.Enter, EInput.KB_KEYPAD_ENTER }, // can be a bug
            { Key.Decimal, EInput.KB_KEYPAD_PERIOD },

            // { Key., EInput. },
        };

        public InputProperty mInputKey;
        public InputProperty InputKey
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
                if (mInputKey.Value == EInput.NULL)
                {
                    return "(없음)";
                }
                return mInputKey.Value.ToString().Substring(3);
            }
        }

        public string KeyInfo
        {
            get
            {
                if (mInputKey.Value == EInput.NULL)
                {
                    return "지원하지 않는 키입니다.";
                }
                return mInputKey.Value.ToString();
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

        public InputWindow(InputProperty key)
        {
            InitializeComponent();
            DataContext = this;
            InputKey = new InputProperty(key.Value);
            InputKey.PropertyChanged += new PropertyChangedEventHandler(eKeysPropertyChanged);
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
                InputKey.Value = InputKBTable[e.Key];
            }
            else
            {
                if (InputKBTable.ContainsKey(e.SystemKey))
                {
                    InputKey.Value = InputKBTable[e.SystemKey];
                }
                else
                {
                    InputKey.Value = EInput.NULL;
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

        private void eKeysPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e != null)
            {
                notifyPropertyChanged("KeyName");
                notifyPropertyChanged("KeyInfo");
            }
        }
    }
}