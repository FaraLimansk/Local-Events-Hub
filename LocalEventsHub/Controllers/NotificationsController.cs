namespace DefaultNamespace;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly AppDbContext _context;

    public NotificationsController(AppDbContext context)
    {
        _context = context;
    }

    // Уведомления пользователя
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetNotifications(int userId)
    {
        var notifications = await _context.Notifications
            .Where(n => n.UserId == userId && n.Status == "unread")
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();

        return Ok(notifications);
    }
}
