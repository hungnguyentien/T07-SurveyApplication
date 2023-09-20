using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.API.Models;
using SurveyApplication.Application.DTOs.Module;
using SurveyApplication.Application.Features.Module.Requests.Commands;
using SurveyApplication.Application.Features.Module.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ModuleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ModuleDto>>> GetAllModule()
        {
            var leaveAllocations = await _mediator.Send(new GetModuleListRequest());
            return Ok(leaveAllocations);
        }

        [HttpGet("GetByCondition")]
        public async Task<ActionResult<BaseQuerieResponse<ModuleDto>>> GetModuleByCondition(
            [FromQuery] Paging paging)
        {
            var leaveAllocations = await _mediator.Send(new GetModuleConditionsRequest
            { PageIndex = paging.PageIndex, PageSize = paging.PageSize, Keyword = paging.Keyword });
            return leaveAllocations;
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<List<ModuleDto>>> GetByModule(int id)
        {
            var leaveAllocations = await _mediator.Send(new GetModuleDetailRequest { Id = id });
            return Ok(leaveAllocations);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<ModuleDto>> CreateModule([FromBody] CreateModuleDto obj)
        {
            var command = new CreateModuleCommand { ModuleDto = obj };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("Update")]
        public async Task<ActionResult<ModuleDto>> UpdateModule([FromBody] UpdateModuleDto obj)
        {
            var command = new UpdateModuleCommand { ModuleDto = obj };
            await _mediator.Send(command);
            return Ok(new
            {
                Success = true
            });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteModule(int id)
        {
            var command = new DeleteModuleCommand { Ids = new List<int> { id } };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("DeleteMultiple")]
        public async Task<ActionResult> DeleteMultipleCauHoi(List<int> ids)
        {
            var command = new DeleteModuleCommand { Ids = ids };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
