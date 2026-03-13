using ASP_Shop.Data;
using ASP_Shop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Додавання базових сервісів
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Підключення Бази Даних (MySQL)
builder.Services.AddDbContext<AppDbContext>(options =>
{
    string? connectionString = builder.Configuration.GetConnectionString("LocalDb");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Підключення Identity (Користувачі та Ролі)
builder.Services.AddDefaultIdentity<UserModel>(options =>
{
    // Спрощуємо вимоги для зручного тестування
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3; // Пароль від 3 символів
})
.AddRoles<IdentityRole>() // Активація ролей (для перевірки "Admin")
.AddEntityFrameworkStores<AppDbContext>();

// 4. Підключення Сесій (для роботи Кошика)
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // час збереження кошика
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Налаштування конвеєра HTTP-запитів
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession();        // 1. Сесії
app.UseAuthentication(); // 2. Аутентифікація
app.UseAuthorization();  // 3. Авторизація

app.MapStaticAssets(); 

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages();

await app.Seed();

app.Run();