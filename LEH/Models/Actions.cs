namespace LEH.Models;

public class Actions
{
        public int ActionId { get; set; }
        public string TargetObject { get; set; }
        public string OperatorLogId { get; set; }
        public int? ModeratorId { get; set; }
        public User Moderator { get; set; }

        public DateTime ActionDate { get; set; } = DateTime.UtcNow;
        public string Details { get; set; }
}