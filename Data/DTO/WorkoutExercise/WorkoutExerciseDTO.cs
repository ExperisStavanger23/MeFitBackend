using MeFitBackend.Data.DTO.Exercises;
using MeFitBackend.Data.DTO.MuscleGroup;

namespace MeFitBackend.Data.DTO.WorkoutExercise
{
    public class WorkoutExerciseDTO
    {
        /* Exercise DTO used for workout, with specs */
        public int Id { get; set; }
        public int ExerciseId { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }
        public ICollection<ExerciseMuscleGroupDTO> MuscleGroupInExerciseDTOs { get; set; }

    }
}
