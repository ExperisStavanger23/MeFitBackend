using MeFitBackend.Data.DTO.MuscleGroup;

namespace MeFitBackend.Data.DTO.Exercises
{
    public class ExerciseMuscleGroupDTO
    {
        public int ExerciseId {  get; set; }
        public int MuscleGroupId { get; set; }
        public MuscleGroupDTO MuscleGroup { get; set; }
    }
}
