using System;
using System.Threading;

namespace SimpleThread
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Master thread");

            var slaveThread = new Thread(state =>
            {
                Console.WriteLine($"Slave thread: state={state}");
                Thread.Sleep(1000);
            });

            slaveThread.Start(5);

            Console.WriteLine("Master thread: Doing other work...");
            Thread.Sleep(10000);

            slaveThread.Join();

            Console.WriteLine("<Enter> to exit");
            Console.ReadKey();
        }
    }
}
