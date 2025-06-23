using LEH;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Add Razor Pages
builder.Services.AddRazorPages();

// 2. Add AppDbContext with PostgreSQL connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// 3. Error handling for production
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// 4. Middlewares
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// 5. Razor Pages routing
app.MapRazorPages();

// 6. DB Test (optional, for development)
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    try
    {
        var usersCount = dbContext.Users.Count();
        Console.WriteLine($"✅ База данных доступна. Пользователей: {usersCount}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Ошибка доступа к БД: {ex.Message}");
    }
}

app.Run();