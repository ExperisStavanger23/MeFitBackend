using MeFitBackend.Data.Entities;
namespace MeFitBackend.Data.DTO.UserRole
{
    public class UserRoleDTO
    {
        //public int Id { get; set; }
        public string UserId { get; set; }
        public int RoleId { get; set; }
        public Data.Entities.Role Role { get; set; }
    }
}
