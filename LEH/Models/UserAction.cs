using System.ComponentModel.DataAnnotations;

namespace LEH.Models;

public class UserAction
{
    [Key]
    public int ActionId { get; set; }

    public string TargetObject { get; set; }
    public string OperatorLogId { get; set; }
    public int? ModeratorId { get; set; }
    public User Moderator { get; set; }

    public DateTime ActionDate { get; set; } = DateTime.UtcNow;
    public string Details { get; set; }
}