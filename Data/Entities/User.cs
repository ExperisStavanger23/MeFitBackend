using MeFitBackend.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeFitBackend.Data.Entities
{
    [Table(nameof(User))]
    public class User
    {
        public int Id { get; set; }
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
        public int Age { get; set; }
        // public int RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<UserGoal> Goals { get; set; } = new List<UserGoal>();
        public ICollection<Created> Created { get; set; } = new List<Created>();
        public ICollection<UserExercise> UserExercises { get; set; } = new List<UserExercise>();
        public ICollection<UserWorkout> UserWorkouts { get; set; } = new List<UserWorkout>();        
        public ICollection<UserProgram> UserPrograms { get; set; } = new List<UserProgram>();

    }
}
