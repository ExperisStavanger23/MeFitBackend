using MeFitBackend.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeFitBackend.Data.Entities
{
    [Table(nameof(UserGoal))]
    public class UserGoal
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
