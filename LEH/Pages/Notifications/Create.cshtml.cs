using LEH.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LEH.Pages.Notifications;

public class CreateModel : PageModel
{
    private readonly AppDbContext _context;

    public CreateModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Notification Notification { get; set; } = new();

    public void OnGet()
    {
        // Можно предварительно подгрузить список пользователей, если нужно
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Notification.CreatedAt = DateTime.Now;
        Notification.Status = string.IsNullOrEmpty(Notification.Status) ? "unread" : Notification.Status;

        _context.Notifications.Add(Notification);
        await _context.SaveChangesAsync();

        return RedirectToPage("Index"); // или другой путь, куда ты хочешь вернуться
    }
}