using System;
using System.Collections.Generic;
using System.IO;

namespace BBCommon
{
    public class Option
    {

        public Option(int order, string command, bool enable)
        {
            if(command == null || enable == null)
                throw new Exception("Cannot be initialize object becuase null parameter");

            this.Order = order;
            this.Command = command;
            this.Enable = enable;

            this.IsExecutable = Path.GetExtension(command).ToLower() == "exe";
        }
        
        public int Order { get; set; }
        public String Command { get; set; }
        public bool Enable { get; set; }
        public bool IsExecutable { get; set; }
    }

    public class Configuration
    {
        public List<Option> Commands { get; set; }
        
    }
}
