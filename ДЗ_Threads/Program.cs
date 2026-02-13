using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ДЗ_Threads
{
    class ProgramRanges
    {
        static List<int> numbers = new List<int>();
        static int maxVal;
        static int minVal;
        static double avgVal;

        static ManualResetEvent calculationsDone = new ManualResetEvent(false);
        static int threadsCompleted = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("1-3");

            // 2
            Console.Write("Початок діапазону: ");
            int start = int.Parse(Console.ReadLine());

            Console.Write("Кінець діапазону: ");
            int end = int.Parse(Console.ReadLine());

            // 3
            Console.Write("Кількість потоків: ");
            int threadCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < threadCount; i++)
            {
                int threadIndex = i;
                Thread t = new Thread(() => PrintNumbers(start, end, threadIndex));
                t.Start();
            }

            Console.ReadLine();

            Console.WriteLine("--- Завдання 4-5: Аналіз даних ---");

            // Генерація 10000 чисел
            Random rand = new Random();
            for (int i = 0; i < 10000; i++)
            {
                numbers.Add(rand.Next(1, 100000));
            }
            Console.WriteLine("Дані згенеровано.");

            // Завдання 4: Створення потоків для обчислень
            Thread tMax = new Thread(FindMax);
            Thread tMin = new Thread(FindMin);
            Thread tAvg = new Thread(FindAvg);

            // Завдання 5: Потік для запису у файл
            Thread tFile = new Thread(WriteToFile);

            // Запуск потоків
            tMax.Start();
            tMin.Start();
            tAvg.Start();
            tFile.Start();

            Console.ReadLine();
        }
        static void PrintNumbers(int start, int end, int threadId)
        {
            for (int i = start; i <= end; i++)
            {
                Console.WriteLine($"{threadId} потік: {i}");
                Thread.Sleep(50);
            }
        }
        static void FindMax()
        {
            maxVal = int.MinValue;
            foreach (var n in numbers)
            {
                if (n > maxVal) maxVal = n;
            }
            Console.WriteLine($"Max завершено: {maxVal}");
            CheckCompletion();
        }

        static void FindMin()
        {
            minVal = int.MaxValue;
            foreach (var n in numbers)
            {
                if (n < minVal) minVal = n;
            }
            Console.WriteLine($"Min завершено: {minVal}");
            CheckCompletion();
        }

        static void FindAvg()
        {
            long sum = 0;
            foreach (var n in numbers)
            {
                sum += n;
            }
            avgVal = (double)sum / numbers.Count;
            Console.WriteLine($"Avg завершено: {avgVal}");
            CheckCompletion();
        }

        static void CheckCompletion()
        {
            if (Interlocked.Increment(ref threadsCompleted) == 3)
            {
                calculationsDone.Set();
            }
        }

        static void WriteToFile()
        {
            string path = "results.txt";
            Console.WriteLine("Початок запису");

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine("Набір чисел");
                foreach (var n in numbers)
                {
                    sw.Write(n + " ");
                }
                sw.WriteLine("\n\nРезультати");

                Console.WriteLine("Очікування");
                calculationsDone.WaitOne();

                sw.WriteLine($"Максимум: {maxVal}");
                sw.WriteLine($"Мінімум: {minVal}");
                sw.WriteLine($"Середнє: {avgVal}");
            }
            Console.WriteLine("Дані збережено у results.txt");
        }
    }
}
