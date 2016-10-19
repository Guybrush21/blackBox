using System;
using System.ComponentModel;
using System.Windows;
using BBConfigurator.Repository;
using BBConfigurator.ViewModel;
using BBConfigurator.Worker;
using Hardcodet.Wpf.TaskbarNotification;

namespace BBConfigurator.View
{
    public partial class MainWindow : Window
    {
        private IBlackboxListner _blackboxListner;
        private ConfigurationViewModel _configurationViewModel;
        private IConfiguratorRepository _configuratorRepository;

        private const string StopProcess = "{0} has been closed.";
        private const string StartProcess = "{0} has been started.";
        private const string BlackBRestart = "BlackBox have been restarted.";

        public MainWindow()
        {
            InitializeComponent();
            
            Init();
            
            DataContext = _configurationViewModel;
            
            Closing += OnClosing;

            _blackboxListner.OnAction += BlackboxListnerOnOnAction;
            _blackboxListner.InitBlackbox();
        }

        public void ShowBallonTip(string title, string message, BalloonIcon icon)
        {
            Taskbar.ShowBalloonTip(title, message, icon);
        }


        private void BlackboxListnerOnOnAction(object sender, ActionEventArgs e)
        {
            String baloon = GetBallonText(e);

            Taskbar.ShowBalloonTip("Event",baloon, BalloonIcon.None);
        }

        private static string GetBallonText(ActionEventArgs e)
        {
            string baloon = string.Empty;
            switch (e.Action)
            {
                case ActionEventArgs.ActionEnum.BlackBoxRestarted:
                    baloon = BlackBRestart;
                    break;
                case ActionEventArgs.ActionEnum.ProcessStarted:
                    baloon = String.Format(StartProcess, e.Message);
                    break;
                case ActionEventArgs.ActionEnum.ProcessStopped:
                    baloon = String.Format(StopProcess, e.Message);
                    break;
                case ActionEventArgs.ActionEnum.ExceptionHappen:
                    baloon = "An error occured";
                    break;
            }
            return baloon;
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            SaveConfiguration();

            _blackboxListner.Restart();

            Taskbar.ShowBalloonTip("Save", "Configuration saved successfully", BalloonIcon.Info);
        }

        private void CloseMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
        }
        
        private void Init()
        {
            _blackboxListner = new BlackboxListner();
            _configuratorRepository = new ConfiguratorRepository();

            _configurationViewModel = LoadConfigurationViewModel();
            foreach (var el in _configurationViewModel.OptionsCollection)
            {
                mainContent.Children.Add(new OptionUC(_blackboxListner) { DataContext = el });
            }
        }

        private ConfigurationViewModel LoadConfigurationViewModel()
        {
            return Mapper.ConfigurationMapper.MapToViewModel(_configuratorRepository.LoadConfiguration());
        }

        private void OnClosing(object sender, CancelEventArgs cancelEventArgs)
        {
            this.Visibility = Visibility.Collapsed;
            cancelEventArgs.Cancel = true;
            Taskbar.ShowBalloonTip("Minimized", "The application is still running minimized", BalloonIcon.Info);

        }

        private void PortComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _configurationViewModel.PortName = PortComboBox.SelectedItem as String;
        }

        private void RestartMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            _blackboxListner.Restart();
            Taskbar.ShowBalloonTip("Restart", "BlackBox has restarted successfully", BalloonIcon.Info);
        }

        private void SaveConfiguration()
        {
            var repo = new ConfiguratorRepository();

            var configToSave = Mapper.ConfigurationMapper.MapToModel(_configurationViewModel);

            repo.SaveConfiguration(configToSave);
        }

        private void ShowHideMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
                this.Visibility = Visibility.Collapsed;
            else
                this.Visibility = Visibility.Visible;
        }
    }
}