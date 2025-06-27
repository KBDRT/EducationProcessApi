using EducationProcess.Presentation.Contracts.Group;
using EducationProcessAPI.Application.Services.CRUD.Definition;
using EducationProcessAPI.Application.Services.CRUD.Implementation;
using EducationProcessAPI.Application.ServiceUtils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EducationProcessAPI.Domain.Entities;

namespace EducationProcess.Presentation.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateGroupRequest request)
        {
            var result = await _groupService.CreateAsync(
                                request.Name,
                                request.StartYear,
                                request.UnionId);

            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }

        [HttpPost("upload")]

        [Consumes("multipart/form-data")]

        public async Task<IActionResult> CreateFromFile([FromForm] CreateGroupsFromFileRequest? request)
        {
            var file = request?.file;

            if (file == null || file.Length == 0)
            {
                return BadRequest("Empty file");
            }

            await _groupService.CreateFromFileAsync(request.unionId, request.file, request.educationYear);

            return Ok();
        }

    }
}
