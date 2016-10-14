using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public void InitWorker()
        {
            var configRepo = new ConfiguratorRepository();
            
            //_worker = new Worker(configRepo.LoadConfiguration());

            //_workerThread = new Thread(_worker.Main);
            //_workerThread.Name = "BlacBoxWorker";

            //_workerThread.Start();
            configuration = configRepo.LoadConfiguration();

            SerialPort port = new SerialPort(configuration.SerialPortName, 9600);
            port.Open();
            port.DataReceived += PortOnDataReceived;
            
        }

        private void PortOnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var port = (SerialPort) sender;

            string s = port.ReadLine();

            ExecuteProgram(s);

        }

        private void ExecuteProgram(string s)
        {
            int order;
            if (!Int32.TryParse(s, out order))
                return;

            ProcessStartInfo info = new ProcessStartInfo( configuration.Commands.First(x=>x.Order == order).Command);

            Process.Start(info);


        }

        public void StopWorker()
        {
            //_worker.RequestStop();
            //while (_workerThread.IsAlive) ;
        }

        public void ResetWorker()
        {
            //StopWorker();

            //this.InitWorker();
        }
    }
}
