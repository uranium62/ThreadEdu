using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TimerExample
{
    class Program
    {
        private static Timer _timer;

        static void Main(string[] args)
        {
            Console.WriteLine("Checking status every 2 seconds");

            _timer = new Timer(Status, null, Timeout.Infinite, Timeout.Infinite);
            _timer.Change(0, Timeout.Infinite);

            Console.ReadLine();
        }

        private static void Status(Object state)
        {

            Console.WriteLine("In Status at {0}", DateTime.Now);
            Thread.Sleep(1000); 

            _timer.Change(2000, Timeout.Infinite);
        }
    }
}
