using System;
using System.Windows;
using System.Windows.Controls;
using BBConfigurator.ViewModel;
using Microsoft.Win32;

namespace BBConfigurator.View
{
    /// <summary>
    /// Interaction logic for OptionUC.xaml
    /// </summary>
    public partial class OptionUC : UserControl
    {
        private Worker.IBlackboxListner _blackboxListner;

        public OptionUC()
        {
            InitializeComponent();
        }

        public OptionUC(Worker.IBlackboxListner blackboxListner)
        {
            InitializeComponent();
            this._blackboxListner = blackboxListner;
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
                _blackboxListner.TestAction(orderLabel.Content.ToString());
        }
    }
}
