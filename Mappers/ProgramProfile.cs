using AutoMapper;
using MeFitBackend.Data.DTO.Programs;
using MeFitBackend.Data.Entities;

namespace MeFitBackend.Mappers
{
    public class ProgramProfile : Profile
    {
        public ProgramProfile() 
        {
            CreateMap<Program, ProgramDTO>()
                .ForMember(pdto => pdto.Workout,opt => opt
                    .MapFrom(u => u.Workout.Select(s => s.Id).ToList()));
            CreateMap<ProgramPostDTO, Program>();
            CreateMap<ProgramPutDTO, Program>();
        }
    }
}
