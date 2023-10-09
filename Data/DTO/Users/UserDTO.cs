using MeFitBackend.Data.Entities;
using MeFitBackend.Data.Enums;

namespace MeFitBackend.Data.DTO.Users
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio {  get; set; }
        public string Email { get; set; }
        public string? ProfilePicture { get; set; }
        public Level ExperienceLvl { get; set; }
        public string Gender { get; set; } = null!;
        public int Weight { get; set; }
        public int Height { get; set; }
        public DateTime Birthday { get; set; }
        // public int RoleId { get; set; }
        public Role Role { get; set; }
        public int[] Goals { get; set; }
        public int[] Created { get; set; }
        public int[] UserExercises { get; set; }
        public int[] UserWorkouts { get; set; }
        public int[] UserPrograms { get; set; }
    }
}
