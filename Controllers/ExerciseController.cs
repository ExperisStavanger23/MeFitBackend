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
        public async Task<ActionResult<IEnumerable<ExerciseDTO>>> GetAllExercises()
        {
            var exercises = await _exerciseService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ExerciseDTO>>(exercises));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExerciseDTO>> GetExerciseById(int id)
        {
            try
            {
                var exercise = await _exerciseService.GetByIdAsync(id);
                var exerciseDTO = _mapper.Map<ExerciseDTO>(exercise);
                return Ok(exerciseDTO);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExercise(int id, ExercisePutDTO exercisePutDTO)
        {
            if (id != exercisePutDTO.Id)
            {
                return BadRequest();
            }
            try
            {
                var exercise = _mapper.Map<Exercise>(exercisePutDTO);
                var updatedExercise = await _exerciseService.UpdateAsync(exercise);
                var exerciseDTO = _mapper.Map<ExerciseDTO>(updatedExercise);

                return Ok(exerciseDTO);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ExerciseDTO>> CreateExercise(ExercisePostDTO exercisePostDTO)
        {
            var exercise = _mapper.Map<Exercise>(exercisePostDTO);
            var createdExercise = await _exerciseService.AddAsync(exercise);
            var exerciseDTO = _mapper.Map<ExerciseDTO>(createdExercise);
            return CreatedAtAction(nameof(GetExerciseById), new { id = exerciseDTO.Id }, exerciseDTO);
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
