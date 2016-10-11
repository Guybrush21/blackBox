﻿using System;
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
        public OptionUC()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(
                Environment.SpecialFolder.ProgramFiles);
            dialog.Multiselect = false;

            if (dialog.ShowDialog() == true)
            {
                commandTxt.Text = dialog.FileName;
            }
        }
    }
}