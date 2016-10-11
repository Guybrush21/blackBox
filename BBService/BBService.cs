using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace BBService
{
    public partial class BBService : ServiceBase
    {
        public BBService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            while (true)
            {
                System.IO.Ports.SerialPort p = new SerialPort("COM3",9600);

                var command = p.ReadByte();

                switch (command)
                {
                        
                }
            }
        }

        protected override void OnStop()
        {
        }
    }
}
