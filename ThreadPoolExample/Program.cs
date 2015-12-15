using System;
using System.Threading;

namespace ThreadPoolExample
{
    class Program
    {
        private static void Main()
        {
            Console.WriteLine("Master thread");
            ThreadPool.QueueUserWorkItem(state =>
            {
                Console.WriteLine($"Slave thread: state={state}");
                Thread.Sleep(1000);
            }, 5);
            
            Console.WriteLine("Master thread: Doing other work here...");
            Thread.Sleep(10000);

            Console.WriteLine("<Enter> exit");
            Console.ReadKey();
        }
    }
}
