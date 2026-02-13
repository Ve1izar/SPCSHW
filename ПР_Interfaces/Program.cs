using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ПР_Interfaces
{
    public interface IOutput
    {
        void Show();
        void Show(string info);
    }

    public interface IMath
    {
        int Max();
        int Min();
        float Avg();
        bool Search(int valueToSearch);
    }

    public interface ISort
    {
        void SortAsc();
        void SortDesc();
        void SortByParam(bool isAsc);
    }

    public class CustomArray : IOutput, IMath, ISort
    {
        private int[] _container;

        public CustomArray(int[] array)
        {
            _container = array;
        }

        public void Show()
        {
            Console.WriteLine("Елементи масиву: " + string.Join(", ", _container));
        }

        public void Show(string info)
        {
            Console.WriteLine($"Повідомлення: {info}");
            Show();
        }

        public int Max()
        {
            if (_container.Length == 0) throw new InvalidOperationException("Масив порожній");
            return _container.Max();
        }

        public int Min()
        {
            if (_container.Length == 0) throw new InvalidOperationException("Масив порожній");
            return _container.Min();
        }

        public float Avg()
        {
            if (_container.Length == 0) return 0;
            return (float)_container.Average();
        }

        public bool Search(int valueToSearch)
        {
            return _container.Contains(valueToSearch);
        }

        public void SortAsc()
        {
            Array.Sort(_container);
            Console.WriteLine("-> Масив відсортовано за зростанням.");
        }

        public void SortDesc()
        {
            Array.Sort(_container);
            Array.Reverse(_container);
            Console.WriteLine("-> Масив відсортовано за спаданням.");
        }

        public void SortByParam(bool isAsc)
        {
            if (isAsc)
            {
                SortAsc();
            }
            else
            {
                SortDesc();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int[] data = { 15, -3, 8, 42, 0, 7 };
            CustomArray myArray = new CustomArray(data);

            Console.WriteLine("Завдання 1");
            myArray.Show();
            Console.WriteLine();
            myArray.Show("Демонстрація info.");

            Console.WriteLine("\nЗавдання 2");
            Console.WriteLine($"Максимум: {myArray.Max()}");
            Console.WriteLine($"Мінімум: {myArray.Min()}");
            Console.WriteLine($"Середнє арифметичне: {myArray.Avg()}");

            int searchVal = 42;
            Console.WriteLine($"Пошук числа {searchVal}: {(myArray.Search(searchVal) ? "Знайдено" : "Не знайдено")}");

            searchVal = 100;
            Console.WriteLine($"Пошук числа {searchVal}: {(myArray.Search(searchVal) ? "Знайдено" : "Не знайдено")}");

            Console.WriteLine("\nЗавдання 3");

            myArray.SortAsc();
            myArray.Show();

            myArray.SortDesc();
            myArray.Show();

            Console.WriteLine("\nСортування через SortByParam(true):");
            myArray.SortByParam(true);
            myArray.Show();

            Console.ReadKey();
        }
    }
}
