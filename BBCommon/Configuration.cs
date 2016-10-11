using System;
using System.Collections.Generic;
using System.IO;

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

    }

    public class Configuration
    {
        public List<Option> Commands { get; set; }

    }
}
