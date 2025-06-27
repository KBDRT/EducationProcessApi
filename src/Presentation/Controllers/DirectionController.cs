using EducationProcess.Presentation.Contracts.ArtDirection;
using EducationProcessAPI.Application.Services.CRUD.Definition;
using Microsoft.AspNetCore.Mvc;

namespace EducationProcess.Presentation.Controllers
{
    [ApiController]

    [Route("[controller]")]
    public class DirectionController : ControllerBase
    {
        private readonly IDirectionService _directionService;

        public DirectionController(IDirectionService directionService)
        {
            _directionService = directionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateDirectionRequest artDirectionRequest)
        {
        
            var result = await _directionService.CreateAsync(artDirectionRequest.FullName, 
                                                             artDirectionRequest.ShortName,
                                                             artDirectionRequest.Description);

            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _directionService.GetAsync());
        }


    }
}
