using AutoMapper;
using MeFitBackend.Data.DTO.Workouts;
using MeFitBackend.Data.Entities;
using MeFitBackend.Data.Exceptions;
using MeFitBackend.Services.Workouts;
using Microsoft.AspNetCore.Mvc;

namespace MeFitBackend.Controllers
{
    [Route("api/v1/Workout")]
    [ApiController]
    [Produces("application/Json")]
    [Consumes("application/Json")]
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;
        private readonly IMapper _mapper;

        public WorkoutController(IWorkoutService workoutService, IMapper mapper)
        {
            _workoutService = workoutService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutDTO>>> GetWorkouts()
        {
            var workouts = await _workoutService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<WorkoutDTO>>(workouts));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutDTO>> GetWorkout(int id)
        {
            var workout = await _workoutService.GetByIdAsync(id);
            if (workout == null)
            {
                return NotFound(new EntityNotFoundException("workout", id));
            }

            return Ok(_mapper.Map<WorkoutDTO>(workout));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkout(int id, WorkoutPutDTO workout)
        {
            if(id != workout.Id)
            {
                throw new EntityNotFoundException("workout", id);
            }

            try
            {
                await _workoutService.UpdateAsync(_mapper.Map<Workout>(workout));
            }
            catch (EntityNotFoundException ex) 
            {
                return NotFound(ex.Message);
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<WorkoutDTO>> PostWorkout(WorkoutPostDTO user)
        {
            var newWorkout = await _workoutService.AddAsync(_mapper.Map<Workout>(user));

            return CreatedAtAction("GetWorkout",
                new { id = newWorkout.Id },
                _mapper.Map<WorkoutDTO>(newWorkout));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkout(int id)
        {
            try
            {
                await _workoutService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
