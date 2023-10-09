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
                .ForMember(programDto => programDto.Workout,opt => opt
                    .MapFrom(program => program.Workout
                    .Select(workout => new Workout
                    {
                        Id = workout.Id,
                        Name = workout.Name,
                        Description = workout.Description,
                        Category = workout.Category,
                        RecommendedLevel = workout.RecommendedLevel,
                        Duration = workout.Duration,
                        WorkoutExercises = workout.WorkoutExercises,
                    })
                    .ToList()));
            CreateMap<ProgramDTO, Program>();

            CreateMap<ProgramPutDTO, Program>().ReverseMap();
        }
    }
}
