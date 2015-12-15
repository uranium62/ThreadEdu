using System;
using System.Threading;

namespace VilotaleExample
{
    internal static class Program
    {
        // Wrong
        //private static Boolean s_stopWorker = false;

        private static volatile Boolean s_stopWorker = false;

        public static void Main()
        {
            Console.WriteLine("Main: letting worker run for 5 seconds");
            Thread t = new Thread(Worker);
            t.Start();
            Thread.Sleep(5000);
            s_stopWorker = true;
            Console.WriteLine("Main: waiting for worker to stop");
            t.Join();
        }

        private static void Worker(Object o)
        {
            Int32 x = 0;
            while (!s_stopWorker) x++;
            Console.WriteLine("Worker: stopped when x={0}", x);
        }
    }
}