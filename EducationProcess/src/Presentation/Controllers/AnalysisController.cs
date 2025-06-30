using Application.DTO;
using CSharpFunctionalExtensions;
using EducationProcess.Presentation.Contracts;
using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Application.Services.CRUD.Definition;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;
using Presentation;
using Microsoft.AspNetCore.Mvc;

namespace EducationProcess.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnalysisController : BaseController
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

            var result = await _analysisService.CreateCriteriaAsync(criteriaDto);

            return FormResultFromService(result.ResultData, result.Messages, result.ResultCode);
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
            var result = await _analysisService.CreateOptionAsync(new CreateOptionDto(request.criteriaId, request.name));

            return FormResultFromService(result.ResultData, result.Messages, result.ResultCode);
        }


        [HttpPost("document")]
        public async Task<ActionResult<Guid>> CreateDocumentAsync([FromBody] CreateAnalysisDocumentDto request)
        {
            var result = await _analysisService.CreateAnalysisDocumentAsync(request);

            return FormResultFromService(result.ResultData, result.Messages, result.ResultCode); ;
        }


        [HttpPost("upload")]
        public async Task<IActionResult> CreateFromFileAsync([FromForm] CreateAnalysisFromFileRequest request)
        {
            var file = request.File;

            if (file == null)
            {
                return BadRequest();
            }

            var result = await _analysisService.CreateFromFileAsync(request);

            return FormResultFromService(null, result.Messages, result.ResultCode);
        }


        [HttpGet]
        public async Task<ActionResult<List<GetCriteriasWithOptionsDto>>> GetCriteriasByTargetAsync(AnalysisTarget target)
        {
            var result =  await _analysisService.GetByTargetAsync(target);

            return FormResultFromService(result.ResultData, result.Messages, result.ResultCode);
        }

    }
}
