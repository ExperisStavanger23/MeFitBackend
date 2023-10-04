using Microsoft.Identity.Client;

namespace MeFitBackend.Data.Entities
{
    public class Goal
    {
        public int GoaldId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<UserGoal> UserGoals{ get; set; }
    } 
}
