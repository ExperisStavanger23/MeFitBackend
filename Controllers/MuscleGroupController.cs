using AutoMapper;
using MeFitBackend.Data.DTO.MuscleGroup;
using MeFitBackend.Services.MuscleGroups;
using Microsoft.AspNetCore.Mvc;

namespace MeFitBackend.Controllers
{
    [Route("api/v1/Musclegroup")]
    [ApiController]
    [Produces("application/Json")]
    [Consumes("application/Json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class MuscleGroupController : ControllerBase
    {
        private readonly IMuscleGroupService _musclegroupService;
        private readonly IMapper _mapper;

        public MuscleGroupController(IMuscleGroupService musclegroupservice, IMapper mapper)
        {
            _musclegroupService = musclegroupservice;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a list of musclegroups
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MuscleGroupDTO>>> GetAllMuscleGroups()
        {
            var musclegroups = await _musclegroupService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<MuscleGroupDTO>>(musclegroups));
        }
    }
}
