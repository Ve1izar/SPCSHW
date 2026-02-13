using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ДЗ_String
{
    internal class Program
    {
        static void Task1() {
            Console.Write("Введіть основний рядок: ");
            string mainStr = Console.ReadLine();

            Console.Write("Введіть рядок для вставки: ");
            string insertStr = Console.ReadLine();

            Console.Write("Введіть позицію для вставки (0 - початок): ");
            int pos = int.Parse(Console.ReadLine());

            if (pos < 0 || pos > mainStr.Length)
            {
                Console.WriteLine("Позиція поза межами рядка!");
                return;
            }

            string result = mainStr.Insert(pos, insertStr);
            Console.WriteLine("\nРезультат: " + result);
        }
        static void Task2() {
            Console.Write("Введіть рядок: ");
            string input = Console.ReadLine();

            string cleaned = new string(input
                .Where(char.IsLetterOrDigit)
                .Select(char.ToLower)
                .ToArray());

            string reversed = new string(cleaned.Reverse().ToArray());

            if (cleaned == reversed)
                Console.WriteLine("Паліндромом");
            else
                Console.WriteLine("Не паліндромом");
        }
        static void Task3() {
            Console.Write("Введіть текст: ");
            string text = Console.ReadLine();

            if (text.Length == 0)
            {
                Console.WriteLine("Текст порожній.");
                return;
            }

            int upper = text.Count(char.IsUpper);
            int lower = text.Count(char.IsLower);
            int total = text.Length;

            double upperPercent = (double)upper / total * 100;
            double lowerPercent = (double)lower / total * 100;

            Console.WriteLine($"\nВеликих літер: {upper} ({upperPercent:F2}%)");
            Console.WriteLine($"Малих літер: {lower} ({lowerPercent:F2}%)");
            Console.WriteLine($"Загальна кількість символів: {total}");
        }
        static void Task4() {

            Console.Write("Введіть слова через пробіл: ");
            string[] words = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);


            Console.Write("Введіть довжину слова для заміни: ");
            int length = int.Parse(Console.ReadLine());

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length == length)
                {
                    if (words[i].Length >= 3)
                        words[i] = words[i].Substring(0, words[i].Length - 3) + "$$$";
                    else
                        words[i] = "$$$";
                }
            }

            Console.WriteLine("\nРезультат: " + string.Join(" ", words));
        }
        static void Task5() {
            Console.Write("Введіть текст: ");
            string text = Console.ReadLine();

            string[] words = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            Console.Write("Введіть номер слова: ");
            int index = int.Parse(Console.ReadLine());

            if (index <= 0 || index > words.Length)
            {
                Console.WriteLine("Немає слова з таким номером.");
                return;
            }

            string word = words[index - 1];
            Console.WriteLine($"Слово №{index}: {word}");
            Console.WriteLine($"Перша літера: '{word[0]}'");
        }
        static void Task6() {
            Console.Write("Введіть рядок: ");
            string text = Console.ReadLine();

            string[] words = text.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
            string cleaned = string.Join("*", words);


            Console.WriteLine("\nРезультат: " + cleaned);
        }
        static void Task7() {
            StringBuilder sb = new StringBuilder();
            Console.WriteLine("Вводьте слова (завершення — слово з крапкою вкінці):");

            while (true)
            {
                string word = Console.ReadLine();

                if (word.EndsWith("."))
                {
                    sb.Append(word.TrimEnd('.'));
                    break;
                }

                sb.Append(word + ", ");
            }

            Console.WriteLine("\nСформований рядок:");
            Console.WriteLine(sb.ToString());
        }
        static void Main(string[] args)
        {
            
            while (true)
            {
                Console.WriteLine("Введіть номер завдання: ");
                int num = int.Parse(Console.ReadLine());

                switch (num)
                {
                    case 1:
                        Task1();
                        break;
                    case 2:
                        Task2();
                        break;
                    case 3:
                        Task3();
                        break;
                    case 4:
                        Task4();
                        break;
                    case 5:
                        Task5();
                        break;
                    case 6:
                        Task6();
                        break;
                    case 7:
                        Task7();
                        break;

                    default:
                        Console.WriteLine("Невірний номер завдання. Спробуйте ще раз.");
                        continue;
                }
            }
        }
    }
}
