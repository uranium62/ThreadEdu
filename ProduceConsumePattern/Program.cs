using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProduceConsumePattern
{
    public class ProducerConsumer<T> : IDisposable
    {
        private readonly Action<T> _job;
        private readonly BlockingCollection<T> _queue;
        private readonly Task[] _workers;

        public ProducerConsumer(
            Action<T> job,
            int degreeOfParallelism,
            int capacity = 1024)
        {
            _job = job;
            _queue = new BlockingCollection<T>(capacity);

            _workers = Enumerable
                .Range(1, degreeOfParallelism)
                .Select(_ => Task.Factory.StartNew(Worker, TaskCreationOptions.LongRunning))
                .ToArray();
        }

        public void Process(T item)
        {
            _queue.Add(item);
        }

        public void CompleteProcessing()
        {
            _queue.CompleteAdding();
        }
         
        public void Dispose()
        {
            if (!_queue.IsAddingCompleted)
            {
                _queue.CompleteAdding();
            }

            Task.WaitAll(_workers);

            _queue.Dispose();
        }

        public void Worker()
        {
            foreach (var item in _queue.GetConsumingEnumerable())
            {
                _job(item);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Action<string> job = item =>
            {
                Console.WriteLine("[{0}]: Processing item '{1}'", Thread.CurrentThread.ManagedThreadId, item);
            };

            var queue = new ProducerConsumer<string>(job, Environment.ProcessorCount);

            for (int i = 0; i < 10; i++)
            {
                string item = "Item " + (i + 1);
                Console.WriteLine("[{0}]: Adding item '{1}'", Thread.CurrentThread.ManagedThreadId, item);
                queue.Process("Item " + (i + 1));
            }

            Console.WriteLine("[{0}]: Complete adding new elements", Thread.CurrentThread.ManagedThreadId);

            queue.CompleteProcessing();
            queue.Dispose();

            Console.ReadKey();
        }
    }
}
