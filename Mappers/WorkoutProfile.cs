using AutoMapper;
using MeFitBackend.Data.DTO.Workouts;
using MeFitBackend.Data.Entities;

namespace MeFitBackend.Mappers
{
    public class WorkoutProfile : Profile
    {
        public WorkoutProfile() 
        {
            CreateMap<Workout, WorkoutDTO>()
                .ForMember(wdto => wdto.Exercises, opt => opt
                    .MapFrom(w => w.Exercises.Select(s => s.Id).ToList()))
                .ForMember(wdto => wdto.UserWorkouts, opt => opt
                    .MapFrom(w => w.UserWorkouts.Select(s => s.Id).ToList()));
            CreateMap<Workout, WorkoutPostDTO>();
            CreateMap<Workout, WorkoutPutDTO>();
        }
    }
}
