using AutoMapper;
using MeFitBackend.Data.DTO.Workouts;
using MeFitBackend.Data.Entities;

namespace MeFitBackend.Mappers
{
    public class WorkoutProfile : Profile
    {
        public WorkoutProfile() 
        {
            CreateMap<Workout, WorkoutDTO>()
                .ForMember(workoutDto => workoutDto.Exercises, opt => opt
                    .MapFrom(workout => workout.Exercises
                    .Select(exercise => new Exercise
                    {
                        Id = exercise.Id,
                        Name = exercise.Name,
                        Description = exercise.Description,
                        MuscleGroups = exercise.MuscleGroups,
                        Reps = exercise.Reps,
                        Sets = exercise.Sets,
                        Image = exercise.Image,
                        Video = exercise.Video,

                    })
                    .ToList()))
                .ForMember(workoutDto => workoutDto.UserWorkouts, opt => opt
                    .MapFrom(workout => workout.UserWorkouts
                    .Select(userWorkout => new UserWorkout
                    {
                        Id= userWorkout.Id,
                        UserId = userWorkout.Id,
                        WorkoutId = userWorkout.Id,
                    })
                    .ToList()));
            CreateMap<WorkoutDTO, Workout>();

            CreateMap<WorkoutPutDTO, Workout>().ReverseMap();
        }
    }
}
