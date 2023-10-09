using AutoMapper;
using MeFitBackend.Data.DTO.WorkoutExercise;
using MeFitBackend.Data.Entities;

namespace MeFitBackend.Mappers
{
    public class WorkoutExercisePeofile : Profile
    {
        public WorkoutExercisePeofile()
        {
            CreateMap<WorkoutExercise, WorkoutExerciseDTO>().ReverseMap();
        }
    }
}
