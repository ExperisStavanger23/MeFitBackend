using MeFitBackend.Data.Entities;

namespace MeFitBackend.Data.DTO.UserProgram
{
    public class UserProgramDTO
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int ProgramId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Program? Program { get; set; }
    }
}
