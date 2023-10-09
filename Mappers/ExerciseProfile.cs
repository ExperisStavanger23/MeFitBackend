using AutoMapper;
using MeFitBackend.Data.DTO.Exercises;
using MeFitBackend.Data.DTO.MuscleGroup;
using MeFitBackend.Data.DTO.UserExercise;
using MeFitBackend.Data.Entities;

namespace MeFitBackend.Mappers
{
    public class ExerciseProfile : Profile
    {
        public ExerciseProfile() 
        {
            CreateMap<Exercise, ExerciseDTO>()
            .ForMember(exerciseDto => exerciseDto.UserExercises,opt => opt
            .MapFrom(exercise => exercise.UserExercises
            .Select(userExercise => new UserExercise
            {
                    Id = userExercise.Id,
                    UserId = userExercise.UserId,
                    ExerciseId = userExercise.Id,
                })
            .ToList()))

            .ForMember(exerciseDto => exerciseDto.MuscleGroups,opt => opt
            .MapFrom(exercise => exercise.MuscleGroups
            .Select(musclegroup => new MuscleGroupDTO
                {
                    Id = musclegroup.Id,
                    Name = musclegroup.Name,
                })
            .ToList()));
            CreateMap<ExerciseDTO, Exercise>();

            CreateMap<ExercisePostDTO, Exercise>().ReverseMap();
            CreateMap<ExercisePutDTO, Exercise>().ReverseMap();
        }
    }
}
