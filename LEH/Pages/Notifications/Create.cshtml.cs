using LEH.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LEH.Pages.Notifications
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(AppDbContext context, ILogger<CreateModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Notification Notification { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                // Можно добавить предзаполнение данных, если нужно
                Notification.CreatedAt = DateTime.UtcNow;
                Notification.Status = "unread";
                
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при загрузке страницы создания уведомления");
                return RedirectToPage("/Error");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Модель не валидна: {@ModelState}", ModelState);
                return Page();
            }

            try
            {
                _logger.LogInformation("Попытка сохранить уведомление: {@Notification}", Notification);
        
                var userExists = await _context.Users.AnyAsync(u => u.UserId == Notification.UserId);
                if (!userExists)
                {
                    _logger.LogWarning("Пользователь с ID {UserId} не найден", Notification.UserId);
                    ModelState.AddModelError("Notification.UserId", "Пользователь не найден");
                    return Page();
                }

                Notification.CreatedAt = DateTime.UtcNow;
                Notification.Status = string.IsNullOrEmpty(Notification.Status) ? "unread" : Notification.Status;

                _context.Notifications.Add(Notification);
                await _context.SaveChangesAsync();
        
                _logger.LogInformation("Уведомление успешно сохранено с ID {NotificationId}", Notification.NotificationId);

                TempData["SuccessMessage"] = "Уведомление успешно создано!";
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Ошибка при сохранении уведомления. Внутреннее исключение: {InnerException}", ex.InnerException?.Message);
                ModelState.AddModelError("", "Не удалось сохранить уведомление. Пожалуйста, попробуйте позже.");
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Неожиданная ошибка при создании уведомления");
                return RedirectToPage("/Error");
            }
        }
    }
}