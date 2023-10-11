using MeFitBackend.Data.Entities;

namespace MeFitBackend.Data.DTO.UserWorkout
{
    public class UserWorkoutDTO
    {
        public int Id { get; set; } 
        public int UserId { get ; set; }
        public int WorkoutId { get; set; }
        public Workout Workout {  get; set; }
    }
}
