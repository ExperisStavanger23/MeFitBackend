namespace MeFitBackend.Data.DTO.Exercises
{
    /// <summary>
    /// DTO representing Exercise in a standalone Exercise context
    /// </summary>
    public class ExerciseDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Optionable
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Optionable image url string
        /// </summary>
        public string? Image { get; set; }
        /// <summary>
        /// Optionable youtube video URL string
        /// </summary>
        public string? Video { get; set; }
        /// <summary>
        /// Associated data of muslcegroups for this specific exercise
        /// </summary>
        public ICollection<ExerciseMuscleGroupDTO> ExerciseMuscleGroups { get; set; } = null!;
    }
}
