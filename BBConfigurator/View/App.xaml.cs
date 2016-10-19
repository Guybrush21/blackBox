using System.Windows;
using System.Windows.Threading;
using Hardcodet.Wpf.TaskbarNotification;

namespace BBConfigurator.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            (MainWindow as MainWindow).ShowBallonTip("Error",e.Exception.Message, BalloonIcon.Error);
            e.Handled = true;
        }
    }
}
