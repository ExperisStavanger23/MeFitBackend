namespace MeFitBackend.Data.Entities
{
    public class UserGoal
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public GoalType GoalType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
