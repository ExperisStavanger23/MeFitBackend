﻿using MeFitBackend.Data.DTO.Exercises;
using MeFitBackend.Data.Enums;

namespace MeFitBackend.Data.DTO.Workouts;

public class WorkoutInProgramDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public WorkoutCategory Category { get; set; }
    public Level RecommendedLevel { get; set; }
    public string Image { get; set; }
    public int Duration { get; set; }
    public ICollection<WorkoutExerciseDTO> WorkoutExercises { get; set; }
}