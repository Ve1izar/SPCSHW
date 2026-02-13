using Games.Model;
using Microsoft.EntityFrameworkCore;

namespace Games.DataAccess
{
    public class GamesContext : DbContext
    {
        public DbSet<Game> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Рядок підключення до MySQL
            var connectionString = "Server=localhost;Database=GamesDb;User=root;Password=Jz&c7!kKA%t=wEh&7R;";

            // Використовуємо драйвер Pomelo для MySQL
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}