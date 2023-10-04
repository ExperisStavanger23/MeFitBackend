using MeFitBackend.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeFitBackend.Data.Entities
{
    [Table(nameof(Exercise))]

    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<UserExercise> UserExercises { get; set; } = new List<UserExercise>();
        public ICollection<MuscleGroup> MuscleGroups { get; set; } = new List<MuscleGroup>();
        public int Reps { get; set; }
        public int Sets { get; set; }
        public string Image {  get; set; }
        public string Video { get; set; }
    }
}
