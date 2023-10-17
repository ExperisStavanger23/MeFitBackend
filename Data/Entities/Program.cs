using MeFitBackend.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeFitBackend.Data.Entities
{
    [Table(nameof(Program))]
    public class Program
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [StringLength(50)]
        public string Description { get; set; } = null!;
        public ProgramCategory Category { get; set; }
        public Level RecommendedLevel { get; set; }
        public string? Image {  get; set; }
        public int Duration { get; set; }
        // Navigation
        public ICollection<Workout> Workouts { get; set; } = new List<Workout>(); // 1-M
        public ICollection<UserProgram> UserPrograms { get; set; } = new List<UserProgram>();

    }
}
