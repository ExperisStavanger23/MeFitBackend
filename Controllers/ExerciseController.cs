using AutoMapper;
using MeFitBackend.Data.DTO.Exercises;
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

        /// <summary>
        /// Retrieves a list of exercises
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseDTO>>> GetAllExercises()
        {
            var exercises = await _exerciseService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ExerciseDTO>>(exercises));
        }

        /// <summary>
        /// Retrieves exercise by their unique identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Updates an existing exercises information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="exercisePutDTO"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Creates a new exercise
        /// </summary>
        /// <param name="exercisePostDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ExerciseDTO>> CreateExercise([FromBody] ExercisePostDTO exercisePostDTO)
        {
            var exercise = _mapper.Map<Exercise>(exercisePostDTO);
            var createdExercise = await _exerciseService.AddAsync(exercise);
            //var exerciseDTO = _mapper.Map<ExerciseDTO>(createdExercise);
            //return CreatedAtAction(nameof(GetExerciseById), new { id = exerciseDTO.Id }, exerciseDTO);
            return Ok();
        }
    
        /// <summary>
        /// Deletes an exercise by their unique identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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


        [HttpGet("{id}/musclegroups")]
        public async Task<ActionResult<IEnumerable<ExerciseMuscleGroupDTO>>> GetAllMusclegroups(int id)
        {
            try
            {
                var musclegroups = await _exerciseService.GetMuscleGroupsAsync(id);
                var mgDTO = _mapper.Map<IEnumerable<ExerciseMuscleGroupDTO>>(musclegroups);
                return Ok(mgDTO);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }

        /// <summary>
        /// Updates musclegroups in an exercise
        /// </summary>
        /// <param name="id"></param>
        /// <param name="musclegroupIds"></param>
        /// <returns></returns>
        [HttpPut("{id}/musclegroups")]
        public async Task<ActionResult> PutMuscleGroups(int id, [FromBody] int[] musclegroupIds)
        {
            try
            {
                await _exerciseService.UpdateMuscleGroupsAsync(id, musclegroupIds);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }
    }
}
