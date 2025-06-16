namespace LocalEventsHub.Models.Entities
{
    public class Badge
    {
        public int BadgeId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public ICollection<UserBadge> UserBadges { get; set; } = new List<UserBadge>();
    }
}