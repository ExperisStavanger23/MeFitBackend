using MeFitBackend.Data.Enums;
using MeFitBackend.Data.DTO.Exercises;
using MeFitBackend.Data.DTO.MuscleGroup;

namespace MeFitBackend.Data.DTO.UserWorkout
{
    public class UserWorkoutWithDetailsDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public WorkoutCategory Category { get; set; }
        public Level RecommendLevel { get; set; }
        public string? Image { get; set; }
        public int Duration { get; set; }
    }
}
