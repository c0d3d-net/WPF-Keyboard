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

        public static readonly DependencyProperty AllowOnlyNumericButtonsProperty = 
            DependencyProperty.RegisterAttached("AllowOnlyNumericButtons", typeof(bool), typeof(KeyboardControl),new PropertyMetadata(false));

        public static bool GetAllowOnlyNumericButtons(DependencyObject d)
        {
            return (bool)d.GetValue(AllowOnlyNumericButtonsProperty);
        }

        public static void SetAllowOnlyNumericButtons(DependencyObject d, bool value)
        {
            d.SetValue(AllowOnlyNumericButtonsProperty, value);
        }

        //
        private readonly ViewModel dataContext;

        // ------------------------------------------------------------------------------------------------------------------------------------

        public bool OnlyNumericButtons
        {
            get => (bool)GetValue(OnlyNumericButtonsProperty);
            set => SetValue(OnlyNumericButtonsProperty, value);
        }

        public static readonly DependencyProperty OnlyNumericButtonsProperty =
            DependencyProperty.Register("OnlyNumericButtons", typeof(bool), typeof(KeyboardControl), new PropertyMetadata(false));

        // ------------------------------------------------------------------------------------------------------------------------------------

        public override void OnApplyTemplate()
        {
            dataContext.OnlyNumericLayout = OnlyNumericButtons;
        }

        // ------------------------------------------------------------------------------------------------------------------------------------


        public KeyboardControl()
        {
            InitializeComponent();

            dataContext = DataContext as ViewModel;

            if (Application.Current.MainWindow == null)
                return;

            Application.Current.MainWindow.PreviewGotKeyboardFocus += KeyboardVisibility;
            Application.Current.MainWindow.PreviewMouseDown += MainWindow_PreviewMouseDown;
        }

        // ------------------------------------------------------------------------------------------------------------------------------------


        private void MainWindow_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

            if (dataContext == null)
                return;

            IInputElement focusedElement = Keyboard.FocusedElement;


            if (focusedElement == null)
                return;

            dataContext.KeyboardVisibility = focusedElement.GetType().IsSubclassOf(typeof(TextBoxBase)) ||
                                             focusedElement is PasswordBox ? Visibility.Visible : Visibility.Collapsed;

        }

        // ------------------------------------------------------------------------------------------------------------------------------------
    
        private void KeyboardVisibility(object sender, KeyboardFocusChangedEventArgs args)
        {
            if (dataContext == null)
                return;

            dataContext.OnlyNumericLayout = GetAllowOnlyNumericButtons(args.NewFocus as DependencyObject);

            dataContext.KeyboardVisibility = args.NewFocus.GetType().IsSubclassOf(typeof(TextBoxBase)) ||
                                             args.NewFocus is PasswordBox ? Visibility.Visible : Visibility.Collapsed;
        }

        // ------------------------------------------------------------------------------------------------------------------------------------


        private void KeyboardControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(this);
        }

    }
}
