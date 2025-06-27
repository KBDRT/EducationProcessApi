using EducationProcess.Presentation.Contracts;
using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Application.Services.CRUD.Definition;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;
using Microsoft.AspNetCore.Mvc;

namespace EducationProcess.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnalysisController : ControllerBase
    {

        private readonly IAnalysisService _analysisService;

        public AnalysisController(IAnalysisService analysisService)
        {
            _analysisService = analysisService;
        }


        [HttpPost]
        public async Task<ActionResult<Guid>> CreateCriteriaAsync([FromBody] CreateAnalysisCriteriaRequest request)
        {
            CreateAnalysisCriteriaDto criteriaDto = new CreateAnalysisCriteriaDto
            (
                request.AnalysisTarget,
                request.Name,
                request.Description,
                request.WordMark,
                request.Order
            );

            var id = await _analysisService.CreateCriteriaAsync(criteriaDto);

            if (id != Guid.Empty)
            {
                return Ok(id);
            }
            else
            {
                return BadRequest();
            }
        }



        [HttpPost("options")]
        public async Task<ActionResult<Guid>> CreateOptionAsync([FromBody] CreateOptionRequest request)
        {

            var id = await _analysisService.CreateOptionAsync(request.criteriaId, request.name);

            if (id != Guid.Empty)
            {
                return Ok(id);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<GetCriteriasWithOptionsDto>>> GetCriteriasByTargetAsync(AnalysisTarget target)
        {

            return await _analysisService.GetByTargetAsync(target);
        }

    }
}
