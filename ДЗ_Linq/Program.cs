using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqTasks
{
    public class Employee
    {
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }

        public override string ToString()
        {
            return $"{FullName} | {Position} | {Phone} | {Email} | ${Salary}";
        }
    }

    public class Firm
    {
        public string Name { get; set; }
        public DateTime FoundationDate { get; set; }
        public string BusinessProfile { get; set; }
        public string DirectorName { get; set; }
        public int EmployeeCount { get; set; }
        public string Address { get; set; }

        public List<Employee> Employees { get; set; } = new List<Employee>();

        public override string ToString()
        {
            return $"[{Name}] Профіль: {BusinessProfile}, Директор: {DirectorName}, " +
                   $"Працівників: {EmployeeCount}, Адреса: {Address}, Дата: {FoundationDate:d}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var firms = new List<Firm>
            {
                new Firm
                {
                    Name = "FoodMaster",
                    FoundationDate = new DateTime(2010, 5, 20),
                    BusinessProfile = "Food",
                    DirectorName = "John White",
                    EmployeeCount = 150,
                    Address = "London, Baker Street",
                    Employees = new List<Employee>
                    {
                        new Employee { FullName = "Lionel Messi", Position = "Manager", Phone = "230001", Email = "lio@food.com", Salary = 5000 },
                        new Employee { FullName = "Mark Smith", Position = "Cook", Phone = "555002", Email = "mark@food.com", Salary = 3000 }
                    }
                },
                new Firm
                {
                    Name = "TechSoft IT",
                    FoundationDate = DateTime.Today.AddDays(-123), 
                    BusinessProfile = "IT",
                    DirectorName = "Bill Gates",
                    EmployeeCount = 50,
                    Address = "New York",
                    Employees = new List<Employee>
                    {
                        new Employee { FullName = "Steve Jobs", Position = "Developer", Phone = "234567", Email = "steve@tech.com", Salary = 8000 },
                        new Employee { FullName = "Diana Prince", Position = "Manager", Phone = "987654", Email = "diana@tech.com", Salary = 6000 }
                    }
                },
                new Firm
                {
                    Name = "White Marketing",
                    FoundationDate = new DateTime(2023, 1, 1),
                    BusinessProfile = "Marketing",
                    DirectorName = "Jack Black",
                    EmployeeCount = 200,
                    Address = "Paris",
                    Employees = new List<Employee>
                    {
                        new Employee { FullName = "Tom Hardy", Position = "Marketer", Phone = "123123", Email = "di_tom@white.com", Salary = 4500 }
                    }
                },
                new Firm
                {
                    Name = "London Foods",
                    FoundationDate = new DateTime(2005, 8, 15),
                    BusinessProfile = "Marketing",
                    DirectorName = "Walter White",
                    EmployeeCount = 10,
                    Address = "London, Oxford Street",
                    Employees = new List<Employee>()
                },
                new Firm
                {
                    Name = "Mega IT Corp",
                    FoundationDate = new DateTime(2015, 3, 10),
                    BusinessProfile = "IT",
                    DirectorName = "Elon Musk",
                    EmployeeCount = 500,
                    Address = "Texas",
                    Employees = new List<Employee>
                    {
                        new Employee { FullName = "Lionel Richie", Position = "Manager", Phone = "239999", Email = "lio.r@mega.com", Salary = 12000 }
                    }
                }
            };

            // Запити
            
            Console.WriteLine("\n Завдання 1");

            Console.WriteLine("\n1. Всі фірми:");
            var q1 = from f in firms select f;
            Print(q1);

            Console.WriteLine("\n2. Фірми зі словом 'Food' у назві:");
            var q2 = from f in firms where f.Name.Contains("Food") select f;
            Print(q2);

            Console.WriteLine("\n3. Фірми у галузі Маркетингу:");
            var q3 = from f in firms where f.BusinessProfile == "Marketing" select f;
            Print(q3);

            Console.WriteLine("\n4. Фірми у галузі Маркетингу або IT:");
            var q4 = from f in firms
                     where f.BusinessProfile == "Marketing" || f.BusinessProfile == "IT"
                     select f;
            Print(q4);

            Console.WriteLine("\n5. Кількість працівників > 100:");
            var q5 = from f in firms where f.EmployeeCount > 100 select f;
            Print(q5);

            Console.WriteLine("\n6. Кількість працівників від 100 до 300:");
            var q6 = from f in firms
                     where f.EmployeeCount >= 100 && f.EmployeeCount <= 300
                     select f;
            Print(q6);

            Console.WriteLine("\n7. Фірми в Лондоні:");
            var q7 = from f in firms where f.Address.Contains("London") select f;
            Print(q7);

            Console.WriteLine("\n8. Прізвище директора White:");
            var q8 = from f in firms where f.DirectorName.Contains("White") select f;
            Print(q8);

            Console.WriteLine("\n9. Засновані більше 2 років тому:");
            var twoYearsAgo = DateTime.Now.AddYears(-2);
            var q9 = from f in firms where f.FoundationDate < twoYearsAgo select f;
            Print(q9);

            Console.WriteLine("\n10. З дня заснування минуло 123 дні:");
            var q10 = from f in firms
                      where (DateTime.Today - f.FoundationDate.Date).TotalDays == 123
                      select f;
            Print(q10);

            Console.WriteLine("\n11. Директор Black і назва фірми 'White':");
            var q11 = from f in firms
                      where f.DirectorName.Contains("Black") && f.Name.Contains("White")
                      select f;
            Print(q11);


            // Розширення
            
            Console.WriteLine("\n\nЗавдання 2");

            Console.WriteLine("\n1. Всі фірми:");
            var m1 = firms;
            Print(m1);

            Console.WriteLine("\n2. Фірми зі словом 'Food':");
            var m2 = firms.Where(f => f.Name.Contains("Food"));
            Print(m2);

            Console.WriteLine("\n3. Маркетинг:");
            var m3 = firms.Where(f => f.BusinessProfile == "Marketing");
            Print(m3);

            Console.WriteLine("\n4. Маркетинг або IT:");
            var m4 = firms.Where(f => f.BusinessProfile == "Marketing" || f.BusinessProfile == "IT");
            Print(m4);

            Console.WriteLine("\n5. Працівників > 100:");
            var m5 = firms.Where(f => f.EmployeeCount > 100);
            Print(m5);

            Console.WriteLine("\n6. Працівників 100-300:");
            var m6 = firms.Where(f => f.EmployeeCount >= 100 && f.EmployeeCount <= 300);
            Print(m6);

            Console.WriteLine("\n7. Лондон:");
            var m7 = firms.Where(f => f.Address.Contains("London"));
            Print(m7);

            Console.WriteLine("\n8. Директор White:");
            var m8 = firms.Where(f => f.DirectorName.Contains("White"));
            Print(m8);

            Console.WriteLine("\n9. > 2 років тому:");
            var m9 = firms.Where(f => f.FoundationDate < DateTime.Now.AddYears(-2));
            Print(m9);

            Console.WriteLine("\n10. Минуло 123 дні:");
            var m10 = firms.Where(f => (DateTime.Today - f.FoundationDate.Date).TotalDays == 123);
            Print(m10);

            Console.WriteLine("\n11. Директор Black і назва 'White':");
            var m11 = firms.Where(f => f.DirectorName.Contains("Black") && f.Name.Contains("White"));
            Print(m11);


            
            // Працівники
        
            Console.WriteLine("\n\nЗавдання 3");

            string targetFirmName = "TechSoft IT";
            var targetFirm = firms.FirstOrDefault(f => f.Name == targetFirmName);

            Console.WriteLine($"\n1. Працівники фірми '{targetFirmName}':");
            if (targetFirm != null)
            {
                PrintEmployees(targetFirm.Employees);
            }

            Console.WriteLine($"\n2. Працівники фірми '{targetFirmName}' із зарплатою > 7000:");
            if (targetFirm != null)
            {
                var richEmployees = targetFirm.Employees.Where(e => e.Salary > 7000);
                PrintEmployees(richEmployees);
            }

            Console.WriteLine("\n3. Працівники всіх фірм з посадою 'Manager':");
            var managers = firms.SelectMany(f => f.Employees).Where(e => e.Position == "Manager");
            PrintEmployees(managers);

            Console.WriteLine("\n4. Телефон починається з '23':");
            var phone23 = firms.SelectMany(f => f.Employees).Where(e => e.Phone.StartsWith("23"));
            PrintEmployees(phone23);

            Console.WriteLine("\n5. Email починається з 'di':");
            var emailDi = firms.SelectMany(f => f.Employees).Where(e => e.Email.StartsWith("di"));
            PrintEmployees(emailDi);

            Console.WriteLine("\n6. Ім'я Lionel:");
            var lionels = firms.SelectMany(f => f.Employees).Where(e => e.FullName.Contains("Lionel"));
            PrintEmployees(lionels);

            Console.ReadLine();
        }

        static void Print(IEnumerable<Firm> firms)
        {
            if (!firms.Any())
            {
                Console.WriteLine("  (Дані відсутні)");
                return;
            }
            foreach (var item in firms)
            {
                Console.WriteLine("  " + item);
            }
        }

        static void PrintEmployees(IEnumerable<Employee> employees)
        {
            if (!employees.Any())
            {
                Console.WriteLine("  (Дані відсутні)");
                return;
            }
            foreach (var item in employees)
            {
                Console.WriteLine("  " + item);
            }
        }
    }
}