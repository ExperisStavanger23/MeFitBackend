namespace MeFitBackend.Data.DTO.Exercises
{
    public class ExercisePostDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
        public int[] MuscleGroupIds { get; set; }
    }
}
