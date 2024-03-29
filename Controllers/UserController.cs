﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MeFitBackend.Data.DTO.Users;
using MeFitBackend.Data.Entities;
using MeFitBackend.Data.Exceptions;
using MeFitBackend.Services.Users;
using MeFitBackend.Data.DTO.UserExercise;
using MeFitBackend.Data.DTO.UserWorkout;
using MeFitBackend.Data.DTO.UserProgram;
using Microsoft.AspNetCore.Authorization;

namespace MeFitBackend.Controllers
{
    [Route("api/v1/User")]
    [ApiController]
    [Produces("application/Json")]
    [Consumes("application/Json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }


        /// <summary>
        /// Retrieves a list of users
        /// </summary>
        /// <returns>List of user objects</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<UserDTO>>(users));
        }

        /// <summary>
        /// Retrieves user by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the user</param>
        /// <returns>The user object with the specified Id</returns>
        /// <response code="200">Ok - Success <br/></response>
        /// <response code="404">Not Found - The user with the given ID was not found.</response>
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


        /// <summary>
        /// Updates an existing users information
        /// </summary>
        /// <param name="id">The unique identifier of the user</param>
        /// <param name="user">The updated user data</param>
        /// <returns>NoContent if the update is successful</returns>
        /// <exception cref="EntityNotFoundException">Error message that implies that user does not exist</exception>
        /// <response code="200">Ok - Success <br/></response>
        /// <response code="404">Not Found - The user with the given ID was not found.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, UserPutDTO user)
        {
            if (id != user.Id)
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

            return Ok();
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="user">Data of the new user</param>
        /// <returns>The newly created user</returns>
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserPostDTO user)
        {
            var newUser = await _userService.AddAsync(_mapper.Map<User>(user));

            return CreatedAtAction("GetUser",
                new { id = newUser.Id },
                newUser);
        }

        /// <summary>
        /// Deletes a user by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the user to delete</param>
        /// <response code="200">Ok - Success <br/></response>
        /// <response code="404">Not Found - The user with the given ID was not found.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                await _userService.DeleteByIdAsync(id);
                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Updates the role of user
        /// </summary>
        /// <param name="id">The unique identifier of the user</param>
        /// <param name="roleIds">The Ids of the new appointed roles of user</param>
        /// <response code="200">Ok - Success <br/></response>
        /// <response code="404">Not Found - The user with the given ID was not found.</response>
        [HttpPut("{id}/userroles")]
        public async Task<ActionResult> PutUserRoles(string id, [FromBody] int[] roleIds)
        {
            try
            {
                await _userService.UpdateUserRolesAsync(id, roleIds);
                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }

        /// <summary>
        /// Retrieves a users exercises by their unique identifier
        /// </summary>
        /// <param name="id">The id of the user to retrieve the exercises from</param>
        /// <exception cref="EntityNotFoundException">Thrown when the user with the provided 'id' is not found.</exception>
        /// <response code="200">Ok - Success <br/></response>
        /// <response code="404">Not Found - The user with the given ID was not found.</response>
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

        /// <summary>
        /// Update exercises in user
        /// </summary>
        /// <param name="id">The unique identifier of the user</param>
        /// <param name="exerciseIds">The unique identifier of the exercise</param>
        /// <response code="200">Ok - Success <br/></response>
        /// <response code="404">Not Found - The user with the given ID was not found.</response>
        [HttpPut("{id}/userexercises")]
        public async Task<ActionResult> PutUserExercises(string id, [FromBody] int[] exerciseIds)
        {
            try
            {
                await _userService.UpdateUserExercisesAsync(id, exerciseIds);
                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }
        /// <summary>
        /// Retrieves a users workouts by their unique identifier
        /// </summary>
        /// <param name="id">The id of the user to retrieve the workouts from</param>
        /// <exception cref="EntityNotFoundException">Thrown when the user with the provided 'id' is not found.</exception>
        /// <response code="200">Ok - Success <br/></response>
        /// <response code="404">Not Found - The user with the given ID was not found.</response>

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

        /// <summary>
        /// Update workouts in user
        /// </summary>
        /// <param name="id">The unique identifier of the user</param>
        /// <param name="workoutIds">The unique identifiers of the workouts</param>
        /// <response code="200">Ok - Success <br/></response>
        /// <response code="404">Not Found - The user with the given ID was not found.</response>
        [HttpPut("{id}/userworkout")]
        public async Task<ActionResult> PutUserWorkouts(string id, UserWorkoutPostDTO[] workouts)
        {
            Console.WriteLine("UpdateUserWorkoutsAsync");
            try
            {
                await _userService.UpdateUserWorkoutsAsync(id, workouts);
                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }

        /// <summary>
        /// Update workout goal in user
        /// </summary>
        /// <param name="id">The unique identifier of the user</param>
        /// <param name="uwId">The unique identifier of the userworkout</param>
        /// <param name="done">The date for when workout is done<param>
        /// <response code="200">Ok - Success <br/></response>
        /// <response code="404">Not Found - The user or userWorkout with the given ID was not found.</response>
        [HttpPut("{id}/userworkout/{uwId}/workoutgoal")]
        public async Task<ActionResult> UpdateWorkoutGoal(string id, int uwId, DateTime? done)
        {
            try
            {
                await _userService.UpdateWorkoutGoal(id, uwId, done);
                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }
        /// <summary>
        /// Gets all userPrograms for a user
        /// </summary>
        /// <param name="id">The unique identifier of the user</param>
        /// <response code="200">Ok - Success <br/></response>
        /// <response code="404">Not Found - The user with the given ID was not found.</response>
        [HttpGet("{id}/userprogram")]
        public async Task<ActionResult<IEnumerable<UserProgramDTO>>> GetAllUserPrograms(string id)
        {
            try
            {
                var userprograms = await _userService.GetUserProgramsAsync(id);
                var upDTO = _mapper.Map<IEnumerable<UserProgramDTO>>(userprograms);
                return Ok(upDTO);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }

        /// <summary>
        /// Update programs in user
        /// </summary>
        /// <param name="id">The unique identifier of the user</param>
        /// <param name="userPrograms">The unique identifiers of the programs in a list</param>
        /// <response code="200">Ok - Success <br/></response>
        /// <response code="404">Not Found - The user with the given ID was not found.</response>
        [HttpPut("{id}/userprogram")]
        public async Task<ActionResult> PutUserPrograms(string id, UserProgramPutDTO[] userPrograms)
        {
            try
            {
                await _userService.UpdateUserProgramsAsync(id, userPrograms);
                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }
    }
}