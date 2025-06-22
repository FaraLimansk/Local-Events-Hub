namespace LEH.Models;

public class Badge
{
    public int BadgeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    // Навигационные свойства
    public ICollection<UserBadge> UserBadges { get; set; }
}