using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelExample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Parallel.For(1, 20, Prime);

            Parallel.Invoke(
                () => WriteToStore("{msg: \"hello world\"}", "store:1"),
                () => WriteToStore("{msg: \"hello world\"}", "store:2"),
                () => WriteToStore("{msg: \"hello world\"}", "store:3"),
                () => WriteToStore("{msg: \"hello world\"}", "store:4"),
                () => WriteToStore("{msg: \"hello world\"}", "store:5"));

            Console.WriteLine("<Enter>");
            Console.ReadKey();
        }


        static void Prime(int n)
        {
            var accum = 0;
            for (int i = 1; i < n; i++)
            {
                if (n%i == 0) accum++;
            }

            Console.WriteLine(accum > 2 ? $"{n} is prime" : $"{n} isn't prime");
        }

        static void WriteToStore(string data, string store)
        {
            Console.WriteLine($"Write data({data}) to store({store}) completed");
        }
    }
}
