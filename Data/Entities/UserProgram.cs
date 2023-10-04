using System.ComponentModel.DataAnnotations.Schema;

namespace MeFitBackend.Data.Entities
{
    [Table(nameof(UserProgram))]

    public class UserProgram
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ProgramId { get; set; }
        public Program Program { get; set; }
    }
}
