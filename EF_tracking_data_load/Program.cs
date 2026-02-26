using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameDbApp
{

    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int FoundationYear { get; set; }
        public string Website { get; set; }

        public List<Game> Games { get; set; } = new();
    }

    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Game> Games { get; set; } = new();
    }

    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public double Rating { get; set; }

        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }

        public List<Genre> Genres { get; set; } = new();
    }


    public class AppDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            var connectionString = "Server=localhost;Database=GamesAdvancedDb;User=root;Password=Jz&c7!kKA%t=wEh&7R;";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }


    public class DbManager
    {
        private readonly AppDbContext _db;

        public DbManager(AppDbContext db)
        {
            _db = db;
        }

        public void CreatePublisher(Publisher p) { _db.Publishers.Add(p); _db.SaveChanges(); }
        public Publisher ReadPublisher(int id) => _db.Publishers.Find(id);
        public void UpdatePublisher(Publisher p) { _db.Publishers.Update(p); _db.SaveChanges(); }
        public void DeletePublisher(int id)
        {
            var p = _db.Publishers.Find(id);
            if (p != null) { _db.Publishers.Remove(p); _db.SaveChanges(); }
        }

        public void CreateGenre(Genre g) { _db.Genres.Add(g); _db.SaveChanges(); }
        public Genre ReadGenre(int id) => _db.Genres.Find(id);
        public void UpdateGenre(Genre g) { _db.Genres.Update(g); _db.SaveChanges(); }
        public void DeleteGenre(int id)
        {
            var g = _db.Genres.Find(id);
            if (g != null) { _db.Genres.Remove(g); _db.SaveChanges(); }
        }

        public void CreateGame(Game g) { _db.Games.Add(g); _db.SaveChanges(); }
        public Game ReadGame(int id) => _db.Games.Find(id);
        public void UpdateGame(Game g) { _db.Games.Update(g); _db.SaveChanges(); }
        public void DeleteGame(int id)
        {
            var g = _db.Games.Find(id);
            if (g != null) { _db.Games.Remove(g); _db.SaveChanges(); }
        }


        // Eager Loading для Games <-> Genres
        public void PrintGamesByGenre(string genreName)
        {
            Console.WriteLine($"\n--- Ігри жанру: {genreName} (Eager Loading) ---");

            var genre = _db.Genres
                .Include(g => g.Games)
                .FirstOrDefault(g => g.Name == genreName);

            if (genre != null && genre.Games.Any())
            {
                foreach (var game in genre.Games)
                {
                    Console.WriteLine($"- {game.Title} (Рейтинг: {game.Rating})");
                }
            }
            else
            {
                Console.WriteLine("Ігор не знайдено.");
            }
        }

        // Eager Loading для Games <-> Genres
        public void PrintGenresOfGame(string gameTitle)
        {
            Console.WriteLine($"\n--- Жанри гри: {gameTitle} (Eager Loading) ---");

            var game = _db.Games
                .Include(g => g.Genres)
                .FirstOrDefault(g => g.Title == gameTitle);

            if (game != null && game.Genres.Any())
            {
                foreach (var genre in game.Genres)
                {
                    Console.WriteLine($"- {genre.Name}");
                }
            }
            else
            {
                Console.WriteLine("Жанрів не знайдено.");
            }
        }

        // Explicit Loading для Publishers -> Games
        public void PrintGamesByPublisher(string publisherName)
        {
            Console.WriteLine($"\n--- Ігри видавця: {publisherName} (Explicit Loading) ---");

            var publisher = _db.Publishers.FirstOrDefault(p => p.Name == publisherName);

            if (publisher != null)
            {
                _db.Entry(publisher).Collection(p => p.Games).Load();

                if (publisher.Games.Any())
                {
                    foreach (var game in publisher.Games)
                    {
                        Console.WriteLine($"- {game.Title} (Ціна: ${game.Price})");
                    }
                }
                else
                {
                    Console.WriteLine("Цей видавець ще не має ігор.");
                }
            }
            else
            {
                Console.WriteLine("Видавця не знайдено.");
            }
        }

        public void PrintPublisherOfGameExplicitly(int gameId)
        {
            var game = _db.Games.Find(gameId);
            if (game != null)
            {
                _db.Entry(game).Reference(g => g.Publisher).Load();
                Console.WriteLine($"\nГра '{game.Title}' створена видавцем '{game.Publisher?.Name}' (Explicit Reference)");
            }
        }
    }

    // ------------------------

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            using (var db = new AppDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                var manager = new DbManager(db);

                var pub1 = new Publisher { Name = "Valve", Country = "USA", FoundationYear = 1996, Website = "valvesoftware.com" };
                var pub2 = new Publisher { Name = "CD Projekt", Country = "Poland", FoundationYear = 1994, Website = "cdprojekt.com" };
                var pub3 = new Publisher { Name = "Rockstar Games", Country = "USA", FoundationYear = 1998, Website = "rockstargames.com" };

                manager.CreatePublisher(pub1);
                manager.CreatePublisher(pub2);
                manager.CreatePublisher(pub3);

                var genre1 = new Genre { Name = "Shooter" };
                var genre2 = new Genre { Name = "RPG" };
                var genre3 = new Genre { Name = "Action" };
                var genre4 = new Genre { Name = "Puzzle" };
                var genre5 = new Genre { Name = "Open World" };

                manager.CreateGenre(genre1);
                manager.CreateGenre(genre2);
                manager.CreateGenre(genre3);
                manager.CreateGenre(genre4);
                manager.CreateGenre(genre5);

                var game1 = new Game { Title = "Half-Life 2", ReleaseDate = new DateTime(2004, 11, 16), Price = 9.99m, Rating = 9.8, PublisherId = pub1.Id, Genres = new List<Genre> { genre1, genre3 } };
                var game2 = new Game { Title = "The Witcher 3", ReleaseDate = new DateTime(2015, 5, 19), Price = 29.99m, Rating = 9.9, PublisherId = pub2.Id, Genres = new List<Genre> { genre2, genre3, genre5 } };

                var game3 = new Game { Title = "Cyberpunk 2077", ReleaseDate = new DateTime(2020, 12, 10), Price = 59.99m, Rating = 8.5, PublisherId = pub2.Id, Genres = new List<Genre> { genre2, genre3, genre5 } };
                var game4 = new Game { Title = "Portal 2", ReleaseDate = new DateTime(2011, 4, 18), Price = 9.99m, Rating = 9.7, PublisherId = pub1.Id, Genres = new List<Genre> { genre4 } };
                var game5 = new Game { Title = "Grand Theft Auto V", ReleaseDate = new DateTime(2013, 9, 17), Price = 29.99m, Rating = 9.5, PublisherId = pub3.Id, Genres = new List<Genre> { genre3, genre1, genre5 } };
                var game6 = new Game { Title = "Red Dead Redemption 2", ReleaseDate = new DateTime(2018, 10, 26), Price = 59.99m, Rating = 9.8, PublisherId = pub3.Id, Genres = new List<Genre> { genre3, genre5, genre2 } };

                manager.CreateGame(game1);
                manager.CreateGame(game2);
                manager.CreateGame(game3);
                manager.CreateGame(game4);
                manager.CreateGame(game5);
                manager.CreateGame(game6);

                Console.WriteLine("Дані успішно збережено!\n");

                manager.PrintGamesByGenre("Action");      // 5 гри
                manager.PrintGamesByGenre("Open World");  // 4 гри
                manager.PrintGamesByGenre("Puzzle");      // 1 гра

                manager.PrintGenresOfGame("Cyberpunk 2077");
                manager.PrintGenresOfGame("Portal 2");

                manager.PrintGamesByPublisher("Rockstar Games");
                manager.PrintGamesByPublisher("CD Projekt");

                manager.PrintPublisherOfGameExplicitly(game5.Id);

                Console.WriteLine("\n--- CRUD Операції ---");
                Console.WriteLine("Оновлення ціни Portal 2");
                var gameToUpdate = manager.ReadGame(game4.Id);
                gameToUpdate.Price = 1.99m;
                manager.UpdateGame(gameToUpdate);
                Console.WriteLine($"Нова ціна для Portal 2: ${manager.ReadGame(game4.Id).Price}");

                Console.WriteLine("Видалення шутерів");
                manager.DeleteGenre(genre1.Id);
                Console.WriteLine("Жанр 'Shooter' видалено з бази. (тільки жанр, не ігри)");
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу");
            Console.ReadKey();
        }
    }
}