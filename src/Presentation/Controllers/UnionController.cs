using EducationProcess.Presentation.Contracts.ArtUnion;
using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Application.Services.CRUD.Definition;
using EducationProcessAPI.Application.ServiceUtils;
using Microsoft.AspNetCore.Mvc;

namespace EducationProcess.Presentation.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UnionController : ControllerBase
    {
        private readonly IUnionService _unionService;

        public UnionController(IUnionService unionService)
        {
            _unionService = unionService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]  CreateUnionRequest unionRequest)
        {
            CreateUnionDto newArtUnion = new CreateUnionDto
            (
                unionRequest.Description,
                unionRequest.Duration,
                unionRequest.Name,
                unionRequest.TeacherId,
                unionRequest.DirectionId
            );

            var result = await _unionService.CreateAsync(newArtUnion);

            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }


        [HttpGet]
        public async Task<List<GetUnionDto>> GetByTeacherIdAsync(Guid teacherId)
        {
            return await _unionService.GetByTeacherIdAsync(teacherId);  
        }


    }
}
