namespace MeFitBackend.Data.DTO.Programs
{
    public class ProgramDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string RecomendedLvl { get; set; }
        public string Image {  get; set; }
        public int Duration { get; set; }
        public int[] Workout { get; set; }
    }
}
