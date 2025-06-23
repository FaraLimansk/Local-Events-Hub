using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LEH.Models;

namespace LEH.Pages.Notifications
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Notification> Notifications { get; set; } = new();

        public async Task OnGetAsync()
        {
            Notifications = await _context.Notifications
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }
    }
}