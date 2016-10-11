using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using BBCommon;

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
                
                configuration.Commands.Add(opt);
            }
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

                view.OptionsCollection.Add(option);
            }

            return view;
        }
    }

}
