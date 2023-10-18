using AutoMapper;
using MeFitBackend.Data.DTO.Exercises;
using MeFitBackend.Data.DTO.MuscleGroup;
using MeFitBackend.Data.DTO.Programs;
using MeFitBackend.Data.DTO.Workouts;
using MeFitBackend.Data.Entities;
using System.Configuration;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Security.Policy;

namespace MeFitBackend.Mappers
{
    public class ProgramProfile : Profile
    {
        public ProgramProfile() 
        {
            CreateMap<Program, ProgramDTO>()
                .ForMember(pDto => pDto.Workouts, opt => opt
                .MapFrom(p => p.Workouts
                .Select(w => new WorkoutDTO
                {
                    Id = w.Id,
                    Name = w.Name,
                    Description = w.Description,
                    Category = w.Category,
                    RecommendedLevel = w.RecommendedLevel,
                    Image = w.Image,
                    Duration = w.Duration,
                    WorkoutExercises = w.WorkoutExercises
                    .Select(we => new WorkoutExerciseDTO
                    {
                        ExerciseId = we.ExerciseId,
                        WorkoutId = we.WorkoutId,
                        Reps = we.Reps,
                        Sets = we.Sets,
                        Exercise = new ExerciseDTO
                        {
                            Id = we.Exercise.Id,
                            Name = we.Exercise.Name,
                            Description = we.Exercise.Description,
                            Image = we.Exercise.Image,
                            Video = we.Exercise.Video,
                            ExerciseMuscleGroups = we.Exercise.ExerciseMuscleGroups
                            .Select(emg => new ExerciseMuscleGroupDTO
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
                    }).ToList()
                }).ToList()));

            CreateMap<ProgramPutDTO, Program>();
                
            CreateMap<ProgramPostDTO, Program>();

            // Helper mappings
            CreateMap<WorkoutInProgramDTO, WorkoutDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.RecommendedLevel, opt => opt.MapFrom(src => src.RecommendedLevel))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(dest => dest.WorkoutExercises, opt => opt.MapFrom(src => src.WorkoutExercises
                .Select(we => we.Exercise)))
                .ForMember(dest => dest.UserWorkouts, opt => opt.Ignore());
        }
    }
}
