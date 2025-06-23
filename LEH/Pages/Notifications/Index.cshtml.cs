using LEH.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LEH.Pages.Notifications
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(AppDbContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<Notification> Notifications { get; set; } = new();

        public async Task OnGetAsync()
        {
            try
            {
                Notifications = await _context.Notifications
                    .Include(n => n.User) // Если есть навигационное свойство
                    .OrderByDescending(n => n.CreatedAt)
                    .Take(100) // Ограничение для больших таблиц
                    .AsNoTracking() // Для оптимизации
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при загрузке уведомлений");
                Notifications = new List<Notification>();
            }
        }
    }
}