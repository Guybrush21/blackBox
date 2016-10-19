using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace BBConfigurator.ViewModel
{
    public class ConfigurationViewModel : INotifyPropertyChanged
    {
        private string _port;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> ComPortsAvailableList { get; set; }

        public ObservableCollection<OptionViewModel> OptionsCollection { get; set; }

        public String PortName
        {
            get { return _port; }
            set
            {
                _port = value;
                NotifyPropertyChanged("PortName");
            }
        }

        private void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}