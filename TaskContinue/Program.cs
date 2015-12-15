using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskContinue
{
    class Program
    {
        static void Main(string[] args)
        {
            var task = new Task<int>(n =>
            {
                int accum = 1;

                for (int i = 1; i < (int) n; i++)
                {
                    accum *= i;
                }

                return accum;
            }, 20);

            // success callback
            task.ContinueWith(t =>
            {
                Console.WriteLine($"Task complete: {t.Result}");
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            task.ContinueWith(t =>
            {
                Console.WriteLine($"It's win: {t.Result}");
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            // error callback
            task.ContinueWith(t =>
            {
                Console.WriteLine($"Error: { t.Exception }");
            }, TaskContinuationOptions.OnlyOnFaulted);

            task.Start();

            Console.WriteLine("<Enter> exit");
            Console.ReadKey();
        }
    }
}
