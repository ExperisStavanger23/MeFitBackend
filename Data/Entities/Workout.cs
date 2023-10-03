using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeFitBackend.Data.Entities
{
    [Table(nameof(Workout))]
    public class Workout
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string RecomendedLvl { get; set; } = null!;
        public string? Image {  get; set; }
        public int Duration { get; set; }
        // Navigation
        public ICollection<Exercise> Exercise { get; set; } = null!; // 1-M
        
    }
}
