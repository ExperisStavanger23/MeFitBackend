using MeFitBackend.Data.Entities;
using MeFitBackend.Data.Enums;

namespace MeFitBackend.Data.DTO.Users
{
    public class UserDTO
    {
        public string Id { get; set; }= null!;
        public string Name { get; set; }= null!;
        public string Bio {  get; set; }= null!;
        public string Email { get; set; }= null!;
        public string? ProfilePicture { get; set; }
        public string ExperienceLvl { get; set; }= null!;
        public string Gender { get; set; } = null!;
        public int Weight { get; set; }
        public int Height { get; set; }
        public DateTime Birthday { get; set; }
        // public int RoleId { get; set; }
        public string Role { get; set; }= null!;
        public int[] Goals { get; set; }= null!;
        public int[] Created { get; set; }= null!;
        public int[] UserExercises { get; set; }= null!;
        public int[] UserWorkouts { get; set; }= null!;
        public int[] UserPrograms { get; set; }= null!;
    }
}
