using AutoMapper;
using MeFitBackend.Data.DTO.Exercises;
using MeFitBackend.Data.DTO.MuscleGroup;
using MeFitBackend.Data.DTO.Workouts;
using MeFitBackend.Data.Entities;
using System.Numerics;

namespace MeFitBackend.Mappers
{
    public class WorkoutProfile : Profile
    {
        public WorkoutProfile() 
        {
            CreateMap<Workout, WorkoutDTO>()
                .ForMember(workoutDto => workoutDto.WorkoutExercises, opt => opt
                    .MapFrom(workout => workout.WorkoutExercises
                    .Select(workoutexercise => new WorkoutExerciseDTO
                    {
                        WorkoutId = workoutexercise.WorkoutId,
                        ExerciseId = workoutexercise.ExerciseId,
                        Reps = workoutexercise.Reps,
                        Sets = workoutexercise.Sets,
                        Exercise = new ExerciseDTO
                        {
                            Id = workoutexercise.ExerciseId,
                            Name = workoutexercise.Exercise.Name,
                            Description = workoutexercise.Exercise.Description,
                            Image = workoutexercise.Exercise.Image,
                            Video = workoutexercise.Exercise.Video,
                            ExerciseMuscleGroups = workoutexercise.Exercise.ExerciseMuscleGroups
                            .Select( emg => new ExerciseMuscleGroupDTO
                            {
                                ExerciseId = emg.ExerciseId,
                                MuscleGroupId = emg.MuscleGroupId,
                                MuscleGroup = new MuscleGroupDTO
                                {
                                    Id = emg.MuscleGroup.Id,
                                    Name = emg.MuscleGroup.Name,
                                }
                            }).ToList()
                        }
                    }).ToList()))
                .ForMember(workoutDto => workoutDto.UserWorkouts, opt => opt
                    .MapFrom(workout => workout.UserWorkouts
                    .Select(userWorkout => new UserWorkout
                    {
                        Id= userWorkout.Id,
                        WorkoutId = userWorkout.Id,
                    })
                    .ToList()));

            // Helper mapper used to WorkoutExercise -> ExerciseDTO
           /* CreateMap<WorkoutExercise, ExerciseDTO>()
                .ForMember(eDto => eDto.Id, opt => opt.MapFrom(we => we.Exercise.Id))
                .ForMember(eDto => eDto.Name, opt => opt.MapFrom(we => we.Exercise.Name))
                .ForMember(eDto => eDto.Description, opt => opt.MapFrom(src => src.Exercise.Description))
                .ForMember(eDto => eDto.Image, opt => opt.MapFrom(we => we.Exercise.Image))
                .ForMember(eDto => eDto.Video, opt => opt.MapFrom(we => we.Exercise.Video))
                .ForMember(eDto => eDto.ExerciseMuscleGroups, opt => opt.MapFrom(we => we.Exercise.ExerciseMuscleGroups
                .Select(emg => new ExerciseMuscleGroupDTO
                {
                    ExerciseId = emg.ExerciseId,
                    MuscleGroupId = emg.MuscleGroupId,
                    MuscleGroup = new MuscleGroupDTO
                    {
                        Id = emg.MuscleGroup.Id,
                        Name = emg.MuscleGroup.Name
                    }
                })));*/
            CreateMap<WorkoutExerciseDTO, ExerciseDTO>()
                .ForMember(eDto => eDto.Id, opt => opt.MapFrom(we => we.Exercise.Id))
                .ForMember(eDto => eDto.Name, opt => opt.MapFrom(we => we.Exercise.Name))
                .ForMember(eDto => eDto.Description, opt => opt.MapFrom(src => src.Exercise.Description))
                .ForMember(eDto => eDto.Image, opt => opt.MapFrom(we => we.Exercise.Image))
                .ForMember(eDto => eDto.Video, opt => opt.MapFrom(we => we.Exercise.Video))
                .ForMember(eDto => eDto.ExerciseMuscleGroups, opt => opt.MapFrom(we => we.Exercise.ExerciseMuscleGroups
                    .Select(emg => new ExerciseMuscleGroupDTO
                    {
                        ExerciseId = emg.ExerciseId,
                        MuscleGroupId = emg.MuscleGroupId,
                        MuscleGroup = new MuscleGroupDTO
                        {
                            Id = emg.MuscleGroup.Id,
                            Name = emg.MuscleGroup.Name
                        }
                    })));

            //CreateMap<Workout, WorkoutDTO>()
            //.ForMember(workoutDto => workoutDto.Exercises, opt => opt.MapFrom(workout => workout.WorkoutExercises));
            CreateMap< Workout, WorkoutInProgramDTO>();
            
            CreateMap<WorkoutPutDTO, Workout>().ReverseMap();

            CreateMap<Workout,WorkoutGetAllDTO>();
            CreateMap<Workout, WorkoutGetByIdDTO>();
                

            CreateMap<WorkoutPostDTO, Workout>()
             .ForMember(dest => dest.WorkoutExercises, opt => opt.MapFrom(src => src.WorkoutExercises
                 .Select(we => new WorkoutExerciseDTO
                 {
                     ExerciseId = we.ExerciseId,
                     Sets = we.Sets,
                     Reps = we.Reps,

                 }).ToList())
             );

            CreateMap <WorkoutExerciseDTO, WorkoutExercise>();


            CreateMap<WorkoutExercise, WorkoutExerciseDTO>();


            CreateMap<WorkoutExercisePostDTO, WorkoutExercise>()
                .ForMember(we => we.Sets, opt => opt.MapFrom(wePostDto => wePostDto.Sets))
                .ForMember(we => we.Reps, opt => opt.MapFrom(wePostDto => wePostDto.Reps));

        }
    }
}
