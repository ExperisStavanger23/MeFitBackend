using MeFitBackend.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeFitBackend.Data.Entities
{
    [Table(nameof(User))]
    public class User
    {
        public string Id { get; set; } = null!;
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [StringLength(250)]
        public string? Bio { get; set; }
        [StringLength(50)]
        public string Email { get; set; } = null!;
        public string? ProfilePicture { get; set; }
        public Level ExperienceLvl { get; set; }
        public string Gender { get; set; } = null!;
        public int Weight { get; set; }
        public int Height { get; set; }
        public DateTime Birthday { get; set; }
        public int? RoleId { get; set; }
        public Role? Role { get; set; }
        public ICollection<Goal> Goals { get; set; }
        public ICollection<Exercise> Exercises { get; set; }
        public ICollection<Workout> Workouts { get; set; }  
        public ICollection<Program> Programs { get; set; }

    }
}
