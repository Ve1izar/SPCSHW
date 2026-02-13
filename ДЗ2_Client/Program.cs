using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Task2_Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Що ви хочете дізнатися?");
            Console.WriteLine("1 - Час (time)");
            Console.WriteLine("2 - Дата (date)");
            Console.Write("Ваш вибір: ");
            string choice = Console.ReadLine();

            string command = "";
            if (choice == "1") command = "time";
            else if (choice == "2") command = "date";
            else
            {
                Console.WriteLine("Невірний вибір.");
                return;
            }

            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 5000;
            IPEndPoint ipEnd = new IPEndPoint(ip, port);

            try
            {
                server.Connect(ipEnd);

                server.Send(Encoding.UTF8.GetBytes(command));

                byte[] buffer = new byte[1024];
                int len = server.Receive(buffer);
                string result = Encoding.UTF8.GetString(buffer, 0, len);

                Console.WriteLine($"Отримані дані від сервера: {result}");

                server.Shutdown(SocketShutdown.Both);
                server.Close();
            }
            catch (SocketException)
            {
                Console.WriteLine("Сервер не доступний.");
            }

            Console.ReadLine();
        }
    }
}