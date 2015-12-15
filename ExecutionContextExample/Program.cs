using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace ExecutionContextExample
{
    class Program
    {
        static void Main(string[] args)
        {
            CallContext.LogicalSetData("Name", "Alex");

            ThreadPool.QueueUserWorkItem(state =>
            {
                Console.WriteLine($"Name={CallContext.LogicalGetData("Name")}");
            });
            ExecutionContext.SuppressFlow();

            ThreadPool.QueueUserWorkItem(state =>
            {
                Console.WriteLine($"Name={CallContext.LogicalGetData("Name")}");
            });
            ExecutionContext.RestoreFlow();

            Console.ReadKey();
        }
    }
}
