using ASP_Shop.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP_Shop.Data
{
    public static class Seeder
    {
        public static void Seed(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            dbContext.Database.Migrate();

            if (!dbContext.Categories.Any())
            {
                var categories = new List<CategoryModel>
                {
                    new CategoryModel
                    {
                        Name = "Процесори", Icon = "bi bi-cpu",
                        Products = new List<ProductModel>
                        {
                            new() { Name = "AMD Ryzen 5 5600X", Description = "6 ядер", Price = 5999, Amount = 10, Rating = 4.8f, Image = "https://m.media-amazon.com/images/I/61vGQNUEsGL._AC_SL1500_.jpg" },
                            new() { Name = "Intel Core i5-12400F", Description = "6 ядер", Price = 7200, Amount = 0, Rating = 4.5f, Image = "https://m.media-amazon.com/images/I/61uJwN8h7PL._AC_SL1500_.jpg" }
                        }
                    },
                    new CategoryModel
                    {
                        Name = "Відеокарти", Icon = "bi bi-gpu-card",
                        Products = new List<ProductModel>
                        {
                            new() { Name = "NVIDIA RTX 3060", Description = "12GB", Price = 13500, Amount = 5, Rating = 4.9f, Image = "https://m.media-amazon.com/images/I/71d1i7e4jSL._AC_SL1500_.jpg" },
                            new() { Name = "NVIDIA RTX 4090", Description = "24GB", Price = 89999, Amount = 0, Rating = 5.0f, Image = "https://m.media-amazon.com/images/I/81Z8RZ9h1wL._AC_SL1500_.jpg" }
                        }
                    }
                };

                dbContext.Categories.AddRange(categories);
                dbContext.SaveChanges();
            }
        }
    }
}
