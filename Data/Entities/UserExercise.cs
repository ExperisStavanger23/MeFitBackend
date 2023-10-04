namespace MeFitBackend.Data.Entities
{
    public class UserExercise
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }

    }
}
