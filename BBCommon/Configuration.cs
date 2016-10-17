using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;

namespace BBCommon
{
    public class Option
    {
        public int Order { get; set; }
        public String Command { get; set; }
        public bool Enable { get; set; }
        public bool IsExecutable
        {
            get
            {
                return Path.GetExtension(Command).ToLower() == "exe";
                
            }
        }


        public bool CanBeKilled { get; set; }
    }

    public class Configuration
    {
        public String SerialPortName{ get; set; }

        public List<Option> Commands { get; set; }
    }
}
