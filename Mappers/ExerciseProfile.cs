using AutoMapper;
using MeFitBackend.Data.DTO.Exercises;
using MeFitBackend.Data.Entities;

namespace MeFitBackend.Mappers
{
    public class ExerciseProfile : Profile
    {
        public ExerciseProfile() 
        {
            CreateMap<Exercise, ExerciseDTO>()
                .ForMember(edto => edto.UserExercises, opt => opt
                    .MapFrom(e => e.UserExercises.Select(s => s.Id).ToList()));
            CreateMap<Exercise, ExercisePostDTO>();
            CreateMap<Exercise, ExercisePutDTO>();
        }
    }
}
