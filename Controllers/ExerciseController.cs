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
    [ApiConventionType(typeof(DefaultApiConventions))]
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
        /// <exception cref="EntityNotFoundException">Thrown when the exercise with the provided 'id' is not found.</exception>
        /// <response code="200">Ok - Success <br/></response>
        /// <response code="404">Not Found - The exercise with the given ID was not found.</response>
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
        /// <exception cref="EntityNotFoundException">Thrown when the exercise with the provided 'id' is not found.</exception>
        /// <response code="200">Ok - Success <br/></response>
        /// <response code="404">Not Found - The exercise with the given ID was not found.</response>
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
            return CreatedAtAction(nameof(GetExerciseById), new { id = createdExercise.Id }, exercisePostDTO);
        }
    
        /// <summary>
        /// Deletes an exercise by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the exercise to delete.</param>
        /// <exception cref="EntityNotFoundException">Thrown when the exercise with the provided 'id' is not found.</exception>
        /// <response code="204">No Content - Success <br/></response> 
        /// <response code="404">Not Found - The exercise with the given ID was not found.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            try
            {
                await _exerciseService.DeleteByIdAsync(id);
                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Gets all muscle groups associated with a specific exercise identified by 'id' using a HTTP GET request.
        /// </summary>
        /// <param name="id">The unique identifier of the exercise to get from.</param>
        /// <returns>
        ///   - HTTP 200 (OK) if the get is successful. With a list of all muscle groups associated with the exercise.
        ///   - HTTP 404 (Not Found) with an error message if the exercise with the provided 'id' is not found.
        /// </returns>
        /// <exception cref="EntityNotFoundException">Thrown when the exercise with the provided 'id' is not found.</exception>
        /// <response code="200">Ok - Success <br/></response>
        /// <response code="404">Not Found - The exercise with the given ID was not found.</response>
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
        /// Updates the muscle groups associated with a specific exercise identified by 'id' using a HTTP PUT request.
        /// </summary>
        /// <param name="id">The unique identifier of the exercise to update.</param>
        /// <param name="musclegroupIds">An array of integers representing the updated muscle group associations.</param>
        /// <exception cref="EntityNotFoundException">Thrown when the resource with the provided 'id' is not found.</exception>
        /// <response code="200">Ok - Success <br/></response>
        /// <response code="404">Not Found - The exercise with the given ID was not found.</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("{id}/musclegroups")]
        public async Task<ActionResult> PutMuscleGroups(int id, [FromBody] int[] musclegroupIds)
        {
            try
            {
                await _exerciseService.UpdateMuscleGroupsAsync(id, musclegroupIds);
                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }
    }
}
