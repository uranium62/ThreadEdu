using System;
using System.Threading;

namespace CancelationExample
{
    class Program
    {
        static void Main()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem(state =>
            {
                var token = cts.Token;

                for (int i = 0; i < 100; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                    Console.Write(i);
                    Thread.Sleep(200);
                }
            });
            
            Console.WriteLine("<Enter> stop");
            Console.ReadLine();

            cts.Cancel();

            Console.WriteLine("<Enter> exit");
            Console.ReadLine();
        }
    }
}
