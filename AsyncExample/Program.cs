using System;
using System.IO.Pipes;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace AsyncExample
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        private static async Task<string> IssueClienRequestAsync(string serverName, string message)
        {
            using (var pipe = new NamedPipeClientStream(serverName, "PipeName",
                PipeDirection.InOut, PipeOptions.Asynchronous | PipeOptions.WriteThrough))
            {
                pipe.Connect(); 
                pipe.ReadMode = PipeTransmissionMode.Message;

                byte[] request = Encoding.UTF8.GetBytes(message);
                await pipe.WriteAsync(request, 0, request.Length);

                byte[] response = new byte[1000];
                Int32 bytesRead = await pipe.ReadAsync(response, 0, response.Length);
                return Encoding.UTF8.GetString(response, 0, bytesRead);
            }
        }


        private static async void StartServer()
        {
            while (true)
            {
                var pipe = new NamedPipeServerStream("PipeName", PipeDirection.InOut, 1,
                PipeTransmissionMode.Message, PipeOptions.Asynchronous |
                PipeOptions.WriteThrough);

                await Task.Factory.FromAsync(pipe.BeginWaitForConnection, pipe.EndWaitForConnection, null);

               //ServiceClientRequestAsync(pipe);
            }
        }
    }
}
