﻿using AutoMapper;
using MeFitBackend.Data.DTO.UserExercise;
using MeFitBackend.Data.DTO.UserProgram;
using MeFitBackend.Data.DTO.Users;
using MeFitBackend.Data.DTO.UserWorkout;
using MeFitBackend.Data.DTO.Workouts;
using MeFitBackend.Data.Entities;

namespace MeFitBackend.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        { 
            CreateMap<User, UserDTO>()
                .ForMember(udto => udto.UserExercises, opt => opt
                    .MapFrom(u => u.UserExercises
                    .Select(s => new UserExercise
                    {
                        Id = s.Id,
                        ExerciseId = s.ExerciseId,
                        Exercise = new Exercise
                        {
                            Id = s.Exercise.Id,
                            Name = s.Exercise.Name,
                            Description = s.Exercise.Description,
                        }
                    }).ToList()))
                .ForMember(udto => udto.UserWorkouts, opt => opt
                    .MapFrom(u => u.UserWorkouts
                    .Select(u => new UserWorkoutDTO()
                    {
                        Id = u.Id,
                        UserId = u.UserId, 
                        WorkoutId = u.WorkoutId,
                        DoneDate = u.DoneDate,
                        Workout = new Workout
                        {
                           Id = u.Workout.Id,
                           Name = u.Workout.Name,
                           Description = u.Workout.Description,
                        }
                      
                    }).ToList()))
                .ForMember(udto => udto.UserPrograms,  opt => opt
                    .MapFrom(udto => udto.UserPrograms
                    .Select(s => new UserProgramDTO()
                    {
                        Id= s.Id,
                        ProgramId = s.ProgramId,
                        Program = new Program
                        {
                            Id = s.Program.Id,
                            Name = s.Program.Name,
                            Description = s.Program.Description,
                        }
                    }).ToList()))
                .ForMember(udto => udto.Role, opt => opt
                    .MapFrom(u => u.Roles.Select(s => s.Id).ToList())); 
                

            // Post
            CreateMap<UserPostDTO, User>().ReverseMap();

            // Put
            CreateMap<User, UserPutDTO>().ReverseMap();

            CreateMap<UserExercise, UserExerciseDTO>();

            CreateMap<UserWorkout, UserWorkoutDTO>();

            CreateMap<UserProgram, UserProgramDTO>();
        }
    }
}
