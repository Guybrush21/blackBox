using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BBCommon;

namespace BBConfigurator.Repository
{
    public class BlackboxRepository
    {
        public void InitWorker()
        {
            var configRepo = new ConfiguratorRepository();
            
            //_worker = new Worker(configRepo.LoadConfiguration());

            //_workerThread = new Thread(_worker.Main);
            //_workerThread.Name = "BlacBoxWorker";

            //_workerThread.Start();

            SerialPort port = new SerialPort(configRepo.LoadConfiguration().SerialPortName, 9600);
            port.Open();
            port.DataReceived += PortOnDataReceived;

        }

        private void PortOnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var port = (SerialPort) sender;

            string s = port.ReadLine();

            Console.WriteLine(s);

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
