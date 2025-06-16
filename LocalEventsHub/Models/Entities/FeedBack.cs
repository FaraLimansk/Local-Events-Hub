namespace LocalEventsHub.Models.Entities
{
    public class Feedback
    {
        public int FeedbackId { get; set; }
        public string Comment { get; set; } = null!;
        public int Rating { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}