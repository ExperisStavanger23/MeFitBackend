﻿using MeFitBackend.Data.Enums;

namespace MeFitBackend.Data.DTO.Workouts
{
    public class WorkoutPutDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public Level RecommendedLevel { get; set; }
        public string Image { get; set; }
        public int Duration { get; set; }
    }
}
