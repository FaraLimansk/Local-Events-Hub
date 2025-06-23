using LEH.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LEH.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


public class CreateModel : PageModel
{
    private readonly AppDbContext _context;

    public CreateModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Event Event { get; set; }

    public void OnGet()
    {
        // Пусто — просто отображаем форму
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        _context.Events.Add(Event);
        await _context.SaveChangesAsync();

        return RedirectToPage("Index"); // вернёт к списку событий
    }
}
