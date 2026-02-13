using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ДЗ1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Завдання 4
            Console.WriteLine("Введіть діапазон проміжка:");
            int start = int.Parse(Console.ReadLine());
            int end = int.Parse(Console.ReadLine());

            int a = 0;
            int b = 1;

            while (a <= end)
            {
                if (a >= start)
                    Console.Write(a + " ");

                int next = a + b;
                a = b;
                b = next;
            }

            Console.WriteLine("\n\n");
            Console.ReadKey();

            // Завдання 5
            Console.WriteLine("Введіть числа A та B:");
            int A = int.Parse(Console.ReadLine());
            int B = int.Parse(Console.ReadLine());

            if (A >= B)
            {
                Console.WriteLine("Помилка: A повинно бути менше за B!");
            }
            else
            {
                for (int i = A; i <= B; i++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        Console.Write(i + " ");          
                    }
                    Console.Write("\n");
                }
            }
            Console.WriteLine("\n\n");
            Console.ReadKey();

            // Завдання 6
            Console.Write("Введіть довжину лінії: ");
            int length = int.Parse(Console.ReadLine());

            Console.Write("Введіть символ заповнювач: ");
            char symbol = Console.ReadKey().KeyChar;

            Console.WriteLine("\nОберіть напрямок лінії (h - горизонтальна, v - вертикальна): ");
            char direction = Console.ReadKey().KeyChar;

            Console.WriteLine("\n\nРезультат:");

            if (direction == 'h' || direction == 'H')
            {
                for (int i = 0; i < length; i++)
                    Console.Write(symbol);
            }
            else if (direction == 'v' || direction == 'V')
            {
                for (int i = 0; i < length; i++)
                    Console.WriteLine(symbol);
            }
            else
            {
                Console.WriteLine("Помилка: невірно вказано напрямок!");
            }

            Console.WriteLine("\n\n");
            Console.ReadKey();
        }
    }
}
