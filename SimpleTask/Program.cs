using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Master thread");

            var task = new Task(() =>
            {
                Console.WriteLine("You win!");
            });
                    
            task.Start();

            Task.Run(() =>
            {
                Console.WriteLine("You win!");
            });


            Console.WriteLine("<Enter> exit");
            Console.ReadKey();
        }
    }
}
