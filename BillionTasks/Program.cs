using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillionTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 1000000;

            for (int i = 0; i < count; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    Task.Delay(1000);

                    Task.Delay(10000);

                    Task.Delay(100000);
                });
            }

            Console.WriteLine("complete");
            Console.ReadKey();
        }
    }
}
