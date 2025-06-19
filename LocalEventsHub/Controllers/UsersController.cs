using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    // Статистика авторов
    [HttpGet("organizer-stats")]
    public async Task<IActionResult> GetOrganizerStats()
    {
        var stats = await _context.Users
            .Select(u => new
            {
                u.Username,
                EventsCreated = u.OrganizedEvents.Count
            })
            .ToListAsync();

        return Ok(stats);
    }
}