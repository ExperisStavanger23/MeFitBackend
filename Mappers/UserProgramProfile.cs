using AutoMapper;
using MeFitBackend.Data.DTO.UserProgram;
using MeFitBackend.Data.Entities;

namespace MeFitBackend.Mappers
{
    public class UserProgramProfile : Profile
    {
        public UserProgramProfile() 
        { 
            CreateMap<UserProgram, UserProgramDTO>()
                .ForMember(updto => updto.Program, opt => opt
                .MapFrom(up => up.Program)).ToString();

            CreateMap<UserProgramDTO, UserProgram>().ReverseMap();
        }
    }
}
