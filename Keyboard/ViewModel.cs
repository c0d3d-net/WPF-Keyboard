/*
 * Author : İbrahim GAZALOĞLU 
 * Date   : 23.03.2019
 * Email  : ibrahimgazaloglu@gmail.com  
 *
 */

using System.Collections.Generic;
using System.Windows.Input;
using WindowsInput;
using WindowsInput.Native;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using Gazaloglu.OnScreenKeyboard.Annotations;
using Gazaloglu.OnScreenKeyboard.Command;
using Gazaloglu.OnScreenKeyboard.Enums;


namespace Gazaloglu.OnScreenKeyboard
{
    public class ViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region PrivateMembers

        private readonly ICommandFactory _commandFactory = new CommandFactory();

        private readonly IInputSimulator _sim = new InputSimulator();

        private LayoutType _currentLayout;

        private Visibility _keyboardVisibility;


        //KeySets
        private readonly List<ICommand> _keySet1;
        private readonly List<ICommand> _keySet2;
        private readonly List<ICommand> _keySet3;
        private readonly List<ICommand> _keySet4;

        //KeySet ShiftVersion
        private readonly List<ICommand> _keySet1ShiftVersion;
        private readonly List<ICommand> _keySet2ShiftVersion;
        private readonly List<ICommand> _keySet3ShiftVersion;
        private readonly List<ICommand> _keySet4ShiftVersion;
        private readonly List<ICommand> _keySet5ShiftVersion;


        #endregion

        #region CustomCommands

        //Commands
        public ICommand CmdBackspaceKeyPress { get; }
        public ICommand CmdSpaceKeyPress { get; }
        public ICommand CmdCapsLockKeyPress { get; }
        public ICommand CmdShiftKeyPress { get; }
        public ICommand CmdTabKeyPress { get; }
        public ICommand CmdEnterKeyPress { get; }
        public ICommand CmdDelKeyPress { get; }
        public ICommand CmdCloseKeyboard { get; }

        #endregion

        #region PublicMembers

        public Visibility KeyboardVisibility
        {
            get => _keyboardVisibility;
            set
            {
                _keyboardVisibility = value;
                OnPropertyChanged();
            }
        }


        public LayoutType CurrentLayout
        {
            get => _currentLayout;
            set
            {
                _currentLayout = value;
                OnPropertyChanged(nameof(CurrentLayout));
            }
        }

        //KeyStates 
        public bool IsCapsLockLocked => Task.Run(() => _sim.InputDeviceState.IsTogglingKeyInEffect(VirtualKeyCode.CAPITAL)).Result;
        public bool IsShiftLocked => Task.Run(() => _sim.InputDeviceState.IsKeyDown(VirtualKeyCode.SHIFT)).Result;

        //KeySets
        public List<ICommand> KeySet1 => IsShiftLocked ? _keySet1ShiftVersion : _keySet1;
        public List<ICommand> KeySet2 => IsShiftLocked ? _keySet2ShiftVersion : _keySet2;
        public List<ICommand> KeySet3 => IsShiftLocked ? _keySet3ShiftVersion : _keySet3;
        public List<ICommand> KeySet4 => IsShiftLocked ? _keySet4ShiftVersion : _keySet4;
        public List<ICommand> KeySet5 { get; }
        public List<ICommand> NumericKeys { get; }

        #endregion

