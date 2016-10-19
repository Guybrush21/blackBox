using System;
using System.ComponentModel;

namespace BBConfigurator.ViewModel
{
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

        private void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}