namespace LocalEventsHub.Models.Entities
{
    public class Challenge
    {
        public int ChallengeId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}