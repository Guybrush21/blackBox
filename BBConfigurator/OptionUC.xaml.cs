using System;
using System.Collections.Generic;
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
using Microsoft.Win32;

namespace BBConfigurator
{
    /// <summary>
    /// Interaction logic for OptionUC.xaml
    /// </summary>
    public partial class OptionUC : UserControl
    {
        private Worker.BlackboxListner _bbRepository;

        public OptionUC()
        {
            InitializeComponent();
        }

        public OptionUC(Worker.BlackboxListner _bbRepository)
        {
            InitializeComponent();
            this._bbRepository = _bbRepository;
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(
                Environment.SpecialFolder.ProgramFiles);
            dialog.Multiselect = false;

            if (dialog.ShowDialog() == true)
            {
                (this.DataContext as OptionViewModel).Command = dialog.FileName;
            }
        }

        private void TryButton_Click(object sender, RoutedEventArgs e)
        {
            if(!String.IsNullOrEmpty(commandTxt.Text))
                _bbRepository.TestAction(orderLabel.Content.ToString());
        }
    }
}
