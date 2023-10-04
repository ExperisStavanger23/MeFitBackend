using MeFitBackend.Data.Enums;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeFitBackend.Data.Entities
{
    [Table(nameof(Goal))]

    public class Goal
    {
        public int Id { get; set; }
        public GoalType GoalType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<UserGoal> UserGoals{ get; set; } = new List<UserGoal>();
    } 
}
