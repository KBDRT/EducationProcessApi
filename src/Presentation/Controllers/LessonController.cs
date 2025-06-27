using EducationProcess.Presentation.Contracts.Group;
using EducationProcess.Presentation.Contracts.Lesson;
using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Application.Services.CRUD.Definition;
using EducationProcessAPI.Application.Services.CRUD.Implementation;
using EducationProcessAPI.Application.ServiceUtils;
using EducationProcessAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EducationProcess.Presentation.Controllers
{

    [ApiController]

    [Route("[controller]")]

    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]  CreateLessonRequest request)
        {
            var newLesson = new LessonDto
            (
                request.groupid,
                request.Name,
                request.Date,
                request.StudyHours,
                request.FormExercise,
                request.Place,
                request.FormControl
           );

            var result = await _lessonService.CreateAsync(newLesson);

            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }

        [HttpGet("{groupId:guid}")]
        public async Task<ActionResult<LessonShortDto>> GetByGroupIdAsync(Guid groupId)
        {
            var lessons = await _lessonService.GetByGroupIdAsync(groupId);
            if (lessons?.Count > 0)
            {
                return Ok(lessons);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<ActionResult<LessonShortDto>> GetByIdAsync([FromQuery] Guid id)
        {
            var lessons = await _lessonService.GetByIdAsync(id);
            if (lessons != null)
            {
                return Ok(lessons);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
