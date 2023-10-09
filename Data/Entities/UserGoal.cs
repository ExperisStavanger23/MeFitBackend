using MeFitBackend.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeFitBackend.Data.Entities
{
    [Table(nameof(UserGoal))]
    public class UserGoal
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int GoalId { get; set; }
        public Goal Goal { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
