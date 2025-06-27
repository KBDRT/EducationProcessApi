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

namespace EducationProcess.Presentation.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeachersController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] BaseTeacherRequest request)
        {
            var result = await _teacherService.CreateAsync(
                                    request.Surname, 
                                    request.Name, 
                                    request.Patronymic,
                                    request.BirthDate);

            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Teacher>> GetByIdAsync(Guid id)
        {
            var teacher = await _teacherService.GetByIdAsync(id);
            if (teacher != null)
            {
                return Ok(teacher);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpDelete]
        public async Task<IActionResult> RemoveAllAsync()
        {
            var deletedRecordsCount = await _teacherService.DeleteAllAsync();

            return new JsonResult(new { message = $"Deleted records: {deletedRecordsCount}" })
            {
                StatusCode = 200
            };
        }

        [HttpGet]
        public async Task<ActionResult<List<Teacher>>> GetByAfterIdWithPaginationAsync([FromQuery] Guid afterId, [FromQuery] int size = 10)
        {
            if (size < 1)
                return BadRequest();

            var teachers = await _teacherService.GetAfterWithSizeAsync(afterId, size);
            if (teachers.Count > 0)
            {
                return Ok(teachers);
            }
            else
            {
                return NotFound();
            }
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

            AppOperationStatus status = await _teacherService.UpdateAsync(updatedTeacher);

            return StatusCode(status.GetStatusCodeByOperationStatus());
        }

        [HttpPatch]

        public async Task<IActionResult> UpdateBirthDateAsync([FromBody] UpdateTeacherBirthRequest request)
        {
            AppOperationStatus status = await _teacherService.UpdateBirthDateAsync(request.Id, request.BirthDate);

            return StatusCode(status.GetStatusCodeByOperationStatus());
        }


        [HttpGet("{year:int}")]
        public async Task<List<TeachersForEduYearDto>?> GetByEduYear(int year)
        {
            return await _teacherService.GetByEduYearAsync(year);
        }

    }
}
