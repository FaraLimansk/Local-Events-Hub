using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // для ToListAsync()
using System.Linq; // для LINQ-методов, как Where, OrderBy и т.д.
namespace DefaultNamespace;


[ApiController]
[Route("api/[controller]")]
public class ActionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ActionsController(AppDbContext context)
    {
        _context = context;
    }

    // Логи действий модераторов
    [HttpGet("moderator/{moderatorId}")]
    public async Task<IActionResult> GetModeratorActions(int moderatorId)
    {
        var actions = await _context.Actions
            .Where(a => a.UserId == moderatorId) // если модератор — это пользователь
            .OrderByDescending(a => a.Timestamp) // используем существующее поле
            .ToListAsync();

        return Ok(actions);
    }
}
