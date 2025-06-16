namespace LocalEventsHub.Models.Entities
{
    public class Event
    {
        public int EventId { get; set; }
        public string Title { get; set; } = null!;
        public DateTime Date { get; set; }
        public int OrganizerId { get; set; }

        public User Organizer { get; set; } = null!;
        public ICollection<EventRegistration> Registrations { get; set; } = new List<EventRegistration>();
    }
}
