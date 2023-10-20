namespace MeFitBackend.Data.DTO.Programs
{
    public class ProgramPutDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? RecommendedLevel { get; set; }
        public string? Image { get; set; }
        public int? Duration { get; set; }
    }
}
