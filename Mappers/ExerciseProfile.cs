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
                .ForMember(eDto => eDto.ExerciseMuscleGroups, opt => opt
                .MapFrom(e => e.ExerciseMuscleGroups
                .Select(emg => new ExerciseMuscleGroupDTO
                {
                    ExerciseId = emg.ExerciseId,
                    MuscleGroupId = emg.MuscleGroupId,
                    MuscleGroup = new MuscleGroupDTO
                    {
                        Id = emg.MuscleGroupId,
                        Name = emg.MuscleGroup.Name,
                    }
                })));
            CreateMap<ExercisePostDTO, Exercise>().ReverseMap();
            CreateMap<ExercisePutDTO, Exercise>().ReverseMap();
        }
    }
}
