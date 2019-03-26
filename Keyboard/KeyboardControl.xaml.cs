/*
 * Author : İbrahim GAZALOĞLU 
 * Date   : 23.03.2019
 * Email  : ibrahimgazaloglu@gmail.com  
 *
 */

using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;


namespace WPFTouchscreenKeyboard
{
    /// <summary>
    /// Interaction logic for GazalogluXaml
    /// </summary>
    public partial class KeyboardControl : UserControl
    {
        public KeyboardControl()
        {
            InitializeComponent();
            
            if (Application.Current.MainWindow == null)
                return;

            Application.Current.MainWindow.PreviewGotKeyboardFocus += KeyboardVisibility;
            Application.Current.MainWindow.PreviewMouseDown += MainWindow_PreviewMouseDown;
        }

        private void MainWindow_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var dataContext = DataContext as ViewModel;

            if (dataContext == null)
                return;

            IInputElement focusedElement = Keyboard.FocusedElement;

            if (focusedElement == null)
                return;

            dataContext.KeyboardVisibility = focusedElement.GetType().IsSubclassOf(typeof(TextBoxBase)) ||
                                             focusedElement is PasswordBox ? Visibility.Visible : Visibility.Collapsed;

        }

        private void KeyboardVisibility(object sender, KeyboardFocusChangedEventArgs args)
        {
            var dataContext = DataContext as ViewModel;

            if (dataContext == null)
                return;

            dataContext.KeyboardVisibility = args.NewFocus.GetType().IsSubclassOf(typeof(TextBoxBase)) ||
                                             args.NewFocus is PasswordBox ? Visibility.Visible : Visibility.Collapsed;
        }

        private void KeyboardControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(this);
        }

    }
}
