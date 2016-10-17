﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Interop;
using BBCommon;

namespace BBConfigurator.Repository
{
    public class BlackboxRepository
    {
        private Configuration configuration;
        static private Dictionary<int, Process> processList;
        private SerialPort port;

        public void InitBlackbox()
        {
            var configRepo = new ConfiguratorRepository();
            configuration = configRepo.LoadConfiguration();
            processList = new Dictionary<int, Process>();

            try
            {
                if (!String.IsNullOrEmpty(configuration.SerialPortName))
                {
                    port = new SerialPort(configuration.SerialPortName, 9600);

                    port.Open();
                    port.DataReceived += PortOnDataReceived;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Impossible to open Serial Port: " + configuration.SerialPortName);
            }

        }

        public void Restart()
        {
            if(port.IsOpen)
                port.Close();

            InitBlackbox();
        }

        private void PortOnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var port = (SerialPort)sender;

            string s = port.ReadLine();

            ExecuteCommand(s);

        }

        private void ExecuteCommand(string s)
        {
            int order;
            if (!Int32.TryParse(s, out order))
                return;

            Option option = configuration.Commands.First(x => x.Order == order);

            if (!processList.ContainsKey(order))
                StartProcess(option);
            else
                StopProcess(option);
        }

        private void StartProcess(Option option)
        {
            var process = new Process();

            ProcessStartInfo info = new ProcessStartInfo(option.Command);
            process.StartInfo = info;
            process.Start();
            processList.Add(option.Order, process);
        }

        private void StopProcess(Option option)
        {
            var process = processList[option.Order];

            if (process.HasExited)
            {
                var exeName = Path.GetFileNameWithoutExtension(option.Command);
                process = Process.GetProcessesByName(exeName).First(p => p.StartTime == Process.GetProcessesByName(exeName).Max(x=>x.StartTime));
            }
            try
            {
                if (process.CloseMainWindow())
                {
                    process.WaitForExit(400);
                    process.Close();
                }
                
                    
                    
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            processList.Remove(option.Order);

        }

    }
}
