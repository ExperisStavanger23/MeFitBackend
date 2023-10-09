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
                .ForMember(programDto => programDto.Workout, 
                    opt => opt.MapFrom(p => p.Workout
                        .Select(w => w.Id).ToList()));
            CreateMap<ProgramDTO, Program>();

            CreateMap<ProgramPutDTO, Program>().ReverseMap();
        }
    }
}
