namespace LEH.Models;

public class Event
{
    public int EventId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime EventDate { get; set; }
    public int? RouteId { get; set; }
    public int OrganizerId { get; set; }
    public User Organizer { get; set; }
    public int? MaxParticipants { get; set; }

    // Навигационные свойства
    public ICollection<EventRegistration> Registrations { get; set; }
    public ICollection<Feedback> Feedbacks { get; set; }
}