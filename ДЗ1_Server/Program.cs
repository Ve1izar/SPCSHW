using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Task1_Server
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

            Console.WriteLine("Server started. Waiting for connection...");

            while (true)
            {
                Socket client = server.Accept();

                byte[] buffer = new byte[1024];
                int len = client.Receive(buffer);
                string receivedText = Encoding.UTF8.GetString(buffer, 0, len);

                string time = DateTime.Now.ToShortTimeString();
                string clientIp = client.RemoteEndPoint.ToString();
                Console.WriteLine($"Сервер: О {time} від [{clientIp}] отримано рядок: {receivedText}");

                string reply = "Привіт, клієнт!";
                byte[] replyBytes = Encoding.UTF8.GetBytes(reply);
                client.Send(replyBytes);

                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
        }
    }
}