using AutoMapper;
using MeFitBackend.Data.DTO.UserExercise;
using MeFitBackend.Data.Entities;

namespace MeFitBackend.Mappers
{
    public class UserExerciseProfile : Profile
    {
        public UserExerciseProfile() 
        {
            CreateMap<UserExercise, UserExerciseDTO>().ReverseMap();
        }
    }
}
