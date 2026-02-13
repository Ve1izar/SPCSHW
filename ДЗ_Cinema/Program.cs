using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ДЗ_Cinema
{
    public enum Genre
    {
        Comedy,
        Horror,
        Adventure,
        Drama,
        Action,
        SciFi
    }

    public class Director : ICloneable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Director(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }

    public class Movie : ICloneable, IComparable<Movie>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Director MovieDirector { get; set; }
        public string Country { get; set; }
        public Genre MovieGenre { get; set; }
        public int Year { get; set; }
        public double Rating { get; set; }

        public Movie(string title, string desc, Director director, string country, Genre genre, int year, double rating)
        {
            Title = title;
            Description = desc;
            MovieDirector = director;
            Country = country;
            MovieGenre = genre;
            Year = year;
            Rating = rating;
        }

        public object Clone()
        {
            Movie clone = (Movie)this.MemberwiseClone();
            clone.MovieDirector = (Director)this.MovieDirector.Clone();
            return clone;
        }

        public int CompareTo(Movie other)
        {
            if (other == null) return 1;
            return string.Compare(this.Title, other.Title, StringComparison.Ordinal);
        }

        public override string ToString()
        {
            return $"Title: \"{Title}\" | Year: {Year} | Rating: {Rating} | Genre: {MovieGenre} | Dir: {MovieDirector}";
        }
    }

    public class CompareByRating : IComparer<Movie>
    {
        public int Compare(Movie x, Movie y)
        {
            if (x == null || y == null) return 0;
            return y.Rating.CompareTo(x.Rating);
        }
    }

    public class CompareByYear : IComparer<Movie>
    {
        public int Compare(Movie x, Movie y)
        {
            if (x == null || y == null) return 0;
            return y.Year.CompareTo(x.Year);
        }
    }

    public class Cinema : IEnumerable<Movie>
    {
        private List<Movie> _movies;
        public string Address { get; set; }

        public Cinema(string address)
        {
            Address = address;
            _movies = new List<Movie>();
        }

        public void AddMovie(Movie movie)
        {
            _movies.Add(movie);
        }

        public void Sort()
        {
            _movies.Sort();
        }

        public void Sort(IComparer<Movie> comparer)
        {
            _movies.Sort(comparer);
        }

        public IEnumerator<Movie> GetEnumerator()
        {
            return _movies.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Director dir1 = new Director("Christopher", "Nolan");
            Director dir2 = new Director("Quentin", "Tarantino");
            Director dir3 = new Director("Greta", "Gerwig");

            Movie m1 = new Movie("Inception", "Dreams within dreams", dir1, "USA", Genre.SciFi, 2010, 8.8);
            Movie m2 = new Movie("Pulp Fiction", "Crime drama", dir2, "USA", Genre.Drama, 1994, 8.9);
            Movie m3 = new Movie("Barbie", "Doll comes to life", dir3, "USA", Genre.Comedy, 2023, 7.0);
            Movie m4 = new Movie("Interstellar", "Space travel", dir1, "USA", Genre.SciFi, 2014, 8.7);

            Cinema cinema = new Cinema("Kyiv, Khreshchatyk 1");
            cinema.AddMovie(m1);
            cinema.AddMovie(m2);
            cinema.AddMovie(m3);
            cinema.AddMovie(m4);

            Console.WriteLine(" Початковий список:");
            PrintCinema(cinema);

            Console.WriteLine("\n Сортування за назвою:");
            cinema.Sort();
            PrintCinema(cinema);

            Console.WriteLine("\n Сортування за рейтингом:");
            cinema.Sort(new CompareByRating());
            PrintCinema(cinema);

            Console.WriteLine("\n Сортування за роком: ");
            cinema.Sort(new CompareByYear());
            PrintCinema(cinema);

            Console.WriteLine("\n Перевірка ICloneable:");
            Movie original = m1;
            Movie clone = (Movie)original.Clone();

            Console.WriteLine("Змінюємо ім'я режисера у клоні");
            clone.MovieDirector.FirstName = "UNKNOWN";
            clone.Title = "Inception (CLONE)";

            Console.WriteLine($"Оригінал: {original}");
            Console.WriteLine($"Клон:     {clone}");

            if (original.MovieDirector.FirstName == "Christopher")
            {
                Console.WriteLine("Успіх! Глибоке копіювання працює.");
            }
            else
            {
                Console.WriteLine("Помилка! Поверхове копіювання.");
            }

            Console.ReadKey();
        }

        static void PrintCinema(Cinema cinema)
        {
            foreach (var movie in cinema)
            {
                Console.WriteLine(movie);
            }
        }
    }
}
