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
                    .MapFrom(workout => workout.WorkoutExercises
                    .Select(workoutexercise => new WorkoutExercise
                    {
                        Id = workoutexercise.Id,
                        Reps = workoutexercise.Reps,
                        Sets = workoutexercise.Sets
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
