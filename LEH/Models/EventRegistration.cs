namespace LEH.Models;

public class EventRegistration
{
    public int RegistrationId { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

    public int EventId { get; set; }
    public Event Event { get; set; }

    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    public int? RouteId { get; set; }
}