using System.ComponentModel.DataAnnotations.Schema;

namespace MeFitBackend.Data.Entities
{
    [Table(nameof(UserWorkout))]
    public class UserWorkout
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int WorkoutId { get; set; }
        public Workout Workout { get; set; }
    }
}
