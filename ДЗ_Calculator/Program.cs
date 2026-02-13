using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ДЗ_Calculator
{
    class Calculator { 
    
        public double Add(double a, double b)
        {
            return a + b;
        }
        public double Sub(double a, double b)
        {
            return a - b;
        }
        public double Mul(double a, double b)
        {
            return a * b;
        }
        public double Div(double a, double b)
        {
            if (b == 0)
                throw new DivideByZeroException("Ділення на нуль неможливе.");
            return a / b;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();
            try
            {
                Console.WriteLine("Введіть перше число: ");
                double num1 = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Введіть перше число: ");
                double num2 = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Виберіть операцію (+,-,*,/): ");
                string operation = Console.ReadLine();
                double result = 0;
                switch (operation)
                {
                    case "+":
                        result = calculator.Add(num1, num2);
                        break;
                    case "-":
                        result = calculator.Sub(num1, num2);
                        break;
                    case "*":
                        result = calculator.Mul(num1, num2);
                        break;
                    case "/":
                        result = calculator.Div(num1, num2);
                        break;
                    default:
                        Console.WriteLine("Помилка: Невідома операція.");
                        return;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Помилка: Некоректний формат числа.");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }

        }
    }
}
