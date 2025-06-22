namespace LEH.Models;

public class EventRegistration
{
    public int RegistrationId { get; set; } // Первичный ключ
    
    // Внешние ключи
    public int UserId { get; set; }
    public int EventId { get; set; }
    
    // Навигационные свойства
    public User User { get; set; }
    public Event Event { get; set; }
    
    // Свойства
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    public int? RouteId { get; set; } // Опциональный внешний ключ

    // Опционально: навигационное свойство для Route, если нужно
    // public Route Route { get; set; }
}