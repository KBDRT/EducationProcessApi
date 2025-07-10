using Application.CQRS.Teachers.Commands.CreateTeacher;
using Application.CQRS.Teachers.Commands.DeleteAllTeachers;
using Application.CQRS.Teachers.Commands.DeleteTeacherById;
using Application.CQRS.Teachers.Commands.UpdateTeacher;
using Application.CQRS.Teachers.Commands.UpdateTeacherBirthDate;
using Application.CQRS.Teachers.Queries.GetTeacherById;
using Application.CQRS.Teachers.Queries.GetTeachersByEducationYear;
using Application.CQRS.Teachers.Queries.GetTeachersPaginationAfter;
using Application.DTO;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2016.Excel;
using EducationProcess.Presentation.Contracts.Teachers;
using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Application.Services.CRUD.Definition;
using EducationProcessAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation;

namespace EducationProcess.Presentation.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "RoleHead")]
    public class TeachersController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TeachersController> _logger;

        public TeachersController(IMediator mediator, ILogger<TeachersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateTeacherCommand command)
        {
            var result = await _mediator.Send(command);

            return FormResultFromService(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Teacher>> GetByIdAsync(Guid id)
        {
            var result = await _mediator.Send(new GetTeacherByIdQuery(id));

            return FormResultFromService(result);
        }


        [HttpDelete]
        public async Task<IActionResult> RemoveAllAsync()
        {
            var result = await _mediator.Send(new DeleteAllTeachersCommand());

            return FormResultFromService(result);
        }



        [HttpDelete("single")]
        public async Task<IActionResult> RemoveById([FromBody] DeleteTeacherByIdCommand request)
        {
            var result = await _mediator.Send(request);

            return FormResultFromService(result);
        }


        [HttpGet]
        public async Task<ActionResult<List<Teacher>>> GetByAfterIdWithPaginationAsync([FromQuery] Guid afterId, [FromQuery] int size = 10)
        {

            if (size < 1)
                return BadRequest();

            var result = await _mediator.Send(new GetTeachersAfterIdQuery(afterId, size));

            return FormResultFromService(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFullAsync([FromBody] UpdateTeacherCommand request)
        {
            var result = await _mediator.Send(request);

            return FormResultFromService(result);
        }

        [HttpPatch]

        public async Task<IActionResult> UpdateBirthDateAsync([FromBody] UpdateTeacherBirthRequest request)
        {
            var result = await _mediator.Send(new UpdateTeacherBirthDateCommand(request.Id, request.BirthDate));

            return FormResultFromService(result);
        }


        [HttpGet("{year:int}")]
        public async Task<IActionResult> GetByEduYear(int year)
        {
            var result = await _mediator.Send(new GetTeachersByEducationYearQuery(year));

            return FormResultFromService(result);
        }

    }
}
