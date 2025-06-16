namespace DefaultNamespace;
using Microsoft.AspNetCore.Mvc;

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
            .Where(a => a.ModeratorId == moderatorId)
            .OrderByDescending(a => a.ActionDate)
            .ToListAsync();

        return Ok(actions);
    }
}
