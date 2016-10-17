using BBCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace BBConfigurator.Repository
{
    public class ConfiguratorRepository
    {
        private string savePath = "store.xml";

        public Configuration LoadConfiguration()
        {
            return Deserialize(savePath);
        }

        public void SaveConfiguration(Configuration config)
        {
            Serialize(config, savePath);
        }

        private Configuration Deserialize(string filename)
        {
            if (!File.Exists(filename))
                return GenerateNewXml();

            XmlSerializer x = new XmlSerializer(typeof(Configuration));
            object obj;
            using (XmlTextReader reader = new XmlTextReader(filename))
            {
                obj = x.Deserialize(reader);
            }
            return obj as Configuration;
        }

        private Configuration GenerateNewXml()
        {
            Configuration config = new Configuration();
            config.Commands = new List<Option>();
            config.SerialPortName = String.Empty;
            for (int i = 0; i < 6; i++)
            {
                Option op = new Option() { Command = "", Enable = false, Order = i + 1 };
                config.Commands.Add(op);
            }
            return config;
        }

        private void Serialize(Configuration item, string filename)
        {
            XmlSerializer x = new XmlSerializer(typeof(Configuration));

            using (XmlTextWriter writer = new XmlTextWriter(filename, Encoding.UTF8))
            {
                x.Serialize(writer, item);

                writer.Close();
            }
        }
    }
}