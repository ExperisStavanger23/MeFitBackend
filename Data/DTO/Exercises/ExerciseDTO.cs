using MeFitBackend.Data.DTO.MuscleGroup;
using MeFitBackend.Data.DTO.UserExercise;
using MeFitBackend.Data.Entities;

namespace MeFitBackend.Data.DTO.Exercises
{
    /* Exercise DTO used for plain Exercise (not for a workout) */
    public class ExerciseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
        public ICollection<ExerciseMuscleGroupDTO> ExerciseMuscleGroups { get; set; }
    }
}
