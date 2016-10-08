using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlackBox
{
    class Program
    {
        static private Process ff, expolore;


        static void Main(string[] args)
        {
            try
            {

                string option = "";
                while (option != "3")
                {
                    Console.WriteLine("1. Firefox");
                    Console.WriteLine("2. Explorer");
                    Console.WriteLine("3. Exit");

                    option = Console.ReadLine();
                    switch (option)
                    {
                        case "1":
                            ToggleFF();
                            break;
                        case "2":
                            ToggleExplorer();
                            break;
                        case "3":
                            break;

                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        private static void ToggleExplorer()
        {
            if(expolore == null)
                expolore = Process.Start(@"explorer.exe");
            else
            {
                try
                {

                    expolore.CloseMainWindow();
                    expolore.Close();
                    expolore = null;
                }
                catch(Exception e)
                {
                    expolore.Kill();
                    Console.WriteLine(e.Message);
                }
            }

            foreach (var p in Process.GetProcessesByName("explorer"))
            {
                Console.WriteLine(p.MainWindowTitle);
            }
        }

        private static void ToggleFF()
        {
            if (ff == null)
                ff = Process.Start(@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe");
            else
            {
                ff.CloseMainWindow();
                ff.Close();
                ff = null;
            }
        }
    }
}
