// Models/Event.cs
using System.ComponentModel.DataAnnotations;

namespace LEH.Models
{
    public class Event
    {
        public int EventId { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        [Required]
        public DateTime EventDate { get; set; }  // Соответствует event_date в БД
        
        public int? RouteId { get; set; }  // Опционально, согласно БД
        
        [Required]
        public int OrganizerId { get; set; }  // Внешний ключ
        
        public User Organizer { get; set; }
        
        public int? MaxParticipants { get; set; }  // nullable, как в БД
    }
}