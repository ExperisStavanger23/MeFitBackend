using System.ComponentModel.DataAnnotations.Schema;

namespace MeFitBackend.Data.Entities
{
    [Table(nameof(ExerciseMuscleGroup))]
    public class ExerciseMuscleGroup
    {
        public int ExerciseId { get; set; }
        [ForeignKey("ExerciseId")]
        public Exercise Exercise { get; set; }

        public int MuscleGroupId { get; set; }
        [ForeignKey("MuscleGroupId")]
        public MuscleGroup MuscleGroup { get; set; }
    }
}
