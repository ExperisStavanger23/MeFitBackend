using AutoMapper;
using MeFitBackend.Data.DTO.Exercises;
using MeFitBackend.Data.DTO.Programs;
using MeFitBackend.Data.Entities;
using MeFitBackend.Data.Exceptions;
using MeFitBackend.Services.Exercises;
using Microsoft.AspNetCore.Mvc;

namespace MeFitBackend.Controllers
{
    [Route("api/v1/Exercise")]
    [ApiController]
    [Produces("application/Json")]
    [Consumes("application/Json")]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseService _exerciseService;
        private readonly IMapper _mapper;

        public ExerciseController(IExerciseService exerciseService, IMapper mapper)
        {
            _exerciseService = exerciseService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseDTO>>> GetExercises()
        {
            var exercises = await _exerciseService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ExerciseDTO>>(exercises));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExerciseDTO>> GetExercise(int id)
        {
            var exercise = await _exerciseService.GetByIdAsync(id);
            if (exercise == null)
            {
                return NotFound(new EntityNotFoundException("exercise", id));
            }

            return Ok(_mapper.Map<ExerciseDTO>(exercise));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutExercise(int id, ExercisePutDTO exercise)
        {
            if (id != exercise.Id)
            {
                throw new EntityNotFoundException("exercise", id);
            }

            try
            {
                await _exerciseService.UpdateAsync(_mapper.Map<Exercise>(exercise));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ExerciseDTO>> PostExercise(ExercisePostDTO exercise)
        {
            var newExercise = await _exerciseService.AddAsync(_mapper.Map<Exercise>(exercise));

            return CreatedAtAction("GetExercise",
                new { id = newExercise.Id },
                _mapper.Map<ExerciseDTO>(newExercise));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            try
            {
                await _exerciseService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
