using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using BBCommon;
using BBConfigurator.ViewModel;

namespace BBConfigurator.Mapper
{
    public static class ConfigurationMapper
    {
        public static Configuration MapToModel(ConfigurationViewModel view)
        {
            Configuration configuration = new Configuration();
            
            configuration.Commands = new List<Option>();

            foreach (var option in view.OptionsCollection)
            {
                Option opt = new Option();
                opt.Command = option.Command;
                opt.Enable = option.Enable;
                opt.Order = option.Order;
                opt.Name = option.Name;

                configuration.Commands.Add(opt);
            }

            configuration.SerialPortName = view.PortName;

            return configuration;
        }

        public static ConfigurationViewModel MapToViewModel(Configuration config)
        {
            ConfigurationViewModel view = new ConfigurationViewModel();
            view.OptionsCollection = new ObservableCollection<OptionViewModel>();
         
            foreach (var cmd in config.Commands)
            {
                OptionViewModel option = new OptionViewModel();
                option.Command = cmd.Command;
                option.Enable = cmd.Enable;
                option.Order = cmd.Order;
                option.Name = cmd.Name;

                view.OptionsCollection.Add(option);
            }

            view.ComPortsAvailableList = new ObservableCollection<string>();
            foreach (var p in SerialPort.GetPortNames().ToList())
            {
                view.ComPortsAvailableList.Add(p);
            }

            view.PortName = config.SerialPortName;

            return view;
        }
    }

}
