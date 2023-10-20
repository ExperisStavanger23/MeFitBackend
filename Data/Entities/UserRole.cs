using System.ComponentModel.DataAnnotations.Schema;

namespace MeFitBackend.Data.Entities
{
    [Table(nameof(UserRole))]
    public class UserRole
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int RoleId { get; set; }
        public Role? Role { get; set; }
    }
}
