using Application.DTO;
using EducationProcess.Presentation.Contracts;
using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Application.Services.CRUD.Definition;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EducationProcess.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnalysisController : ControllerBase
    {

        private readonly IAnalysisService _analysisService;
        private readonly IValidator<IFormFile> _validator;

        public AnalysisController(IAnalysisService analysisService, IValidator<IFormFile> validator)
        {
            _analysisService = analysisService;
            _validator = validator;
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

            var result = await _analysisService.CreateCriteriaAsync(criteriaDto);

            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteByTargetAsync(AnalysisTarget target)
        {
            await _analysisService.DeleteByTargetAsync(target);

            return Ok();
        }


        [HttpPost("options")]
        public async Task<ActionResult<Guid>> CreateOptionAsync([FromBody] CreateOptionRequest request)
        {
            var result = await _analysisService.CreateOptionAsync(request.criteriaId, request.name);

            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }


        [HttpPost("document")]
        public async Task<ActionResult<Guid>> CreateDocumentAsync([FromBody] CreateAnalysisDocumentDto request)
        {
            var result = await _analysisService.CreateAnalysisDocumentAsync(request);

            return result.Item1.IsValid ? Ok(result.Item2) : BadRequest(result.Item1.Errors);
        }


        [HttpPost("upload")]
        public async Task<IActionResult> CreateFromFileAsync([FromForm] CreateAnalysisFromFileRequest request)
        {
            var file = request.File;

            if (file == null)
            {
                return BadRequest();
            }

            var validator = await _validator.ValidateAsync(file);

            if (!validator.IsValid)
            {
                return BadRequest(validator.Errors);
            }

            await _analysisService.CreateFromFileAsync(request);

            return Ok();
        }


        [HttpGet]
        public async Task<ActionResult<List<GetCriteriasWithOptionsDto>>> GetCriteriasByTargetAsync(AnalysisTarget target)
        {

            return await _analysisService.GetByTargetAsync(target);
        }

    }
}
