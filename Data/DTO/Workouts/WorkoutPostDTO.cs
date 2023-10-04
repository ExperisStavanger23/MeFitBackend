﻿namespace MeFitBackend.Data.DTO.Workouts
{
    public class WorkoutPostDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public Level RecomendedLevel { get; set; }
        public string Image { get; set; }
        public int Duration { get; set; }
    }
}
