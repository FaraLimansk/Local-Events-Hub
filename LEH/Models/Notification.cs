using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LEH.Models;

public class Notification
{
    public int NotificationId { get; set; }
    
    [Required]
    public int UserId { get; set; }
    
    [ForeignKey("UserId")]
    public User? User { get; set; }  // Делаем nullable

    [Required]
    public string Type { get; set; } = null!;
    
    [Required]
    public string Message { get; set; } = null!;
    
    public string Status { get; set; } = "unread";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? RelatedOperation { get; set; }
    public string? Comments { get; set; }
}