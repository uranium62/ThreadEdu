using System;
using System.Threading;

namespace BackgroundThread
{
    class Program
    {
        static void Main()
        {
            Thread slaveThread = new Thread(() =>
            {
                Thread.Sleep(10000);
                Console.WriteLine("Slave thread");
            });

            slaveThread.IsBackground = true;
            slaveThread.Start();

            Console.WriteLine("Master thread completed");
        }
    }
}
