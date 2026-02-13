using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Task1_Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 5000;
            IPEndPoint ipEnd = new IPEndPoint(ip, port);

            try
            {
                server.Connect(ipEnd);

                string message = "Привіт, сервер!";
                byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                server.Send(messageBytes);

                byte[] buffer = new byte[1024];
                int len = server.Receive(buffer);
                string response = Encoding.UTF8.GetString(buffer, 0, len);

                string time = DateTime.Now.ToShortTimeString();
                string serverIp = server.RemoteEndPoint.ToString();
                Console.WriteLine($"Клієнт: О {time} від [{serverIp}] отримано рядок: {response}");

                server.Shutdown(SocketShutdown.Both);
                server.Close();
            }
            catch (SocketException)
            {
                Console.WriteLine("Не вдалося підключитися до сервера. Перевірте, чи він запущений.");
            }
        }
    }
}