using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocalEventsHub.Models.Entities;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly AppDbContext _context;

    public EventsController(AppDbContext context)
    {
        _context = context;
    }

    // 1. Поиск мероприятий по названию (вместо категории)
    [HttpGet("by-title")]
    public async Task<IActionResult> GetEventsByTitle([FromQuery] string title)
    {
        var events = await _context.Events
            .Where(e => e.Title.Contains(title))
            .ToListAsync();

        return Ok(events);
    }

    // 2. Ближайшие события с участниками
    [HttpGet("upcoming")]
    public async Task<IActionResult> GetUpcomingEvents()
    {
        var events = await _context.Events
            .Where(e => e.Date > DateTime.UtcNow)
            .Select(e => new
            {
                e.Title,
                e.Date,
                ParticipantCount = _context.EventRegistrations.Count(er => er.EventId == e.EventId)
            })
            .ToListAsync();

        return Ok(events);
    }

    // 3. Поиск событий по дате
    [HttpGet("search-by-date")]
    public async Task<IActionResult> SearchEventsByDate([FromQuery] DateTime date)
    {
        var events = await _context.Events
            .Where(e => e.Date.Date == date.Date)
            .ToListAsync();

        return Ok(events);
    }

    // 4. Часто используемые даты (вместо популярных категорий)
    [HttpGet("popular-dates")]
    public async Task<IActionResult> GetPopularDates()
    {
        var result = await _context.Events
            .GroupBy(e => e.Date.Date)
            .Select(g => new
            {
                Date = g.Key,
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToListAsync();

        return Ok(result);
    }

    // 5. Проверка дублей по названию и дате
    [HttpPost("check-duplicate")]
    public async Task<IActionResult> CheckDuplicate([FromQuery] string title, [FromQuery] DateTime date)
    {
        var exists = await _context.Events
            .AnyAsync(e => e.Title == title && e.Date.Date == date.Date);

        return Ok(new { Duplicate = exists });
    }

    // 6. Регистрация пользователя на мероприятие
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromQuery] int userId, [FromQuery] int eventId)
    {
        var evt = await _context.Events.FindAsync(eventId);
        if (evt == null || evt.Date < DateTime.UtcNow)
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
