using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace BestPatternDelay
{
    static class Ext
    {
        public static T DeepClone<T>(this T self)
        {
            if (!typeof (T).IsSerializable)
            {
                throw new ArgumentException("Type must be seriazible");
            }
            if (self == null)
            {
                return default(T);
            }

            using (var ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                bf.Serialize(ms, self);
                ms.Seek(0, SeekOrigin.Begin);
                return (T) bf.Deserialize(ms);
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Checking status every 2 seconds");
            Status();
            Console.ReadLine();
        }

        private static async void Status()
        {
            while (true)
            {
                Console.WriteLine("Checking status at {0}", DateTime.Now);
                await Task.Delay(2000);
            }
        }
    }
}
