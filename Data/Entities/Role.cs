using System.ComponentModel.DataAnnotations.Schema;

namespace MeFitBackend.Data.Entities
{
    [Table(nameof(Role))]

    public class Role
    {
        public int Id { get; set; }
        public string RoleTitle { get; set; } = null!;
    }
}
