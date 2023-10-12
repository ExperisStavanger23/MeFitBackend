using MeFitBackend.Data.DTO.WorkoutExercise;
using MeFitBackend.Data.Enums;

namespace MeFitBackend.Data.DTO.Workouts
{
    public class WorkoutPostDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string RecommendedLevel { get; set; }
        public string Image { get; set; }
        public int Duration { get; set; }
        
        public WorkoutExerciseDTO[] WorkoutExercises { get; set; }
    }
}