        public ViewModel()
        {


            _currentLayout = LayoutType.Initial;
  

            #region Commands
            CmdCloseKeyboard = _commandFactory.CreateCommand(() => { KeyboardVisibility = Visibility.Collapsed; });
            #endregion

            #region KeySet1

            //KeySet1
            _keySet1 = new List<ICommand>
            {
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.ESCAPE)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry("`")),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD1)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD2)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD3)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD4)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD5)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD6)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD7)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD8)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD9)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD0)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry("-")),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry("="))

            };

            #endregion

            #region KeySet1ShiftVersion

            //KeySet1
            _keySet1ShiftVersion = new List<ICommand>
            {
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.ESCAPE)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry("~")),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry("!")),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry("@")),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry("#")),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry("$")),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry("%")),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry("^")),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry("&")),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry("*")),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry("(")),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry(")")),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry("_")),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry("+"))

            };
            #endregion

            #region KeySet2

            //KeySet2
            _keySet2 = new List<ICommand>
            {
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_Q)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_W)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_E)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_R)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_T)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_Y)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_U)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_I)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_O)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_P)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry("[")),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry("]")),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry("\\")),

            };

            #endregion

            #region KeySet2ShiftVersion

            //KeySet2
            _keySet2ShiftVersion = new List<ICommand>
            {
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_Q)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_W)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_E)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_R)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_T)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_Y)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_U)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_I)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_O)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_P)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry("{")),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry("}")),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry("|")),

            };

            #endregion

            #region KeySet3

            //KeySet3
            _keySet3 = new List<ICommand>
            {
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_A)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_S)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_D)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_F)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_G)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_H)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_J)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_K)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_L)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry(":")),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry(".")),

            };

            #endregion

            #region KeySet3ShiftVersion

            //KeySet3
            _keySet3ShiftVersion = new List<ICommand>
            {
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_A)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_S)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_D)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_F)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_G)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_H)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_J)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_K)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_L)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry(":")),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry("\""))

            };

            #endregion

            #region KeySet4

            //KeySet4
            _keySet4 = new List<ICommand>
            {
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_Z)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_X)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_C)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_V)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_B)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_N)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_M)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry(",")),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry(".")),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry("/")),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.UP)),
            };
            #endregion

            #region KeySet4ShiftVersion

            //KeySet4
            _keySet4ShiftVersion = new List<ICommand>
            {
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_Z)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_X)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_C)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_V)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_B)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_N)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.VK_M)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry("<")),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry(">")),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry("?")),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.UP)),
            };
            #endregion

            #region KeySet5

            //KeySet5
            KeySet5 = new List<ICommand>
            {
                _commandFactory.CreateCommand(() => CurrentLayout = LayoutType.Numeric,ignorePostCommands: new [] {0,1}  /* Ignore SetProperLayoutCommand */),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.CONTROL)),
                _commandFactory.CreateCommand(() => Debug.WriteLine("WIN")),
                _commandFactory.CreateCommand(() => Debug.WriteLine("ALT")),
            };


            #endregion

            #region NumericKeys

            //NumeicKeys
            NumericKeys = new List<ICommand>
            {
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD1)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD2)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD3)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.BACK)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD4)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD5)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD6)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry("00")),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD7)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD8)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD9)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry("000")),
                _commandFactory.CreateCommand(() => CurrentLayout = LayoutType.Initial),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD0)),
                _commandFactory.CreateCommand(() => _sim.Keyboard.TextEntry(".")),
                _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.RETURN))
            };


            #endregion

            #region PreCommands
            _commandFactory.PreCommands.Add(KeySetsChanged);

            #endregion

            #region PostCommands

            //Deactivate Shift, Index = 0
            _commandFactory.PostCommands.Add(() =>
            {
                if (IsShiftLocked)
                    _sim.Keyboard.KeyPress(VirtualKeyCode.SHIFT);

                OnPropertyChanged(nameof(IsShiftLocked));

            });


            //Set Proper Layout
            _commandFactory.PostCommands.Add(() =>
            {
                if (CurrentLayout == LayoutType.Numeric)
                    return;

                if (IsShiftLocked)
                {
                    CurrentLayout = LayoutType.Shift;
                    KeySetsChanged();
                }

                else if (IsCapsLockLocked)
                    CurrentLayout = LayoutType.Capital;

                else
                    CurrentLayout = LayoutType.Initial;

            });


            #endregion

            #region CustomKeyDefinitions

            //Backspace
            CmdBackspaceKeyPress = _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.BACK));

            //CapsLock
            CmdCapsLockKeyPress = _commandFactory.CreateCommand(() =>
            {
                _sim.Keyboard.KeyPress(VirtualKeyCode.CAPITAL);
                OnPropertyChanged(nameof(IsCapsLockLocked));

            });

            //Shift
            CmdShiftKeyPress = _commandFactory.CreateCommand(() =>
            {
                Debug.WriteLine("Basıldı");
                if (!IsShiftLocked)
                    _sim.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
                else
                    _sim.Keyboard.KeyUp(VirtualKeyCode.SHIFT);

                OnPropertyChanged(nameof(IsShiftLocked));

            }, ignorePostCommands: new[] { 0 } /* Ignore DeactivateShiftPostCommand */);

            //Tab
            CmdTabKeyPress = _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.TAB));

            //Space
            CmdSpaceKeyPress = _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.SPACE));

            //Enter
            CmdEnterKeyPress = _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.RETURN));

            //Delete
            CmdDelKeyPress = _commandFactory.CreateCommand(() => _sim.Keyboard.KeyPress(VirtualKeyCode.DELETE));

            #endregion

        }

        private void KeySetsChanged()
        {
            OnPropertyChanged(nameof(KeySet1));
            OnPropertyChanged(nameof(KeySet2));
            OnPropertyChanged(nameof(KeySet3));
            OnPropertyChanged(nameof(KeySet4));
            OnPropertyChanged(nameof(KeySet5));
        }

    }
}
