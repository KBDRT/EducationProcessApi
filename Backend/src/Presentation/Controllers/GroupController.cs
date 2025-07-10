using Application.DTO;
using EducationProcess.Presentation.Contracts.Group;
using EducationProcessAPI.Application.Services.CRUD.Definition;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation;

namespace EducationProcess.Presentation.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class GroupController : BaseController
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }


        [HttpPost]
        [Authorize(Policy = "RoleHead")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateGroupRequest request)
        {
            var result = await _groupService.CreateAsync(new Application.DTO.CreateGroupDto(request.Name,
                                                                                            request.StartYear,
                                                                                            request.UnionId));

            return FormResultFromService(result);
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        [Authorize(Policy = "RoleTeacher")]

        public async Task<IActionResult> CreateFromFile([FromForm] CreateGroupsFromFileRequest request)
        {
            var file = request?.file;

            if (file == null || file.Length == 0)
            {
                return BadRequest("Empty file");
            }

            var result = await _groupService.CreateFromFileAsync(new CreateGroupFromFileDto(request.unionId, 
                                                                                request.file, 
                                                                                request.educationYear));

            return FormResultFromService(result);
        }

    }
}
