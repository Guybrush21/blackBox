using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlackBox
{
    class Program
    {
        
        static void Main(string[] args)
        {
            try
            {

                SerialPort p = new SerialPort("COM6",9600);
                p.Open();
                string option = "";
                while (option != "9")
                {

                    Console.WriteLine("1. Firefox");
                    Console.WriteLine("2. Explorer");
                    Console.WriteLine("9. Exit");

                    option = Console.ReadLine();

                    p.WriteLine(option);


                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        
    }
}
