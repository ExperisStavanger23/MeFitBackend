using MeFitBackend.Data.DTO.UserGoal;
using MeFitBackend.Data.Entities;
using MeFitBackend.Data.Enums;

namespace MeFitBackend.Data.DTO.Goal
{
    public class GoalDTO
    {
        public int Id { get; set; }
        public GoalType GoalType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<UserGoalDTO> UserGoals { get; set; }
    }
}
