using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeFitBackend.Data.Entities
{
    [Table(nameof(Created))]
    public class Created
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public Enums.EntityType EntityType { get; set; }
        public int CreatorId { get; set; }
        public User? Creator { get; set; }
    }
}
