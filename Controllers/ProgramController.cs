using AutoMapper;
using MeFitBackend.Data.DTO.Programs;
using MeFitBackend.Data.DTO.Workouts;
using MeFitBackend.Data.Entities;
using MeFitBackend.Data.Exceptions;
using MeFitBackend.Services.Programs;
using Microsoft.AspNetCore.Mvc;

namespace MeFitBackend.Controllers
{
    [Route("api/v1/Program")]
    [ApiController]
    [Produces("application/Json")]
    [Consumes("application/Json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ProgramController : ControllerBase
    {
        private readonly IProgramService _programService;
        private readonly IMapper _mapper;

        public ProgramController(IProgramService programService, IMapper mapper)
        {
            _programService = programService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a list of programs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProgramDTO>>> GetPrograms()
        {
            var programs = await _programService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ProgramDTO>>(programs));
        }

        /// <summary>
        /// Retrieve program by their unique identifier
        /// </summary>
        /// <param name="id"> Id of the program to retrieve</param>
        /// <response code="200">Ok - Success <br/></response>
        /// <response code="404">Not Found - The exercise with the given ID was not found.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProgramGetByIdDTO>> GetProgramWithWorkouts(int id)
        {
            var program = await _programService.GetByIdAsync(id);
            if (program == null)
            {
                return NotFound(); // Handle the case where the program is not found
            }

            var pDto = _mapper.Map<ProgramGetByIdDTO>(program);
            return Ok(pDto);
        }

        /// <summary>
        /// Updates an existing programs information
        /// </summary>
        /// <param name="id">The id of the program to update</param>
        /// <param name="program">The new program data</param>
        /// <returns></returns>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <response code="200">Ok - Success <br/></response>
        /// <response code="404">Not Found - The program with the given ID was not found.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProgram(int id, ProgramPutDTO program)
        {
            if (id != program.Id)
            {
                throw new EntityNotFoundException("program", id);
            }
            try
            {
                await _programService.UpdateAsync(_mapper.Map<Program>(program));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return Ok();
        }

        /// <summary>
        /// Creates a new program
        /// </summary>
        /// <param name="programDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ProgramPostDTO>> PostProgram(ProgramPostDTO programDto)
        {
            var program = _mapper.Map<Program>(programDto);

            // Pass the WorkoutIds from the DTO to the service
            var newProgram = await _programService.AddAsync(program, programDto.WorkoutIds);

            return CreatedAtAction("GetProgramWithWorkouts",
                new { id = newProgram.Id },
                _mapper.Map<ProgramDTO>(newProgram));
        }

        /// <summary>
        /// Deletes a program by their unique identifier
        /// </summary>
        /// <param name="id">The id of the program to delete</param>
        /// <returns></returns>
        /// <response code="204">No Content - Success <br/></response> 
        /// <response code="404">Not Found - The program with the given ID was not found.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _programService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Gets all workouts associated with a specific program identified by 'id'.
        /// </summary>
        /// <param name="id">The unique identifier of the program to get from.</param>
        /// <exception cref="EntityNotFoundException">Thrown when the exercise with the provided 'id' is not found.</exception>
        /// <response code="200">Ok - Success <br/></response>
        /// <response code="404">Not Found - The exercise with the given ID was not found.</response>
        [HttpGet("{id}/workouts")]
        public async Task<ActionResult<IEnumerable<WorkoutDTO>>> GetAllWorkouts(int id)
        {
            try
            {
                var workouts = await _programService.GetWorkoutsAsync(id);
                var workoutDTO = _mapper.Map<IEnumerable<WorkoutDTO>>(workouts);
                return Ok(workoutDTO);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }

        /// <summary>
        /// Updates workouts in program
        /// </summary>
        /// <param name="id">The id of the program to update workouts in</param>
        /// <param name="workoutIds">The ids of the workouts to insert into the program</param>
        /// <exception cref="EntityNotFoundException">Thrown when the resource with the provided 'id' is not found.</exception>
        /// <response code="200">Ok - Success <br/></response>
        /// <response code="404">Not Found - The exercise with the given ID was not found.</response>
        [HttpPut("{id}/workouts")]
        public async Task<ActionResult> PutProgramWorkouts(int id, [FromBody] int[] workoutIds)
        {
            try
            {
                await _programService.UpdateWorkoutsAsync(id, workoutIds);
                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }
    }
}