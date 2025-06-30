using CSharpFunctionalExtensions;
using EducationProcess.Presentation.Contracts.Teachers;
using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Application.Services;
using EducationProcessAPI.Application.Services.CRUD.Definition;
using EducationProcessAPI.Application.Services.CRUD.Implementation;
using EducationProcessAPI.Application.ServiceUtils;
using EducationProcessAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Presentation;
using System.Web.Http.Results;

namespace EducationProcess.Presentation.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TeachersController : BaseController
    {
        private readonly ITeacherService _teacherService;

        public TeachersController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] BaseTeacherRequest request)
        {
            var result = await _teacherService.CreateAsync(new CreateTeacherDto(request.Surname, 
                                                                                request.Name, 
                                                                                request.Patronymic,
                                                                                request.BirthDate));

            return FormResultFromService(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Teacher>> GetByIdAsync(Guid id)
        {
            var result = await _teacherService.GetByIdAsync(id);

            return FormResultFromService(result);
        }


        [HttpDelete]
        public async Task<IActionResult> RemoveAllAsync()
        {
            var result = await _teacherService.DeleteAllAsync();

            return FormResultFromService(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<Teacher>>> GetByAfterIdWithPaginationAsync([FromQuery] Guid afterId, [FromQuery] int size = 10)
        {
            if (size < 1)
                return BadRequest();

            var result = await _teacherService.GetAfterWithSizeAsync(afterId, size);

            return FormResultFromService(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFullAsync([FromBody] Contracts.Teachers.UpdateTeacherRequest teacher)
        {
            var updatedTeacher = new TeacherDto
            (
                teacher.Id,
                teacher.TeacherData.Surname,
                teacher.TeacherData.Name,
                teacher.TeacherData.Patronymic,
                teacher.TeacherData.BirthDate
            );

            var result = await _teacherService.UpdateAsync(updatedTeacher);

            return FormResultFromService(result);
        }

        [HttpPatch]

        public async Task<IActionResult> UpdateBirthDateAsync([FromBody] UpdateTeacherBirthRequest request)
        {
            var result = await _teacherService.UpdateBirthDateAsync(request.Id, request.BirthDate);

            return FormResultFromService(result);
        }


        [HttpGet("{year:int}")]
        public async Task<IActionResult> GetByEduYear(int year)
        {
            var result = await _teacherService.GetByEduYearAsync(year);

            return FormResultFromService(result);
        }

    }
}
