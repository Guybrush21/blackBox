using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace BBConfigurator.Commands
{
    public  class CustomCommands
    {
        public static RoutedUICommand CloseWindow { get; set; }

        public static void CloseWindowExecute(object sender, ExecutedRoutedEventArgs e)
        {
            ((Window)e.Parameter).Close();
        }
        public static void CanExecuteIfParameterIsNotNull(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        static CustomCommands()
        {
            CloseWindow = new RoutedUICommand();
            CloseWindow.InputGestures.Add(new KeyGesture(Key.F4, ModifierKeys.Alt));
            
        }

       

    }
}