namespace LEH.Models;

public class Feedback
{
    public int FeedbackId { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

    public string Message { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int? EventId { get; set; }
    public Event Event { get; set; }
}