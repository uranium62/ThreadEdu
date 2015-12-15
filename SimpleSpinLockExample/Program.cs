using System;
using System.Linq;
using System.Threading;

namespace SimpleSpinLockExample
{
    class Program
    {
        internal class SimpleSpinLock
        {
            private int _lock = 0;

            public void Enter()
            {
                while (true)
                {
                    if (Interlocked.Exchange(ref _lock, 1) == 0) return;
                }
            }

            public void Leave()
            {
                Volatile.Write(ref _lock, 0);
            }
        }

        static void Main(string[] args)
        {
            var spinLock = new SimpleSpinLock();

            var count = 0;

            Enumerable
                .Range(0, 4)
                .ToList()
                .ForEach(th =>
                {
                    ThreadPool.QueueUserWorkItem((state) =>
                    {
                        for (int i = 0; i < 10000; i++)
                        {
                            spinLock.Enter();

                            count++;
                            count--;

                            spinLock.Leave();
                        }
                    });
                });

            Thread.Sleep(2000);
            Console.WriteLine(count);
            Console.ReadKey();
        }
    }
}
