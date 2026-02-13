using Games.DataAccess;
using Games.Model;
using System;
using System.Linq;

namespace Games.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            using (var db = new GamesContext())
            {
                Console.WriteLine("Підключення до бази даних...");

                if (!db.Games.Any())
                {
                    Console.WriteLine("База порожня. Додавання тестових даних...");

                    db.Games.Add(new Game
                    {
                        Name = "The Witcher 3",
                        DeveloperStudio = "CD Projekt Red",
                        Style = "RPG",
                        ReleaseDate = new DateTime(2015, 5, 19),
                        GameMode = "Singleplayer",
                        SoldCopies = 50000000
                    });

                    db.Games.Add(new Game
                    {
                        Name = "Counter-Strike 2",
                        DeveloperStudio = "Valve",
                        Style = "Shooter",
                        ReleaseDate = new DateTime(2023, 9, 27),
                        GameMode = "Multiplayer",
                        SoldCopies = 0 // Free to play
                    });

                    db.SaveChanges();
                    Console.WriteLine("Дані успішно збережено!");
                }

                // Виведення даних
                Console.WriteLine("\n--- Список ігор у MySQL ---");
                var games = db.Games.ToList();
                foreach (var game in games)
                {
                    Console.WriteLine(game);
                }
            }
            
            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}