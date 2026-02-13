using System;
using System.Linq;

namespace ПР_Worker
{
    class Worker
    {
        private string name;
        private string initials;
        private DateTime hireDate;
        private int age;
        private double salary;

        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Прізвище не може бути порожнім.");
                name = value;
            }
        }

        public string Initials
        {
            get => initials;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Ініціали не можуть бути порожніми.");
                initials = value;
            }
        }

        public DateTime HireDate
        {
            get => hireDate;
            set
            {
                if (value > DateTime.Now)
                    throw new ArgumentException("Дата прийняття не може бути в майбутньому.");
                hireDate = value;
            }
        }

        public int Age
        {
            get => age;
            set
            {
                if (value < 18 || value > 75)
                    throw new ArgumentOutOfRangeException("Вік повинен бути між 18 і 75 роками.");
                age = value;
            }
        }

        public double Salary
        {
            get => salary;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Зарплата не може бути від'ємною.");
                salary = value;
            }
        }

        public Worker(string name, string initials, int age, double salary, DateTime hireDate)
        {
            Name = name;
            Initials = initials;
            Age = age;
            Salary = salary;
            HireDate = hireDate;
        }

        public double GetWorkExperience()
        {
            TimeSpan experience = DateTime.Now - hireDate;
            return Math.Floor(experience.TotalDays / 365.25);
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"{Name} {Initials}, {Age} р., {Salary} грн, Прийнятий: {HireDate.ToShortDateString()} (Стаж: {GetWorkExperience()} років)");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Worker[] workers = new Worker[5];

            for (int i = 0; i < workers.Length; i++)
            {
                Console.WriteLine($"\nВведіть дані для працівника #{i + 1}:");

                try
                {
                    Console.Write("Прізвище: ");
                    string name = Console.ReadLine();

                    Console.Write("Ініціали: ");
                    string initials = Console.ReadLine();

                    Console.Write("Вік: ");
                    int age = int.Parse(Console.ReadLine());

                    Console.Write("Зарплата: ");
                    double salary = double.Parse(Console.ReadLine());

                    Console.Write("Дата прийняття на роботу (рррр-мм-дд): ");
                    DateTime hireDate = DateTime.Parse(Console.ReadLine());

                    workers[i] = new Worker(name, initials, age, salary, hireDate);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Помилка введення: {ex.Message}");
                    i--;
                }
            }

            var sortedWorkers = workers.OrderBy(w => w.Name).ToArray();

            Console.WriteLine("\nСписок працівників (відсортований):");
            foreach (var w in sortedWorkers)
                w.DisplayInfo();

            Console.Write("\nВведіть мінімальний стаж (у роках): ");
            double minExperience = double.Parse(Console.ReadLine());

            Console.WriteLine($"\nПрацівники зі стажем більше {minExperience} років:");
            bool found = false;
            foreach (var w in sortedWorkers)
            {
                if (w.GetWorkExperience() > minExperience)
                {
                    Console.WriteLine($"- {w.Name} ({w.GetWorkExperience()} років)");
                    found = true;
                }
            }

            if (!found)
                Console.WriteLine("Немає працівників з таким стажем.");

            Console.WriteLine("\nГотово.");
        }
    }
}
