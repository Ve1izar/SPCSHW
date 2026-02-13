using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameStore
{

    public class Developer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public List<Game> Games { get; set; } = new();
    }

    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int ReleaseYear { get; set; }
        
        public int DeveloperId { get; set; }
        public Developer Developer { get; set; }
        
        public List<OrderItem> OrderItems { get; set; } = new();
    }

    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<Order> Orders { get; set; } = new();
    }

    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        
        public List<OrderItem> OrderItems { get; set; } = new();
    }

    public class OrderItem
    {
        public int Id { get; set; }
        
        public int OrderId { get; set; }
        public Order Order { get; set; }
        
        public int GameId { get; set; }
        public Game Game { get; set; }
        
        public int Quantity { get; set; }
    }


    public class GameStoreContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=localhost;Database=GameStoreDb;User=root;Password=Jz&c7!kKA%t=wEh&7R;";

            optionsBuilder.UseMySql(
                connectionString, 
                ServerVersion.AutoDetect(connectionString)
            );
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            try 
            {
                using (var db = new GameStoreContext())
                {
                    Console.WriteLine("Підключення до MySQL");

                    db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();
                    
                    Console.WriteLine("База даних 'GameStoreDb' створена.");
                    
                    SeedData(db);
                    Console.WriteLine("Дані успішно додані!\n");


                    Console.WriteLine("1. Всі ігри з розробниками: ");
                    var gamesWithDevs = db.Games.Include(g => g.Developer).ToList();
                    foreach (var g in gamesWithDevs)
                    {
                        Console.WriteLine($"- {g.Title} ({g.ReleaseYear}) - Dev: {g.Developer.Name}");
                    }

                    Console.WriteLine("\n 2. Замовлення з клієнтами та іграми: ");
                    var ordersFull = db.Orders
                        .Include(o => o.Customer)
                        .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Game)
                        .ToList();

                    foreach (var o in ordersFull)
                    {
                        Console.WriteLine($"Order #{o.Id} by {o.Customer.FullName} ({o.OrderDate:d}):");
                        foreach (var item in o.OrderItems)
                        {
                            Console.WriteLine($"   * {item.Game.Title} x{item.Quantity}");
                        }
                    }

                    Console.WriteLine("\n 3. Сума кожного замовлення: ");
                    var orderTotals = db.Orders
                        .Select(o => new
                        {
                            OrderId = o.Id,
                            Customer = o.Customer.FullName,
                            Total = o.OrderItems.Sum(oi => oi.Quantity * oi.Game.Price)
                        })
                        .ToList();

                    foreach (var t in orderTotals)
                    {
                        Console.WriteLine($"Order #{t.OrderId} ({t.Customer}): ${t.Total}");
                    }

                    // 4. Топ 3 найдорожчі ігри
                    Console.WriteLine("\n 4. 3 найдорожчі ігри: ");
                    var topExpensive = db.Games
                        .OrderByDescending(g => g.Price)
                        .Take(3)
                        .ToList();

                    foreach (var g in topExpensive)
                    {
                        Console.WriteLine($"${g.Price} - {g.Title}");
                    }

                    Console.WriteLine("\n 5. Клієнти з > 1 замовленням: ");
                    var activeCustomers = db.Customers
                        .Where(c => c.Orders.Count > 1)
                        .Select(c => new { c.FullName, OrderCount = c.Orders.Count })
                        .ToList();

                    foreach (var c in activeCustomers)
                    {
                        Console.WriteLine($"{c.FullName}: {c.OrderCount} замовлень");
                    }

                    Console.WriteLine("\n 6. Загальний дохід магазину: ");
                    var totalRevenue = db.OrderItems
                        .Sum(oi => oi.Quantity * oi.Game.Price);

                    Console.WriteLine($"Загальний дохід: ${totalRevenue}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ПОМИЛКА: {ex.Message}");
                Console.WriteLine("Перевірте пароль до MySQL та чи запущений сервер.");
            }
            
            Console.ReadKey();
        }

        static void SeedData(GameStoreContext db)
        {
            var devs = new List<Developer>
            {
                new Developer { Name = "Valve", Country = "USA" },
                new Developer { Name = "CD Projekt Red", Country = "Poland" },
                new Developer { Name = "Ubisoft", Country = "France" },
                new Developer { Name = "Capcom", Country = "Japan" },
                new Developer { Name = "Rockstar Games", Country = "USA" }
            };
            db.Developers.AddRange(devs);
            db.SaveChanges();

            var games = new List<Game>
            {
                new Game { Title = "Half-Life 3", Price = 59.99m, ReleaseYear = 2024, Developer = devs[0] },
                new Game { Title = "Portal 2", Price = 19.99m, ReleaseYear = 2011, Developer = devs[0] },
                new Game { Title = "Cyberpunk 2077", Price = 49.99m, ReleaseYear = 2020, Developer = devs[1] },
                new Game { Title = "The Witcher 3", Price = 39.99m, ReleaseYear = 2015, Developer = devs[1] },
                new Game { Title = "Assassin's Creed Mirage", Price = 49.99m, ReleaseYear = 2023, Developer = devs[2] },
                new Game { Title = "Far Cry 6", Price = 59.99m, ReleaseYear = 2021, Developer = devs[2] },
                new Game { Title = "Resident Evil 4", Price = 59.99m, ReleaseYear = 2023, Developer = devs[3] },
                new Game { Title = "Street Fighter 6", Price = 59.99m, ReleaseYear = 2023, Developer = devs[3] },
                new Game { Title = "GTA V", Price = 29.99m, ReleaseYear = 2013, Developer = devs[4] },
                new Game { Title = "Red Dead Redemption 2", Price = 59.99m, ReleaseYear = 2018, Developer = devs[4] }
            };
            db.Games.AddRange(games);
            db.SaveChanges();

            var customers = new List<Customer>();
            for (int i = 1; i <= 8; i++)
            {
                customers.Add(new Customer { FullName = $"Customer {i}", Email = $"user{i}@mail.com" });
            }
            db.Customers.AddRange(customers);
            db.SaveChanges();

            var orders = new List<Order>();
            var rnd = new Random();
            
            orders.Add(new Order { Customer = customers[0], OrderDate = DateTime.Now.AddDays(-10) });
            orders.Add(new Order { Customer = customers[0], OrderDate = DateTime.Now.AddDays(-5) });
            orders.Add(new Order { Customer = customers[1], OrderDate = DateTime.Now.AddDays(-20) });
            orders.Add(new Order { Customer = customers[1], OrderDate = DateTime.Now.AddDays(-2) });
            
            for (int i = 0; i < 6; i++)
            {
                orders.Add(new Order { Customer = customers[rnd.Next(2, 8)], OrderDate = DateTime.Now.AddDays(-rnd.Next(1, 30)) });
            }
            db.Orders.AddRange(orders);
            db.SaveChanges();

            var orderItems = new List<OrderItem>();
            for (int i = 0; i < 20; i++)
            {
                orderItems.Add(new OrderItem
                {
                    Order = orders[rnd.Next(orders.Count)],
                    Game = games[rnd.Next(games.Count)],
                    Quantity = rnd.Next(1, 4)
                });
            }
            db.OrderItems.AddRange(orderItems);
            db.SaveChanges();
        }
    }
}