using MeFitBackend.Data.Entities;

namespace MeFitBackend.Data.DTO.Exercises
{
    public class ExerciseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
        public int[] UserExercises { get; set; }
        public int[] MuscleGroups { get; set; }
    }
}
