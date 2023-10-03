using System.ComponentModel.DataAnnotations.Schema;

namespace MeFitBackend.Data.Entities
{
    [Table(nameof(Exercise))]
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string MuscleGroup { get; set; } = null!;
        public string? Image {  get; set; }
        public string? Video { get; set; }
    }
}
