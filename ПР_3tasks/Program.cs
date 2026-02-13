using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ПР_3tasks
{
    //Користувач вводить до рядка з клавіатури набір символів від 0-9. Необхідно перетворити рядок на число цілого типу. 
    //Передбачити випадок виходу за межі діапазону, який визначається типом int. Використовуйте механізм виключень.
    class Task_1
    {
        public void Run()
        {
            Console.WriteLine("Введіть набір символів від 0-9:");
            string input = Console.ReadLine();
            try
            {
                int number = int.Parse(input);
                Console.WriteLine($"Ви ввели число: {number}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Помилка: Введено некоректний формат числа.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Помилка: Введене число виходить за межі типу int.");
            }
        }
    }

    //Створіть клас «Кредитна картка». Вам необхідно зберігати інформацію про номер картки, ПІБ власника, CVC, дату завершення роботи картки і т.д. 
    //Передбачити механізми ініціалізації полів класу. Якщо значення для ініціалізації неправильне, генеруйте виняток.
    class Credit_card
    {
        private double cardNumber;
        private string cardHolderName;
        private int cvc;
        private DateTime expirationDate;

        public int cardnumber
        {
            get { return (int)cardNumber; }
            set
            {
                if (value.ToString().Length == 16)
                    cardNumber = value;
                else
                    throw new ArgumentException("Номер картки повинен містити 16 цифр.");
            }
        }

        public string cardholdername
        {
            get { return cardHolderName; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    cardHolderName = value;
                else
                    throw new ArgumentException("Ім'я власника картки не може бути порожнім.");
            }
        }

        public int CVC
        {
            get { return cvc; }
            set
            {
                if (value >= 100 && value <= 999)
                    cvc = value;
                else
                    throw new ArgumentException("CVC повинен містити 3 цифри.");
            }
        }
        public DateTime ExpirationDate
        {
            get { return expirationDate; }
            set
            {
                if (value > DateTime.Now)
                    expirationDate = value;
                else
                    throw new ArgumentException("Дата закінчення дії картки повинна бути в майбутньому.");
            }
        }

        public void PrintCardInfo()
        {
            Console.WriteLine($"Номер картки: {cardNumber}");
            Console.WriteLine($"Власник картки: {cardHolderName}");
            Console.WriteLine($"CVC: {cvc}");
            Console.WriteLine($"Дата закінчення дії: {expirationDate.ToShortDateString()}");
        }

    }

    //Користувач вводить до рядка з клавіатури математичний вираз. 
    //Наприклад, 3*2*1*4. Програма має підрахувати результат введеного виразу. 
    //У рядку можуть бути лише цілі числа і оператор *. 
    //Для обробки помилок введення використовуйте механізм виключень.
    class Task_3
    {
        public void Run()
        {
            try
            {
                Console.WriteLine("Введіть математичний вираз (має містити лише числа і оператор *): ");
                string input = Console.ReadLine();
                string[] parts = input.Split('*');
                int result = 1;
                foreach (string part in parts)
                {
                    result *= int.Parse(part.Trim());
                }
                Console.WriteLine($"Результат: {result}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Помилка: Вираз містить некоректні символи.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Помилка: Результат виходить за межі типу int.");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Введіть номер завдання: ");
                int taskNumber = int.Parse(Console.ReadLine());
                switch (taskNumber)
                {
                    case 1:
                        Task_1 task1 = new Task_1();
                        task1.Run();
                        break;
                    case 2:
                        try
                        {
                            Credit_card card = new Credit_card();
                            Console.WriteLine("Введіть номер картки (16 цифр): ");
                            card.cardnumber = int.Parse(Console.ReadLine());
                            Console.WriteLine("Введіть ім'я власника картки: ");
                            card.cardholdername = Console.ReadLine();
                            Console.WriteLine("Введіть CVC (3 цифри): ");
                            card.CVC = int.Parse(Console.ReadLine());
                            Console.WriteLine("Введіть дату закінчення дії картки (формат: рррр-мм-дд): ");
                            card.ExpirationDate = DateTime.Parse(Console.ReadLine());
                            card.PrintCardInfo();
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine($"Помилка: {ex.Message}");
                        }
                        break;
                    case 3:
                        Task_3 task3 = new Task_3();
                        task3.Run();
                        break;
                    default:
                        Console.WriteLine("Некоректний номер завдання. Спробуйте ще раз.");
                        break;
                }
            }
        
        }
    }
}
