using AutoMapper;
using MeFitBackend.Data.DTO.MuscleGroup;
using MeFitBackend.Data.Entities;

namespace MeFitBackend.Mappers
{
    public class MuscleGroupProfile: Profile
    {
        public MuscleGroupProfile()
        {
            CreateMap<MuscleGroup, MuscleGroupDTO>().ReverseMap();
        }
    }
}
