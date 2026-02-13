using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace NetworkTasks
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Скачати зображення за посиланням");
                Console.WriteLine("2. Отримати дані з API");
                Console.WriteLine("0. Вихід");
                Console.Write("\nВаш вибір: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await DownloadImageTask();
                        break;
                    case "2":
                        await ApiDataTask();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Невірний вибір. Натисніть Enter");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private static async Task DownloadImageTask()
        {
            Console.Clear();
            Console.WriteLine("Завантаження зображення");

            Console.Write("Введіть URL картинки: ");
            string url = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(url))
            {
                Console.WriteLine("URL не може бути порожнім.");
                Console.ReadKey();
                return;
            }

            Console.Write("Введіть шлях для збереження (формат типу D:\\Images або просто Enter для поточної папки): ");
            string directoryPath = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(directoryPath))
            {
                directoryPath = Directory.GetCurrentDirectory();
            }

            Console.Write("Введіть назву файлу (з розширенням, типу image.jpg): ");
            string fileName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(fileName))
            {
                Console.WriteLine("Назва файлу не вказана. Використовуємо default.jpg");
                fileName = "default.jpg";
            }

            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string fullPath = Path.Combine(directoryPath, fileName);

                Console.WriteLine("Завантаження");

                byte[] fileBytes = await client.GetByteArrayAsync(url);

                await File.WriteAllBytesAsync(fullPath, fileBytes);

                Console.WriteLine($"\nУспіх! Файл збережено за адресою:\n{fullPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nПомилка при завантаженні: {ex.Message}");
            }

            Console.WriteLine("\nНатисніть Enter для повернення в меню");
            Console.ReadLine();
        }

        private static async Task ApiDataTask()
        {
            bool backToMenu = false;
            while (!backToMenu)
            {
                Console.Clear();
                Console.WriteLine("Оберіть ресурс для відображення:");
                Console.WriteLine("1. posts");
                Console.WriteLine("2. comments");
                Console.WriteLine("3. albums");
                Console.WriteLine("4. photos");
                Console.WriteLine("5. todos");
                Console.WriteLine("6. users");
                Console.WriteLine("0. Назад у головне меню");
                Console.Write("\nВаш вибір: ");

                string choice = Console.ReadLine();
                string resource = "";

                switch (choice)
                {
                    case "1": resource = "posts"; break;
                    case "2": resource = "comments"; break;
                    case "3": resource = "albums"; break;
                    case "4": resource = "photos"; break;
                    case "5": resource = "todos"; break;
                    case "6": resource = "users"; break;
                    case "0": backToMenu = true; continue;
                    default:
                        Console.WriteLine("Невірний вибір.");
                        Console.ReadKey();
                        continue;
                }

                try
                {
                    string url = $"https://jsonplaceholder.typicode.com/{resource}";

                    Console.WriteLine($"\nЗапит до {url} ...");

                    string jsonResponse = await client.GetStringAsync(url);

                    Console.WriteLine("--- Отримані дані ---");
                    Console.WriteLine(jsonResponse);
                    Console.WriteLine("---------------------");
                    Console.WriteLine("Дані успішно отримано.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nПомилка отримання даних: {ex.Message}");
                }

                Console.WriteLine("\nНатисніть Enter, щоб продовжити");
                Console.ReadLine();
            }
        }
    }
}