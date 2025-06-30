using Application.DTO;
using CSharpFunctionalExtensions;
using EducationProcess.Presentation.Contracts.ArtDirection;
using EducationProcessAPI.Application.Services.CRUD.Definition;
using Microsoft.AspNetCore.Mvc;
using Presentation;

namespace EducationProcess.Presentation.Controllers
{
    [ApiController]

    [Route("[controller]")]
    public class DirectionController : BaseController
    {
        private readonly IDirectionService _directionService;

        public DirectionController(IDirectionService directionService)
        {
            _directionService = directionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateDirectionRequest artDirectionRequest)
        {
        
            var result = await _directionService.CreateAsync(new CreateDirectionDto(artDirectionRequest.FullName,
                                                                                    artDirectionRequest.ShortName,
                                                                                    artDirectionRequest.Description));

            return FormResultFromService(result.ResultData, result.Messages, result.ResultCode);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _directionService.GetAsync();

            return FormResultFromService(result.ResultData, result.Messages, result.ResultCode);
        }

    }
}
