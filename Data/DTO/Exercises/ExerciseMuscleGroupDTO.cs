using MeFitBackend.Data.DTO.MuscleGroup;

namespace MeFitBackend.Data.DTO.Exercises
{
    /// <summary>
    /// DTO representing the association between an exercise and musclegroup
    /// </summary>
    public class ExerciseMuscleGroupDTO
    {
        /// <summary>
        /// The id of respective Exercise 
        /// </summary>
        public int ExerciseId {  get; set; }
        /// <summary>
        /// The id of respective MuscleGroup
        /// </summary>
        public int MuscleGroupId { get; set; }
        /// <summary>
        /// Associated MusclGroupDTO
        /// </summary>
        public MuscleGroupDTO MuscleGroup { get; set; } = null!;
    }
}
