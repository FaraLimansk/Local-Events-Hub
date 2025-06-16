namespace LocalEventsHub.Models.Entities
{
    public class EventRegistration
    {
        public int EventRegistrationId { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    }
}