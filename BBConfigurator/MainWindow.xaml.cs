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
        
        private Worker _worker;
        private Thread _workerThread;

        public MainWindow()
        {
            InitializeComponent();
            
            Init();
            DataContext = _configurationViewModel;
            Closing += OnClosing;

            InitWorker();
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            SaveConfiguration();
        }

        private void CloseMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            StopWorker();
            this.Close();
            Application.Current.Shutdown();
        }

        private void StopWorker()
        {
            _worker.RequestStop();
            while (_workerThread.IsAlive) ;
        }

        private void Init()
        {
            _configurationViewModel = LoadConfigurationViewModel();
            foreach (var el in _configurationViewModel.OptionsCollection)
            {
                mainContent.Children.Add(new OptionUC() { DataContext = el });
            }


        }

        private void InitWorker()
        {
            _worker = new Worker();
            _workerThread = new Thread(_worker.Main);
            _workerThread.Name = "BlacBoxWorker";

            _workerThread.Start();
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
            _worker.RequestStop();
            while (_workerThread.IsAlive) ;
            _workerThread.Start();
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