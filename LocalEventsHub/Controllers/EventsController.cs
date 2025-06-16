using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocalEventsHub.Data;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly AppDbContext _context;

    public EventsController(AppDbContext context)
    {
        _context = context;
    }

    // 1. Мероприятия по категории
    [HttpGet("by-category")]
    public async Task<IActionResult> GetEventsByCategory([FromQuery] string category)
    {
        var events = await _context.Events
            .Where(e => e.Category == category)
            .ToListAsync();

        return Ok(events);
    }

    // 2. Ближайшие события с участниками
    [HttpGet("upcoming")]
    public async Task<IActionResult> GetUpcomingEvents()
    {
        var events = await _context.Events
            .Where(e => e.EventDate > DateTime.UtcNow)
            .Select(e => new
            {
                e.Title,
                e.EventDate,
                ParticipantCount = _context.EventRegistrations.Count(er => er.EventId == e.EventId)
            })
            .ToListAsync();

        return Ok(events);
    }

    // 3. Поиск событий по дате и месту
    [HttpGet("search")]
    public async Task<IActionResult> SearchEvents([FromQuery] DateTime date, [FromQuery] string location)
    {
        var events = await _context.Events
            .Where(e => e.EventDate.Date == date.Date && e.Location.Contains(location))
            .ToListAsync();

        return Ok(events);
    }

    // 4. Популярные категории
    [HttpGet("popular-categories")]
    public async Task<IActionResult> GetPopularCategories()
    {
        var result = await _context.Events
            .GroupBy(e => e.Category)
            .Select(g => new
            {
                Category = g.Key,
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToListAsync();

        return Ok(result);
    }

    // 5. Проверка дублей событий
    [HttpPost("check-duplicate")]
    public async Task<IActionResult> CheckDuplicate([FromQuery] string title, [FromQuery] DateTime date)
    {
        var exists = await _context.Events
            .AnyAsync(e => e.Title == title && e.EventDate.Date == date.Date);

        return Ok(new { Duplicate = exists });
    }

    // 6. Закрытие регистрации (встроенная проверка)
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromQuery] int userId, [FromQuery] int eventId)
    {
        var evt = await _context.Events.FindAsync(eventId);
        if (evt == null || evt.EventDate < DateTime.UtcNow)
            return BadRequest("Регистрация закрыта");

        var alreadyRegistered = await _context.EventRegistrations
            .AnyAsync(r => r.UserId == userId && r.EventId == eventId);

        if (alreadyRegistered)
            return Conflict("Пользователь уже зарегистрирован");

        var registration = new EventRegistration
        {
            UserId = userId,
            EventId = eventId,
            RegistrationDate = DateTime.UtcNow
        };

        _context.EventRegistrations.Add(registration);
        await _context.SaveChangesAsync();

        return Ok("Регистрация успешна");
    }
}
