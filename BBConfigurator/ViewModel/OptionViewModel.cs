using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBConfigurator
{

    public class ConfigurationViewModel 
    {
        public ObservableCollection<OptionViewModel> OptionsCollection { get; set; }
    }

    public class OptionViewModel : INotifyPropertyChanged
    {
        private String command;
        private bool enable;
        private int order;

        public int Order { get { return order; } set { this.order = value; } }

        public String Command { get{return command;}
            set
            {
                this.command = value;
                this.NotifyPropertyChanged("Command");
            }
        }
        public bool Enable {
            get { return enable;}
            set
            {
                this.enable = value;
                this.NotifyPropertyChanged("Enable");
            }
        }
        public bool IsExecutable { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
