using MeFitBackend.Data.DTO.MuscleGroup;

namespace MeFitBackend.Data.DTO.Exercises
{
    public class ExercisePutDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
        //public ICollection<MuscleGroupDTO> MuscleGroups { get; set; }
    }
}
