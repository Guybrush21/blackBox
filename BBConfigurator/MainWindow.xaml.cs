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
            tb = (TaskbarIcon)FindResource("NotifyTrayTaskbarIcon");
            

            options = LoadOptionFromXml();

           // this.DataContext = options;

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
    }
}
