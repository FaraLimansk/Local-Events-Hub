namespace LEH.Models;

public class Notification
{
    public int NotificationId { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

    public string Type { get; set; }
    public string Message { get; set; }
    public string Status { get; set; } = "unread";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string RelatedOperation { get; set; }
    public string Comments { get; set; }
}