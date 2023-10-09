using System.ComponentModel.DataAnnotations.Schema;

namespace MeFitBackend.Data.Entities
{
    [Table(nameof(Role))]

    public class Role
    {
        public int Id { get; set; }
        public string RoleTitle { get; set; } = null!;

        // FK user
        public int UserId { get; set; }
        //Nav user
        public User User { get; set; }
    }
}
