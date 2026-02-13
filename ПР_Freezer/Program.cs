using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ПР_Freezer
{

    partial class Freezer
    {
        public void TurnOn()
        {
            isOn = true;
            Console.WriteLine($"{brand} {model} увімкнено.");
        }

        public void TurnOff()
        {
            isOn = false;
            Console.WriteLine($"{brand} {model} вимкнено.");
        }

        public void SetTemperatureRange(int newMin, int newMax)
        {
            if (newMin >= -30 && newMax <= 0 && newMin < newMax)
            {
                minTemp = newMin;
                maxTemp = newMax;
                Console.WriteLine($"Температурний діапазон оновлено: {minTemp}°C - {maxTemp}°C");
            }
            else
            {
                Console.WriteLine("Некоректний температурний діапазон.");
            }
        }
        public void ChangeCapacity(ref double newCapacity)
        {
            if (newCapacity > 0)
            {
                capacity = newCapacity;
                Console.WriteLine($"Місткість змінено на {capacity} літрів.");
            }
            else
            {
                Console.WriteLine("Некоректна місткість.");
                newCapacity = capacity;
            }
        }
    }
    partial class Freezer
    {
        private string brand;
        private string model;
        private double capacity;
        private int minTemp;
        private int maxTemp;
        private int height;
        private int width;
        private bool isOn;

        public static string type;
        public static int totalFreezers;

        static Freezer()
        {
            type = "Морозильна камера";
            totalFreezers = 0;
        }
        public Freezer(string brand, string model, double capacity)
        {
            this.brand = brand;
            this.model = model;
            this.capacity = capacity;
            this.minTemp = -20;
            this.maxTemp = -5;
            this.height = 200;
            this.width = 85;
            this.isOn = false;
            totalFreezers++;
        }
        public Freezer(string brand, string model, double capacity, int minTemp, int maxTemp, int height, int width, bool isOn)
        {
            this.brand = brand;
            this.model = model;
            this.capacity = capacity;
            this.minTemp = minTemp;
            this.maxTemp = maxTemp;
            this.height = height;
            this.width = width;
            this.isOn = isOn;
            totalFreezers++;
        }

        public int minTemperature
        {
            get { return minTemp; }
            set
            {
                if (value >= -30 && value <= 0)
                    minTemp = value;
                else
                    minTemp = -20;
            }
        }
        public int maxTemperature
        {
            get { return maxTemp; }
            set
            {
                if (value >= -30 && value <= 0)
                    maxTemp = value;
                else
                    maxTemp = -20;
            }
        }
        public int Height
        {
            get { return height; }
            set
            {
                if (value > 0)
                    height = value;
                else
                    height = 200;
            }
        }
        public int Width
        {
            get { return width; }
            set
            {
                if (value > 0)
                    width = value;
                else
                    width = 200;
            }
        }
        public double Capacity
        {
            get { return capacity; }
            set
            {
                if (value > 0)
                    capacity = value;
                else
                    capacity = 100;
            }
        }

        public override string ToString()
        {
            return $"Тип: {type}\n" +
                   $"Бренд: {brand}\n" +
                   $"Модель: {model}\n" +
                   $"Місткість: {capacity} л\n" +
                   $"Температура: {minTemp}°C – {maxTemp}°C\n" +
                   $"Розміри: {height}x{width} см\n" +
                   $"Стан: {(isOn ? "Увімкнено" : "Вимкнено")}\n";
        }

        public void Print()
        {
            Console.WriteLine("\nІнформація про морозильник");
            Console.WriteLine($"Тип: {type}");
            Console.WriteLine($"Бренд: {brand}");
            Console.WriteLine($"Модель: {model}");
            Console.WriteLine($"Об'єм: {capacity} л");
            Console.WriteLine($"Діапазон температур: {minTemp}°C – {maxTemp}°C");
            Console.WriteLine($"Розміри (ВхШ): {height}x{width} см");
            Console.WriteLine($"Стан: {(isOn ? "Увімкнено" : "Вимкнено")}");
            Console.WriteLine($"Загальна кількість морозильників: {totalFreezers}");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {

            Freezer[] freezers =
            {
                new Freezer("Samsung", "FrostX200", 220),
                new Freezer("LG", "CoolBox300", 180),
                new Freezer("Bosch", "IcePro100", 150, -25, -10, 190, 80, true),
                new Freezer("Whirlpool", "FZ-90", 200),
                new Freezer("Indesit", "FreezMaster", 250, -28, -12, 210, 90, false)
            };

            Console.WriteLine("\nМорозильники: ");
            foreach (var f in freezers)
                Console.WriteLine(f.ToString());

            Freezer f1 = new Freezer("Samsung", "FrostX200", 220);

            f1.TurnOn();
            f1.SetTemperatureRange(-25, -10);
            f1.Print();

            double newCap = 250;
            f1.ChangeCapacity(ref newCap);
            f1.Print();

            f1.TurnOff();

            
        }
    }
}
