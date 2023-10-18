using MeFitBackend.Data.Enums;

namespace MeFitBackend.Data.DTO.Workouts;

public class WorkoutGetAllDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Category { get; set; }
    public string RecommendedLevel { get; set; }
    public string? Image { get; set; } 
    public int Duration { get; set; }
}