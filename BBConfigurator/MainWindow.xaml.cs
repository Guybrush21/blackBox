using BBCommon;
using BBConfigurator.Repository;
using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Threading;
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
            
            _bbRepository = new BlackboxRepository();
            _configuratorRepository = new ConfiguratorRepository();
            
            Init();
            DataContext = _configurationViewModel;
            Closing += OnClosing;

            _bbRepository.InitWorker();
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            SaveConfiguration();
        }

        private void CloseMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            _bbRepository.StopWorker();
            this.Close();
            Application.Current.Shutdown();
        }

        private void Init()
        {
            _configurationViewModel = LoadConfigurationViewModel();
            foreach (var el in _configurationViewModel.OptionsCollection)
            {
                mainContent.Children.Add(new OptionUC() { DataContext = el });
            }
        }


        private ConfigurationViewModel LoadConfigurationViewModel()
        {
            var repo = new ConfiguratorRepository();

            return Mapper.ConfigurationMapper.MapToViewModel(repo.LoadConfiguration());
        }

        private void OnClosing(object sender, CancelEventArgs cancelEventArgs)
        {
            this.Visibility = Visibility.Collapsed;
            cancelEventArgs.Cancel = true;
        }

        private void RestartMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            _bbRepository.ResetWorker();
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