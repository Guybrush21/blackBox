using BBCommon;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;

namespace BBConfigurator.Repository
{
    public delegate void ActionOccurredHandler(object sender, ActionEventArgs e);

    public class ActionEventArgs : EventArgs
    {
        public enum ActionEnum
        {
            ProcessStarted, 
            ProcessStopped, 
            BlackBoxRestarted,
            ExceptionHappen
        }

        public ActionEnum Action { get; set; }
        public String Program { get; set; }
    }

    public class BlackboxRepository
    {
        static private Dictionary<int, Process> processList;
        private Configuration configuration;
        private SerialPort port;

        public event ActionOccurredHandler OnAction;

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
                throw new Exception("Error occured in listenting for Serial Port: " + configuration.SerialPortName);
            }
        }

        public void Restart()
        {
            if (port.IsOpen)
                port.Close();

            InitBlackbox();
            
            if(OnAction != null)
                OnAction(this, new ActionEventArgs(){Action = ActionEventArgs.ActionEnum.BlackBoxRestarted });

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

        private void PortOnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var port = (SerialPort)sender;

            string s = port.ReadLine();

            ExecuteCommand(s);
        }

        private void StartProcess(Option option)
        {
            var process = new Process();

            ProcessStartInfo info = new ProcessStartInfo(option.Command);
            process.StartInfo = info;
            process.Start();
            processList.Add(option.Order, process);

            if (OnAction != null)
                OnAction(this, new ActionEventArgs()
                {
                    Action = ActionEventArgs.ActionEnum.ProcessStarted,
                    Program = process.ProcessName
                });

        }

        private void StopProcess(Option option)
        {
            var process = processList[option.Order];
            string procssName = process.ProcessName;

            if (process.HasExited)
            {
                process = Process.GetProcessesByName(process.ProcessName).OrderByDescending(x => x.StartTime).First();
            }
            if (process.CloseMainWindow())
            {
                process.WaitForExit(4000);
                process.Close();
            }

            processList.Remove(option.Order);

            if (OnAction != null)
                OnAction(this, new ActionEventArgs()
                {
                    Action = ActionEventArgs.ActionEnum.ProcessStopped,
                    Program = procssName
                });
        }
    }
}