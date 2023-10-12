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
                .ForMember(dest => dest.Workout, opt => opt.MapFrom(src => src.Workout.Select(w => w.Id).ToArray()));
            CreateMap<ProgramDTO, Program>();

            CreateMap<ProgramPutDTO, Program>().ReverseMap();
            CreateMap<ProgramPostDTO, Program>();
        }
    }
}
