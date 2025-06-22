namespace LEH.Models;

public class Challenge
{
    public int ChallengeId { get; set; }
    public string Category { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

    public string Operation { get; set; }
    public int? RouteId { get; set; }
}