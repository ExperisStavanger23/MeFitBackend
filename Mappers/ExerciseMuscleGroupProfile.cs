using AutoMapper;
using MeFitBackend.Data.DTO.Exercises;
using MeFitBackend.Data.Entities;

namespace MeFitBackend.Mappers
{
    public class ExerciseMuscleGroupProfile : Profile
    {
        public ExerciseMuscleGroupProfile() 
        {
            CreateMap<ExerciseMuscleGroup, ExerciseMuscleGroupDTO>()
                .ForMember(emgDto => emgDto.MuscleGroup, opt => opt
                .MapFrom(emg => emg.MuscleGroup));
        }
    }
}
