using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Task2_Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 5000;
            IPEndPoint ipEnd = new IPEndPoint(ip, port);

            server.Bind(ipEnd);
            server.Listen(5);

            Console.WriteLine("Server Time/Date service started...");

            while (true)
            {
                Socket client = server.Accept();

                byte[] buffer = new byte[1024];
                int len = client.Receive(buffer);
                string request = Encoding.UTF8.GetString(buffer, 0, len).ToLower();

                string response = "";

                if (request == "time")
                {
                    response = DateTime.Now.ToLongTimeString();
                }
                else if (request == "date")
                {
                    response = DateTime.Now.ToShortDateString();
                }
                else
                {
                    response = "Невідома команда";
                }

                Console.WriteLine($"Запит від {client.RemoteEndPoint}: {request}. Відповідь: {response}");

                client.Send(Encoding.UTF8.GetBytes(response));

                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
        }
    }
}