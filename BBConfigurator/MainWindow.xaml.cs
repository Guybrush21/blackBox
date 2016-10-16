using BBConfigurator.Repository;
using System;
using System.ComponentModel;
using System.Windows;

namespace BBConfigurator
{
    public partial class MainWindow : Window
    {
        private ConfigurationViewModel _configurationViewModel;

        private BlackboxRepository _bbRepository;
        private ConfiguratorRepository _configuratorRepository;

        public MainWindow()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Init();

            DataContext = _configurationViewModel;

            Closing += OnClosing;

            _bbRepository.InitBlackbox();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Taskbar.ShowBalloonTip("Error", (e.ExceptionObject as Exception).Message, Hardcodet.Wpf.TaskbarNotification.BalloonIcon.Error);
            //MessageBox.Show((e.ExceptionObject as Exception).Message);
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            SaveConfiguration();
        }

        private void CloseMenuItem_OnClick(object sender, RoutedEventArgs e)
        {            
            this.Close();
            Application.Current.Shutdown();
        }

        private void Init()
        {
            _bbRepository = new BlackboxRepository();
            _configuratorRepository = new ConfiguratorRepository();

            _configurationViewModel = LoadConfigurationViewModel();
            foreach (var el in _configurationViewModel.OptionsCollection)
            {
                mainContent.Children.Add(new OptionUC() { DataContext = el });
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
        }

        private void RestartMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            _bbRepository.Restart();
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

        private void PortComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _configurationViewModel.PortName = PortComboBox.SelectedItem as String;
        }
    }
}