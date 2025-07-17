using Application.CQRS.Analysis.Commands.CreateCriteria;
using Application.CQRS.Analysis.Commands.CreateCriteriasFromFile;
using Application.CQRS.Analysis.Commands.CreateDocument;
using Application.CQRS.Analysis.Commands.CreateFileForDocument;
using Application.CQRS.Analysis.Commands.CreateOption;
using Application.CQRS.Analysis.Commands.DeleteCriteriasByTarget;
using Application.CQRS.Analysis.Querires.DownloadFileForDocument;
using Application.CQRS.Analysis.Querires.GetCriteriasByTarget;
using Application.CQRS.Result.CQResult;
using Application.DTO;
using CSharpFunctionalExtensions;
using EducationProcess.Presentation.Contracts;
using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation;
using System.Threading;

namespace EducationProcess.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "RoleHead")]
    public class AnalysisController : BaseController
    {

        private readonly IMediator _mediator;
        public AnalysisController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateCriteriaAsync([FromBody] CreateAnalysisCriteriaRequest request,
                                                                   CancellationToken cancellationToken)
        {
            CreateCriteriaCommand criteriaCommand = new CreateCriteriaCommand
            (
                request.AnalysisTarget,
                request.Name,
                request.Description,
                request.WordMark,
                request.Order
            );

            var result = await _mediator.Send(criteriaCommand, cancellationToken);
            return FormResultFromService(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteByTargetAsync(AnalysisTarget target,
                                                             CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteCriteriasByTargetCommand(target), cancellationToken);

            return FormResultFromService(result);
        }


        [HttpPost("options")]
        public async Task<ActionResult<Guid>> CreateOptionAsync([FromBody] CreateOptionRequest request,
                                                                 CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateOptionCommand(request.criteriaId, request.name), cancellationToken);

            return FormResultFromService(result);
        }


        [HttpPost("document")]
        public async Task<ActionResult<Guid>> CreateDocumentAsync([FromBody] CreateDocumentCommand request,
                                                                   CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);

            return FormResultFromService(result);
        }


        [HttpPost("upload")]
        public async Task<IActionResult> CreateFromFileAsync([FromForm] CreateCriteriasFromFileCommand request, 
                                                             CancellationToken cancellationToken)
        {
            var file = request.File;

            if (file == null)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(request, cancellationToken);

            return FormResultFromService(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<GetCriteriasWithOptionsDto>>> GetCriteriasByTargetAsync(AnalysisTarget target, 
                                                                                                    CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCriteriasByTargetQuery(target), cancellationToken);

            return FormResultFromService(result);
        }



        [HttpPost("downloaddocument")]
        public async Task<IActionResult> DownloadDocument(Guid documentId, CancellationToken cancellationToken)
        {

            var stream = await _mediator.Send(new DowndloadFileForDocumentCommand(documentId));
            if (stream.ResultCode == CQResultStatusCode.Success && stream.ResultData != null)
            {
                return File(stream.ResultData, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "example.docx");
            }
            else
            {
                return NotFound();
            }
  
        }

    }
}
