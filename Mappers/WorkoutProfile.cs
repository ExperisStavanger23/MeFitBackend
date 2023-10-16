using AutoMapper;
using MeFitBackend.Data.DTO.Exercises;
using MeFitBackend.Data.DTO.MuscleGroup;
using MeFitBackend.Data.DTO.WorkoutExercise;
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
                        WorkoutId = userWorkout.Id,
                    })
                    .ToList()));
            CreateMap<WorkoutDTO, Workout>();
            CreateMap< Workout, WorkoutInProgramDTO>();
            
            CreateMap<WorkoutPutDTO, Workout>().ReverseMap();

            CreateMap<Workout,WorkoutGetAllDTO>();
            CreateMap<Workout, WorkoutGetByIdDTO>()
                .ForMember(
                    wdto => wdto.Exercises,
                    options => options.MapFrom(workout => workout.WorkoutExercises
                        .Select(workoutExercise => new ExerciseDTO
                        {
                            Id = workoutExercise.Exercise != null ? workoutExercise.Exercise.Id : 0, 
                            Name = workoutExercise.Exercise != null ? workoutExercise.Exercise.Name : "Unknown",
                            //Sets = workoutExercise.Sets,
                            //Reps = workoutExercise.Reps,
                        }).ToList())
                );

            //CreateMap<ExerciseMuscleGroup, Data.DTO.MuscleGroup.ExerciseMuscleGroupDTO>();
            CreateMap<WorkoutPostDTO, Workout>()
                .ForMember(dest => dest.WorkoutExercises, opt => opt.MapFrom(src => src.WorkoutExercises))
                .ReverseMap();
            CreateMap<WorkoutExerciseDTO, WorkoutExercise>()
                .ReverseMap();
        }
    }
}
