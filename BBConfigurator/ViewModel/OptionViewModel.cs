using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace BBConfigurator
{
    public class ConfigurationViewModel : INotifyPropertyChanged
    {
        private string _port;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<String> ComPortsAvailableList { get; set; }

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

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }

    public class OptionViewModel : INotifyPropertyChanged
    {
        private String command;
        private bool enable;
        private int order;
        private string name; 

        public event PropertyChangedEventHandler PropertyChanged;

        public String Name {
            get {return name;}
            set
            {
                this.name = value;
                this.NotifyPropertyChanged("Name");
            }
        }

        public String Command
        {
            get { return command; }
            set
            {
                this.command = value;
                this.NotifyPropertyChanged("Command");
            }
        }

        public bool Enable
        {
            get { return enable; }
            set
            {
                this.enable = value;
                this.NotifyPropertyChanged("Enable");
            }
        }

        public bool IsExecutable { get; private set; }

        public int Order { get { return order; } set { this.order = value; } }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}