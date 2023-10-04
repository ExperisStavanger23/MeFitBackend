using System.ComponentModel.DataAnnotations.Schema;

namespace MeFitBackend.Data.Entities
{
    [Table(nameof(MuscleGroup))]
    public class MuscleGroup
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int ExerciseId { get; set; }
        public Exercise? Exercise { get; set; }
    }
}
