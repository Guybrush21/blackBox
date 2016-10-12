using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BBCommon;
using BBConfigurator.Commands;
using BBConfigurator.Repository;
using Hardcodet.Wpf.TaskbarNotification;

namespace BBConfigurator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TaskbarIcon tb;
        ConfigurationViewModel options; 

        public MainWindow()
        {
            InitializeComponent();
            
            Closing += OnClosing;

            options = LoadOptionFromXml();
            foreach (var el in options.OptionsCollection)
            {
                mainContent.Children.Add(new OptionUC() {DataContext = el});
            }

        }
        

        private ConfigurationViewModel LoadOptionFromXml()
        {
            var repo = new ConfiguratorRepository();

            return Mapper.ConfigurationMapper.MapToViewModel(repo.LoadConfiguration());
        }


        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            SaveConfiguration();
        }

        private void SaveConfiguration()
        {
            var repo = new ConfiguratorRepository();

            var configToSave = Mapper.ConfigurationMapper.MapToModel(options);

            repo.SaveConfiguration(configToSave);
        }

        private void ShowHideMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            if(this.Visibility == Visibility.Visible)
                this.Visibility = Visibility.Collapsed;
            else
                this.Visibility = Visibility.Visible;
        }

        private void CloseMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
        }

        private void OnClosing(object sender, CancelEventArgs cancelEventArgs)
        {
            this.Visibility = Visibility.Collapsed;
            cancelEventArgs.Cancel = true;
        }

        private void RestartMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}


