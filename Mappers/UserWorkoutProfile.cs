using AutoMapper;
using MeFitBackend.Data.DTO.UserWorkout;
using MeFitBackend.Data.Entities;

namespace MeFitBackend.Mappers
{
    public class UserWorkoutProfile : Profile
    {
        public UserWorkoutProfile() 
        {
            CreateMap<UserWorkout, UserWorkoutDTO>()
                .ForMember(uwdto => uwdto.Workout, opt => opt
                .MapFrom(uw => uw.Workout)).ToString();
        }
    }
}
