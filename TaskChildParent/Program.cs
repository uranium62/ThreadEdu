using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskChildParent
{
    class Program
    {
        static void Main(string[] args)
        {
            var parent = new Task<int[]>(() =>
            {
                var results = new int[3];

                new Task(() => results[0] = Sum(10), TaskCreationOptions.AttachedToParent).Start();
                new Task(() => results[1] = Sum(20), TaskCreationOptions.AttachedToParent).Start();
                new Task(() => results[2] = Sum(30), TaskCreationOptions.AttachedToParent).Start();

                return results;
            });

            parent.ContinueWith(t => Array.ForEach(t.Result, Console.WriteLine));
            parent.Start();

            Console.WriteLine("<Enter> exit");
            Console.ReadKey();
        }

        static int Sum(int n)
        {
            int accum = 1;
            for (int i = 1; i < n; i++)
            {
                accum *= i;
            }
            return accum;
        }
    }
}
