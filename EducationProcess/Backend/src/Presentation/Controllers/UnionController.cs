using EducationProcess.Presentation.Contracts.ArtUnion;
using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Application.Services.CRUD.Definition;
using Microsoft.AspNetCore.Mvc;
using Presentation;

namespace EducationProcess.Presentation.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UnionController : BaseController
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

            return FormResultFromService(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetByTeacherIdAsync(Guid teacherId)
        {
            var result = await _unionService.GetByTeacherIdAsync(teacherId);

            return FormResultFromService(result);
        }

    }
}
