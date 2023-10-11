using AutoMapper;
using MeFitBackend.Data.DTO.Exercises;
using MeFitBackend.Data.DTO.MuscleGroup;
using MeFitBackend.Data.DTO.UserExercise;
using MeFitBackend.Data.Entities;

namespace MeFitBackend.Mappers
{
    public class ExerciseProfile : Profile
    {
        public ExerciseProfile() 
        {
            CreateMap<Exercise, ExerciseDTO>()
            .ForMember(exerciseDto => exerciseDto.MuscleGroupIds,opt => opt
            .MapFrom(exercise => exercise.ExerciseMuscleGroups
            .Select(exerciseMuscleGroup => exerciseMuscleGroup.MuscleGroupId)
            .ToList()));

            CreateMap<ExercisePostDTO, Exercise>().ReverseMap();
            CreateMap<ExercisePutDTO, Exercise>().ReverseMap();
        }
    }
}
