using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ПР1
{
    internal class Program
    {   
        static void Task1() {
            Random random = new Random();

            int[] array = new int[10];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(1, 100);
            }

            int even = 0;
            int odd = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] % 2 == 0)
                {
                    even++;
                }
                else
                {
                    odd++;
                }
            }

            bool isUnique = true;
            int[] uniqueelems = new int[array.Length];

            Console.WriteLine("Масив: " + string.Join(", ", array));
            Console.WriteLine("Кількість парних чисел: " + even);
            Console.WriteLine("Кількість непарних чисел: " + odd);
            if (uniqueelems.Length == array.Length)
            {
                Console.WriteLine("Всі елементи масиву унікальні.");
            }
            else
            {
                Console.WriteLine("Неунікальні елементи масиву: " + string.Join(", ", uniqueelems.Where(x => x != 0)));
            }

            Console.WriteLine("\n");
            Console.ReadKey();
        }

        static void Task2()
        {
            Random random = new Random();

            int[] array1 = new int[10];
            for (int i = 0; i < array1.Length; i++)
            {
                array1[i] = random.Next(20);
            }

            Console.WriteLine("Введіть параметр:");
            int a = int.Parse(Console.ReadLine());
            int counter = 0;

            foreach (int num in array1)
            {
                if (num < a)
                {
                    counter++;
                }
            }

            Console.WriteLine("Масив: " + string.Join(", ", array1));
            Console.WriteLine("Кількість елементів менших за введене число: " + counter);

            Console.WriteLine("\n");
            Console.ReadKey();
        }

        static void Task3()
        {
            Random random = new Random();

            int[] A = new int[5];
            int[,] B = new int[3, 4];

            for (int i = 0; i < A.Length; i++)
            {
                Console.WriteLine($"Введіть елемент [ {i + 1} ]: ");

                int y = int.Parse(Console.ReadLine());
                A[i] = y;
            }
            Console.WriteLine("Масив A: " + string.Join(", ", A) + "\n");

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    B[i, j] = random.Next(20); ;
                }
            }

            Console.WriteLine("Масив B:");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write(B[i, j] + "\t");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\n");

            int maxA = A.Max();
            int maxB = B.Cast<int>().Max();
            int minA = A.Min();
            int minB = B.Cast<int>().Min();

            int globalMax = Math.Max(maxA, maxB);
            int globalMin = Math.Min(minA, minB);

            int sumA = A.Sum();
            int sumB = B.Cast<int>().Sum();
            int totalSum = sumA + sumB;

            long productA = 1;
            foreach (int num in A)
                productA *= num;

            long productB = 1;
            foreach (int num in B)
                productB *= num;

            long totalProduct = productA * productB;

            int evenSumA = A.Where(x => x % 2 == 0).Sum();

            int oddColumnsSumB = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (j % 2 != 0)
                        oddColumnsSumB += B[i, j];
                }
            }

            Console.WriteLine($"Загальний максимум: {globalMax}");
            Console.WriteLine($"Загальний мінімум: {globalMin}");
            Console.WriteLine($"Загальна сума елементів: {totalSum}");
            Console.WriteLine($"Загальний добуток елементів: {totalProduct}");
            Console.WriteLine($"Сума парних елементів масиву A: {evenSumA}");
            Console.WriteLine($"Сума елементів непарних стовпців масиву B: {oddColumnsSumB}");
        }

        static void Task4() {

            Random random = new Random();
            int size_i = 5;
            int size_j = 5;
            int[,] A = new int[size_i,size_j];
            for (int i = 0; i < size_i; i++)
            {
                for (int j = 0; j < size_j; j++)
                {
                    A[i, j] = random.Next(-100, 100);
                }
            }

            int maxB = A.Cast<int>().Max();
            int minB = A.Cast<int>().Min();

            int totalSum = 0;
            for (int i = 0; i < size_i; i++)
            {
                int rowSum = 0;
                bool between = false;
                for (int j = 0; j < size_j; j++)
                {
                    if (A[i, j] == minB)
                    {
                        between = true;
                        continue;
                    }
                    if (A[i, j] == maxB)
                    {
                        between = false;
                        continue;
                    }
                    if (between)
                    {
                        rowSum += A[i, j];
                    }
                }
                totalSum += rowSum;
            }
            Console.WriteLine("Масив A:");
            for (int i = 0; i < size_i; i++)
            {
                for (int j = 0; j < size_j; j++)
                {
                    Console.Write(A[i, j] + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine($"Сума елементів між мінімальним та максимальним у кожному рядку: {totalSum}");
        }

        static void Task5()
        {
            Random random = new Random();

            int[] A = new int[20];
            for (int i = 0; i < A.Length; i++)
            {
                A[i] = random.Next(20);
            }

            int minA = A.Min();

            int counter = 0;

            foreach (int num in A)
            {
                if (num == minA-5 || num == minA - 5) 
                {
                    counter++;
                }
            }

            Console.WriteLine("Масив: ");
            for (int i = 0; i < 20; i++)
            {
                Console.Write(A[i] + "\t");
            }
            Console.WriteLine($"\nКількість елементів, що відрізняються nвід мінімального на 5: {counter}");
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
                    default:
                        Console.WriteLine("Невірний номер завдання. Спробуйте ще раз.");
                        continue;
                }
            }
        }
    }
}
