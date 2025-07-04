using CSharpFunctionalExtensions;
using EducationProcess.Presentation.Contracts.Lesson;
using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Application.Services.CRUD.Definition;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Presentation;

namespace EducationProcess.Presentation.Controllers
{

    [ApiController]

    [Route("[controller]")]

    public class LessonController : BaseController
    {
        private readonly ILessonService _lessonService;
        private readonly IMemoryCache _memoryCache;

        public LessonController(ILessonService lessonService, IMemoryCache memoryCache)
        {
            _lessonService = lessonService;
            _memoryCache = memoryCache;
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

            return FormResultFromService(result);
        }

        [HttpGet("{groupId:guid}")]
        public async Task<ActionResult<LessonShortDto>> GetByGroupIdAsync(Guid groupId)
        {
            var result = await _lessonService.GetByGroupIdAsync(groupId);

            return FormResultFromService(result);
        }

        [HttpGet]
        public async Task<ActionResult<LessonShortDto>> GetByIdAsync([FromQuery] Guid id)
        {
            var result = await _lessonService.GetByIdAsync(id);

            return FormResultFromService(result);
        }

    }
}
