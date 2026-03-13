using ASP_Shop.Models; // Виправлено
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ASP_Shop.Data // Виправлено
{
    public static class Seeder
    {
        public static async Task Seed(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserModel>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await dbContext.Database.MigrateAsync();

            // 1. СТВОРЕННЯ РОЛІ "Admin"
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // 2. СТВОРЕННЯ АДМІНІСТРАТОРА
            string adminEmail = "admin@shop.com";
            string adminPassword = "123";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new UserModel
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // 3. ДОДАВАННЯ КАТЕГОРІЙ ТА ТОВАРІВ
            if (!await dbContext.Categories.AnyAsync())
            {
                var categories = new List<CategoryModel>
                {
                    new CategoryModel
                    {
                        Name = "Процесори", Icon = "bi bi-cpu",
                        Products = new List<ProductModel>
                        {
                            new() { Name = "AMD Ryzen 5 5600X", Description = "6 ядер", Price = 5999, Amount = 10, Rating = 4.8f, Image = "https://img.ktc.ua/img/base/1_505/2/402682.webp" },
                            new() { Name = "Intel Core i5-12400F", Description = "6 ядер", Price = 7200, Amount = 0, Rating = 4.5f, Image = "https://cdn.27.ua/sc--media--prod/default/51/23/57/512357b8-9d4b-44e4-9fbb-73aab0d413c6.jpg" }
                        }
                    },
                    new CategoryModel
                    {
                        Name = "Відеокарти", Icon = "bi bi-gpu-card",
                        Products = new List<ProductModel>
                        {
                            new() { Name = "NVIDIA RTX 3060", Description = "12GB", Price = 13500, Amount = 5, Rating = 4.9f, Image = "https://scdn.comfy.ua/89fc351a-22e7-41ee-8321-f8a9356ca351/https://cdn.comfy.ua/media/catalog/product/8/_/8_81_332.jpg/w_600g" },
                            new() { Name = "NVIDIA RTX 4090", Description = "24GB", Price = 89999, Amount = 0, Rating = 5.0f, Image = "https://microtron.ua/data/product/cache/671310d43077b.webp" }
                        }
                    }
                };

                await dbContext.Categories.AddRangeAsync(categories);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}