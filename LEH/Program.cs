using LEH;
using LEH.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Конфигурация сервисов ДО builder.Build()
builder.Services.AddRazorPages();

// 2. Добавление DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. Настройка TempData (перенесено ДО Build())
builder.Services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
builder.Services.Configure<CookieTempDataProviderOptions>(options => 
{
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// 4. Middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();

// 5. Проверка БД (только для разработки)
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        try
        {
            var usersCount = dbContext.Users.Count();
            Console.WriteLine($"База данных доступна. Пользователей: {usersCount}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка доступа к БД: {ex.Message}");
        }
    }
}

app.Run();