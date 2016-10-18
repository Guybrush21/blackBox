using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using BBCommon;
using BBConfigurator.Repository;

namespace BBConfigurator.Worker
{
    public class BlackboxListner
    {
        static private Dictionary<int, Process> _processList;
        private Configuration _configuration;
        private SerialPort _port;

        public event ActionOccurredHandler OnAction;

        public void InitBlackbox()
        {
            var configRepo = new ConfiguratorRepository();
            _configuration = configRepo.LoadConfiguration();
            _processList = new Dictionary<int, Process>();

            try
            {
                if (!String.IsNullOrEmpty(_configuration.SerialPortName))
                {
                    _port = new SerialPort(_configuration.SerialPortName, 9600);

                    _port.Open();
                    _port.DataReceived += PortOnDataReceived;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured in listenting for Serial Port: " + _configuration.SerialPortName);
            }
        }

        public void TestAction(string command)
        {
            ExecuteCommand(command);
        }

        public void Restart()
        {
            if (_port.IsOpen)
                _port.Close();

            InitBlackbox();
            if(OnAction != null)
                OnAction(this, new ActionEventArgs() { Action = ActionEventArgs.ActionEnum.BlackBoxRestarted });
        }

        private void ExecuteCommand(string s)
        {
            int order;
            if (!Int32.TryParse(s, out order))
                return;

            Option option = _configuration.Commands.First(x => x.Order == order);

            if (!_processList.ContainsKey(order))
                StartProcess(option);
            else
                StopProcess(option);
        }

        private void PortOnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var port = (SerialPort)sender;

            string command = port.ReadLine();

            ExecuteCommand(command);
        }

        private void StartProcess(Option option)
        {
            var process = new Process();

            ProcessStartInfo info = new ProcessStartInfo(option.Command);
            process.StartInfo = info;

            process.Start();

            _processList.Add(option.Order, process);

            if (OnAction != null)
                OnAction(this, new ActionEventArgs()
                {
                    Action = ActionEventArgs.ActionEnum.ProcessStarted,
                    Message = option.Name
                });
        }

        private void StopProcess(Option option)
        {
            var process = GetRunningProcess(option);
         
            if (process.CloseMainWindow())
            {
                process.WaitForExit(4000);
                process.Close();
            }

            _processList.Remove(option.Order);

            if (OnAction != null)
                OnAction(this, new ActionEventArgs()
                {
                    Action = ActionEventArgs.ActionEnum.ProcessStopped,
                    Message = option.Name
                });
        }

        private Process GetRunningProcess(Option option)
        {
            var process = _processList[option.Order];

            if (process.HasExited)
            {
                process = Process.GetProcessesByName(
                    Path.GetFileNameWithoutExtension(option.Command)).OrderByDescending(x => x.StartTime).First();
            }
            return process;
        }
    }
}