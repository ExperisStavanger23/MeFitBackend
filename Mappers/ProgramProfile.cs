using AutoMapper;
using MeFitBackend.Data.DTO.Exercises;
using MeFitBackend.Data.DTO.MuscleGroup;
using MeFitBackend.Data.DTO.Programs;
using MeFitBackend.Data.DTO.Workouts;
using MeFitBackend.Data.Entities;

namespace MeFitBackend.Mappers
{
    public class ProgramProfile : Profile
    {
        public ProgramProfile() 
        {
            CreateMap<Program, ProgramDTO>()
                .ForMember(pDto => pDto.Workouts, opt => opt
                .MapFrom(p => p.Workout
                .Select(w => new WorkoutDTO
                {
                    Id = w.Id,
                    Name = w.Name,
                    Description = w.Description,
                    Category = w.Category,
                    RecommendedLevel = w.RecommendedLevel,
                    Image = w.Image,
                    Duration = w.Duration,
                }).ToList()));

            CreateMap<ProgramPutDTO, Program>();
                
            CreateMap<ProgramPostDTO, Program>();
        }
    }
}
