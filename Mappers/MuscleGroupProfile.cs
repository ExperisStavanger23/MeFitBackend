using AutoMapper;
using MeFitBackend.Data.DTO.Exercises;
using MeFitBackend.Data.DTO.MuscleGroup;
using MeFitBackend.Data.Entities;

namespace MeFitBackend.Mappers
{
    public class MuscleGroupProfile: Profile
    {
        public MuscleGroupProfile()
        {
            CreateMap<MuscleGroup, ExerciseMuscleGroupDTO>().ReverseMap();
            CreateMap<MuscleGroup, MuscleGroupDTO>().ReverseMap();
        }
    }
}
