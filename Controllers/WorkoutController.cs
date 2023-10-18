using AutoMapper;
using MeFitBackend.Data.DTO.Exercises;
using MeFitBackend.Data.DTO.UserWorkout;
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
        public async Task<ActionResult<IEnumerable<WorkoutDTO>>> GetAllWorkouts()
        {
            var workouts = await _workoutService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<WorkoutDTO>>(workouts));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutDTO>> GetWorkoutById(int id)
        {
            try
            {
                var workout = await _workoutService.GetByIdAsync(id);
                var workoutDTO = _mapper.Map<WorkoutDTO>(workout);
                return Ok(workoutDTO);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkout(int id, WorkoutPutDTO workoutPutDTO)
        {
            if (id != workoutPutDTO.Id)
            {
                return BadRequest();
            }
            try
            {
                var workout = _mapper.Map<Workout>(workoutPutDTO);
                await _workoutService.UpdateAsync(workout);
                // var updatedWorkoutDTO = _mapper.Map<Workout>(workoutPutDTO);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<WorkoutPostDTO>> CreateWorkout(WorkoutPostDTO workoutPostDTO)
        {
            var newWorkout = await _workoutService.AddAsync(_mapper.Map<Workout>(workoutPostDTO));

            return CreatedAtAction("GetWorkoutById",
                new { id = newWorkout.Id },
                workoutPostDTO);
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

        [HttpGet("{id}/workoutexercises")]
        public async Task<ActionResult<IEnumerable<WorkoutExerciseDTO>>> GetAllWorkoutExercises(int id)
        {
            try
            {
                var workoutexercises = await _workoutService.GetWorkoutExercisesAsync(id);
                var weDTO = _mapper.Map<IEnumerable<WorkoutExerciseDTO>>(workoutexercises);
                return Ok(weDTO);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }

        [HttpPut("{id}/workoutexercises")]
        public async Task<ActionResult> PutWorkoutExercises(int id, [FromBody] int[] workoutexerciseIds)
        {
            try
            {
                await _workoutService.UpdateWorkoutExersiesAsync(id, workoutexerciseIds);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }
    }
}
