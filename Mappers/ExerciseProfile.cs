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
            CreateMap<ExercisePostDTO, Exercise>()
                .ForMember(e => e.ExerciseMuscleGroups, opt => opt
                .MapFrom(ePostDTO => ePostDTO.MuscleGroups
                .Select(mgDTO => new ExerciseMuscleGroup
                {
                    MuscleGroupId = mgDTO.Id
                })));

            CreateMap<ExercisePutDTO, Exercise>().ReverseMap();
        }
    }
}
