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
                .ForMember(workoutDto => workoutDto.Exercises,
                opt => opt.MapFrom(w => w.Exercises
                    .Select(e => e.Id).ToList()));

            CreateMap<WorkoutDTO, Workout>();

            CreateMap<WorkoutPutDTO, Workout>().ReverseMap();
        }
    }
}
