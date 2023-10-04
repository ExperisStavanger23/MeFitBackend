namespace MeFitBackend.Data.Entities
{
    public class UserWorkout
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int WorkoutId { get; set; }
        public Workout Workout { get; set; }
    }
}
