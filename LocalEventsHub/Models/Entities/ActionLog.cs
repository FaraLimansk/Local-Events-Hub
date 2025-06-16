namespace LocalEventsHub.Models.Entities
{
    public class ActionLog
    {
        public int ActionLogId { get; set; }
        public string Action { get; set; } = null!;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}