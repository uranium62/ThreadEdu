using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskTimeoutPattern
{
    public static class TaskExt
    {
        public static async Task<T> WithTimeout<T>(this Task<T> task, TimeSpan timeout)
        {
            if (task.IsCompleted || timeout == Timeout.InfiniteTimeSpan)
            {
                return await task;
            }
            var cts = new CancellationTokenSource();
            if (await Task.WhenAny(task, Task.Delay(timeout, cts.Token)) == task)
            {
                cts.Cancel();
                return await task;
            }

            task.ContinueWith(_ => { }, TaskContinuationOptions.ExecuteSynchronously);
            throw new TimeoutException();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
