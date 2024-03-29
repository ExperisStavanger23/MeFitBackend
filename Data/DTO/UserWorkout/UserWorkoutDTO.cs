﻿using MeFitBackend.Data.Entities;

namespace MeFitBackend.Data.DTO.UserWorkout
{
    public class UserWorkoutDTO
    {
        public int Id { get; set; } 
        public string UserId { get ; set; }
        public int WorkoutId { get; set; }
        public DateTime? DoneDate { get; set; }
        public Workout Workout {  get; set; }
    }
} 
