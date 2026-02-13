using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ДЗ_Delegates
{
    class Program
    {
        public delegate bool NumberPredicate(int number);

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("\n1:");
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 13, 21, 34, 40, 55, 89, 100 };
            Console.WriteLine("Вихідний масив: " + string.Join(", ", numbers));

            Console.WriteLine("Парні числа: " + string.Join(", ", FilterArray(numbers, IsEven)));
            Console.WriteLine("Непарні числа: " + string.Join(", ", FilterArray(numbers, IsOdd)));
            Console.WriteLine("Прості числа: " + string.Join(", ", FilterArray(numbers, IsPrime)));
            Console.WriteLine("Числа Фібоначчі: " + string.Join(", ", FilterArray(numbers, IsFibonacci)));

            Console.WriteLine("\n2:");

            Action showTime = () => Console.WriteLine($"Поточний час: {DateTime.Now.ToShortTimeString()}");
            Action showDate = () => Console.WriteLine($"Поточна дата: {DateTime.Now.ToShortDateString()}");
            Action showDay = () => Console.WriteLine($"День тижня: {DateTime.Now.DayOfWeek}");

            showTime();
            showDate();
            showDay();

            Func<double, double, double> triangleArea = (b, h) => 0.5 * b * h;
            Func<double, double, double> rectangleArea = (w, h) => w * h;

            Predicate<double> isValidDimension = (d) => d > 0;

            double b1 = 10, h1 = 5;
            if (isValidDimension(b1) && isValidDimension(h1))
            {
                Console.WriteLine($"Площа трикутника ({b1}x{h1}): {triangleArea(b1, h1)}");
                Console.WriteLine($"Площа прямокутника ({b1}x{h1}): {rectangleArea(b1, h1)}");
            }

            Console.WriteLine("\n3:");

            Func<string, string> getRainbowRGB = delegate (string color)
            {
                switch (color.ToLower())
                {
                    case "червоний": return "255, 0, 0";
                    case "помаранчевий": return "255, 165, 0";
                    case "жовтий": return "255, 255, 0";
                    case "зелений": return "0, 128, 0";
                    case "блакитний": return "0, 191, 255";
                    case "синій": return "0, 0, 255";
                    case "фіолетовий": return "238, 130, 238";
                    default: return "Колір не знайдено (не веселковий)";
                }
            };

            string testColor = "Зелений";
            Console.WriteLine($"RGB для кольору '{testColor}': {getRainbowRGB(testColor)}");
            testColor = "Червоний";
            Console.WriteLine($"RGB для кольору '{testColor}': {getRainbowRGB(testColor)}");


            Console.WriteLine("\n4:");

            Func<int[], int, int, int> countInRange = (arr, min, max) =>
            {
                int count = 0;
                foreach (var n in arr)
                {
                    if (n >= min && n <= max) count++;
                }
                return count;
            };

            int minVal = 10, maxVal = 50;
            int resultCount = countInRange(numbers, minVal, maxVal);
            Console.WriteLine($"Кількість чисел у діапазоні [{minVal}-{maxVal}]: {resultCount}");


            Console.WriteLine("\n5:");

            string sampleText = "Сонце світить, сонце гріє. Сонце - це життя.";
            string searchWord = "Сонце";

            Func<string, string, (bool Found, int Count)> searchStats = (text, word) =>
            {
                if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(word))
                    return (false, 0);

                string[] words = text.Split(new char[] { ' ', '.', ',', '-', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

                int count = 0;
                foreach (var w in words)
                {
                    if (w.Equals(word, StringComparison.OrdinalIgnoreCase))
                        count++;
                }

                return (count > 0, count);
            };

            var stats = searchStats(sampleText, searchWord);
            Console.WriteLine($"Текст: \"{sampleText}\"");
            Console.WriteLine($"Шукаємо слово: \"{searchWord}\"");
            Console.WriteLine($"Знайдено: {stats.Found}, Кількість входжень: {stats.Count}");

            Console.ReadKey();
        }

        static List<int> FilterArray(int[] array, NumberPredicate predicate)
        {
            List<int> result = new List<int>();
            foreach (int item in array)
            {
                if (predicate(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        static bool IsEven(int n) => n % 2 == 0;

        static bool IsOdd(int n) => n % 2 != 0;

        static bool IsPrime(int n)
        {
            if (n < 2) return false;
            for (int i = 2; i <= Math.Sqrt(n); i++)
            {
                if (n % i == 0) return false;
            }
            return true;
        }

        static bool IsFibonacci(int n)
        {
            bool IsPerfectSquare(long x)
            {
                long s = (long)Math.Sqrt(x);
                return (s * s == x);
            }

            if (n < 0) return false;
            long nLong = n;
            return IsPerfectSquare(5 * nLong * nLong + 4) || IsPerfectSquare(5 * nLong * nLong - 4);
        }
    }
}
