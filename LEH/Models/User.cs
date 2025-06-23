namespace LEH.Models;
using System.ComponentModel.DataAnnotations;

public class User
{
        public int UserId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Username { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        // Навигационные свойства
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Event> OrganizedEvents { get; set; }
        public ICollection<EventRegistration> EventRegistrations { get; set; }
        public ICollection<Challenge> Challenges { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<UserBadge> UserBadges { get; set; }
}