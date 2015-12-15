using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InterlockedExample
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;

            Enumerable
                .Range(0, 8)
                .ToList()
                .ForEach(t =>
                {
                    ThreadPool.QueueUserWorkItem((state) =>
                    {
                        for (int i = 0; i < 100000; i++)
                        {
                            count++;
                            count--;
                        }
                    });
                });

            Thread.Sleep(5000);
            Console.WriteLine(count);

            count = 0;

            Enumerable
                .Range(0, 8)
                .ToList()
                .ForEach(t =>
                {
                    ThreadPool.QueueUserWorkItem((state) =>
                    {
                        for (int i = 0; i < 100000; i++)
                        {
                            Interlocked.Add(ref count, 1);
                            Interlocked.Add(ref count, -1);
                        }
                    });
                });

            Thread.Sleep(5000);
            Console.WriteLine(count);
            Console.ReadKey();
        }
    }
}
