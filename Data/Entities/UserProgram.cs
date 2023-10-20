using System.ComponentModel.DataAnnotations.Schema;

namespace MeFitBackend.Data.Entities
{
    [Table(nameof(UserProgram))]

    public class UserProgram
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int ProgramId { get; set; }
        public Program? Program { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
