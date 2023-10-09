using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MeFitBackend.Data.DTO.Users;
using MeFitBackend.Data.Entities;
using MeFitBackend.Data.Exceptions;
using MeFitBackend.Services.Users;
using MeFitBackend.Data.DTO.UserExercise;
using MeFitBackend.Data.DTO.Created;
using MeFitBackend.Data.DTO.UserWorkout;
using MeFitBackend.Data.DTO.UserGoal;
using MeFitBackend.Data.DTO.UserProgram;

namespace MeFitBackend.Controllers
{
    [Route("api/v1/User")]
    [ApiController]
    [Produces("application/Json")]
    [Consumes("application/Json")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<UserDTO>>(users));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(string id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound(new EntityNotFoundException(nameof(user), id));
            }
            return Ok(_mapper.Map<UserDTO>(user));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, UserPutDTO user)
        {
            if(id != user.Id)
            {
                throw new EntityNotFoundException(nameof(user), id);
            }

            try
            {
                await _userService.UpdateAsync(_mapper.Map<User>(user));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(UserPostDTO user)
        {
            var newUser = await _userService.AddAsync(_mapper.Map<User>(user));

            return CreatedAtAction("GetUser",
                new { id = newUser.Id },
                _mapper.Map<UserDTO>(newUser));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                await _userService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}/usergoals")]
        public async Task<ActionResult<IEnumerable<UserGoalDTO>>> GetAllUserGoals(string id)
        {
            try
            {
                var usergoals = await _userService.GetUserGoalsAsync(id);
                var ugDTO = _mapper.Map<IEnumerable<UserGoalDTO>>(usergoals);
                return Ok(ugDTO);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }

        [HttpPut("{id}/usergoals")]
        public async Task<ActionResult> PutUserGoals(string id, [FromBody] int[] usergoalIds)
        {
            try
            {
                await _userService.UpdateUserGoalsAsync(id, usergoalIds);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }

        [HttpGet("{id}/created")]
        public async Task<ActionResult<IEnumerable<CreatedDTO>>> GetAllCreated(string id)
        {
            try
            {
                var created = await _userService.GetUserGoalsAsync(id);
                var createdDTO = _mapper.Map<IEnumerable<CreatedDTO>>(created);
                return Ok(createdDTO);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }

        [HttpPut("{id}/created")]
        public async Task<ActionResult> PutCreated(string id, [FromBody] int[] createdIds)
        {
            try
            {
                await _userService.UpdateCreatedAsync(id, createdIds);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }

        [HttpGet("{id}/userexercises")]
        public async Task<ActionResult<IEnumerable<UserExerciseDTO>>> GetAllUserExercises(string id)
        {
            try
            {
                var userexercises = await _userService.GetUserExercisesAsync(id);
                var ueDTO = _mapper.Map<IEnumerable<UserExerciseDTO>>(userexercises);
                return Ok(ueDTO);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }

        [HttpPut("{id}/userexercises")]
        public async Task<ActionResult> PutUserExercises(string id, [FromBody] int[] userexerciseIds)
        {
            try
            {
                await _userService.UpdateUserExercisesAsync(id, userexerciseIds);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }

        [HttpGet("{id}/userworkout")]
        public async Task<ActionResult<IEnumerable<UserWorkoutDTO>>> GetAllUserWorkouts(string id)
        {
            try
            {
                var userworkouts = await _userService.GetUserWorkoutsAsync(id);
                var uwDTO = _mapper.Map<IEnumerable<UserWorkoutDTO>>(userworkouts);
                return Ok(uwDTO);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }

        [HttpPut("{id}/userworkout")]
        public async Task<ActionResult> PutUserWorkouts(string id, [FromBody] int[] workoutIds)
        {
            try
            {
                await _userService.UpdateUserWorkoutsAsync(id, workoutIds);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }

        [HttpGet("{id}/userprogram")]
        public async Task<ActionResult<IEnumerable<UserProgramDTO>>> GetAllUserPrograms(string id)
        {
            try
            {
                var userprograms = await _userService.GetUserProgramsAsync(id);
                var upDTO = _mapper.Map<IEnumerable<UserWorkoutDTO>>(userprograms);
                return Ok(upDTO);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }

        [HttpPut("{id}/userprogram")]
        public async Task<ActionResult> PutUserPrograms(string id, [FromBody] int[] userprogramIds)
        {
            try
            {
                await _userService.UpdateUserProgramsAsync(id, userprogramIds);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }


    }
}
