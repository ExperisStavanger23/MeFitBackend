using MeFitBackend.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace MeFitBackend.Data.DTO.Workouts
{
    public class WorkoutDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public Level RecomendedLevel { get; set; }
        public string Image { get; set; }
        public int Duration { get; set; }
        public int[] Exercises { get; set; }
        public int[] UserWorkouts { get; set; }
    }
}
