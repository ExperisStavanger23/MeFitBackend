using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeFitBackend.Data.Entities
{
    [Table(nameof(Program))]
    public class Program
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        public string Category { get; set; }
        public string RecomendedLvl { get; set; }
        public string Image {  get; set; }
        public int Duration { get; set; }
        // Navigation
        public ICollection<Workout> Workouts { get; set; } // M-M
        public ICollection<UserProgram> UserPrograms { get; set; }
    }
}
