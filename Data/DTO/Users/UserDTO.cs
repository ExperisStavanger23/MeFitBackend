using MeFitBackend.Data.Entities;
using MeFitBackend.Data.Enums;

namespace MeFitBackend.Data.DTO.Users
{
    public class UserDTO
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; }
        public string Bio {  get; set; }
        public string Email { get; set; }
        public string? ProfilePicture { get; set; }
        public Level ExperienceLvl { get; set; }
        public string Gender { get; set; } = null!;
        public int Weight { get; set; }
        public int Height { get; set; }
        public DateTime Birthday { get; set; }
        public int? RoleId { get; set; }
        public Role? Role { get; set; }
        public int[] Goals { get; set; }
        public int[] Created { get; set; }
        public int[] Exercises { get; set; }
        public int[] Workouts { get; set; }
        public int[] Programs { get; set; }
    }
}
