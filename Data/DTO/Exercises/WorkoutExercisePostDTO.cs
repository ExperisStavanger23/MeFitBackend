namespace MeFitBackend.Data.DTO.Exercises
{
    /// <summary>
    /// DTO used for assinging exercise with reps and sets to a workout
    /// </summary>
    public class WorkoutExercisePostDTO
    {
        public int ExerciseId { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
    }
}
