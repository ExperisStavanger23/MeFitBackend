using MeFitBackend.Data.DTO.MuscleGroup;

namespace MeFitBackend.Data.DTO.Exercises
{
    /// <summary>
    /// 
    /// </summary>
    public class ExercisePostDTO
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Video { get; set; }
        public int[] ExerciseMuscleGroupsIds { get; set; } = null!;
    }
}
