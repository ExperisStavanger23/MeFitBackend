using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MeFitBackend.Data.DTO.Users;
using MeFitBackend.Data.Entities;
using MeFitBackend.Data.Exceptions;
using MeFitBackend.Services.Users;



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
    }
}
