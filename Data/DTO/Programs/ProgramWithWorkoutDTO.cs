using MeFitBackend.Data.DTO.Workouts;
using MeFitBackend.Data.Enums;

namespace MeFitBackend.Data.DTO.Programs;

public class ProgramWithWorkoutDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ProgramCategory Category { get; set; }
    public Level RecommendedLevel { get; set; }
    public string Image { get; set; }
    public int Duration { get; set; }

    public List<WorkoutDTO> Workouts { get; set; }
}