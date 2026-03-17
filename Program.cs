using Microsoft.EntityFrameworkCore;
using QuestBooking.Infrastructure; // Простір імен твого контексту
using QuestBooking.Domain.Model;   // Простір імен твоїх моделей

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<QuestBookingIcptContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Додавання сервісів для MVC (контролери та представлення)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 2. БЛОК ПЕРЕВІРКИ ПІДКЛЮЧЕННЯ ДО БАЗИ ДАНИХ
// Цей код виконається один раз при запуску програми
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<QuestBookingIcptContext>();
        // Перевіряємо, чи може застосунок "достукатися" до PostgreSQL
        if (context.Database.CanConnect())
        {
            Console.WriteLine("\n>>>> [DB TEST] УСПІХ: Підключення до бази QuestBookingICPT встановлено!");
        }
        else
        {
            Console.WriteLine("\n>>>> [DB TEST] ПОМИЛКА: База даних доступна, але підключення відхилено.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("\n>>>> [DB TEST] КРИТИЧНА ПОМИЛКА ПІДКЛЮЧЕННЯ:");
        Console.WriteLine(ex.Message);
        // Якщо тут виникне помилка 28P01 — значить пароль в appsettings.json невірний
    }
}

// Налаштування конвеєра HTTP-запитів
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Налаштування маршруту за замовчуванням
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Questrooms}/{action=Index}/{id?}");

app.Run();