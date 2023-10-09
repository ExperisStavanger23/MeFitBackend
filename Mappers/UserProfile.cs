using AutoMapper;
using MeFitBackend.Data.DTO.Users;
using MeFitBackend.Data.Entities;

namespace MeFitBackend.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        { 
            CreateMap<User, UserDTO>()
                .ForMember(udto => udto.Programs,
                opt => opt.MapFrom(u => u.Programs.Select(u => u.Id).ToList()))
                .ForMember(udto => udto.Workouts,
                opt => opt.MapFrom(u => u.Workouts.Select(u => u.Id).ToList()))
                .ForMember(udto => udto.Exercises,
                opt => opt.MapFrom(u => u.Exercises.Select(u => u.Id).ToList()));
            // Post
            CreateMap<UserPostDTO, User>().ReverseMap();

            // Put
            CreateMap<User, UserPutDTO>().ReverseMap();
        }
    }
}
