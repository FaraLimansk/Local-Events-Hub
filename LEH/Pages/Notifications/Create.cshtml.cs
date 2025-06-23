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
                return Page();
            }

            try
            {
                // Валидация пользователя
                var userExists = await _context.Users.AnyAsync(u => u.UserId == Notification.UserId);
                if (!userExists)
                {
                    ModelState.AddModelError("Notification.UserId", "Пользователь не найден");
                    return Page();
                }

                // Установка значений по умолчанию
                Notification.CreatedAt = DateTime.UtcNow;
                Notification.Status = string.IsNullOrEmpty(Notification.Status) ? "unread" : Notification.Status;

                _context.Notifications.Add(Notification);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Уведомление успешно создано!";
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Ошибка при сохранении уведомления");
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