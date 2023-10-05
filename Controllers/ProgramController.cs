using AutoMapper;
using MeFitBackend.Data.DTO.Programs;
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
    public class ProgramController : ControllerBase
    {
        private readonly IProgramService _programService;
        private readonly IMapper _mapper;

        public ProgramController(IProgramService programService, IMapper mapper)
        {
            _programService = programService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProgramDTO>>> GetPrograms()
        {
            var programs = await _programService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ProgramDTO>>(programs));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProgramDTO>> GetProgram(int id)
        {
            var program = await _programService.GetByIdAsync(id);
            if (program == null)
            {
                return NotFound(new EntityNotFoundException("program", id));
            }

            return Ok(_mapper.Map<ProgramDTO>(program));
        }

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

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ProgramDTO>> PostProgram(ProgramPostDTO program)
        {
            var newProgram = await _programService.AddAsync(_mapper.Map<Program>(program));

            return CreatedAtAction("GetProgram",
                new { id = newProgram.Id },
                _mapper.Map<ProgramDTO>(newProgram));
        }

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
    }
}
