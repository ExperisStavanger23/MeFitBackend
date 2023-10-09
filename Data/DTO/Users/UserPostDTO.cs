using MeFitBackend.Data.Entities;

namespace MeFitBackend.Data.DTO.Users
{
    public class UserPostDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Bio {  get; set; }
        public string Email { get; set; }
        public string ProfilePicture { get; set; }
        public string Gender { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public DateTime Birthday { get; set; }
        // public int RoleId { get; set; }
    }
}
