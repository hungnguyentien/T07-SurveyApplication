using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Application.DTOs.GuiEmail;
using SurveyApplication.Application.Features.GuiEmails.Requests.Commands;
using SurveyApplication.Application.Features.GuiEmails.Requests.Queries;

namespace SurveyApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuiEmailController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GuiEmailController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllGuiEmail")]
        public async Task<ActionResult<List<GuiEmailDto>>> GetAllGuiEmail()
        {
            var leaveAllocations = await _mediator.Send(new GetGuiEmailListRequest());
            return Ok(leaveAllocations);
        }

        [HttpGet("GetGuiEmailByCondition")]
        public async Task<ActionResult<List<GuiEmailDto>>> GetGuiEmailByCondition(int pageIndex = 1, int pageSize = 5, string? keyword = "")
        {
            var leaveAllocations = await _mediator.Send(new GetGuiEmailConditionsRequest { PageIndex = pageIndex, PageSize = pageSize, Keyword = keyword });
            return Ok(leaveAllocations);
        }

        [HttpGet("GetByGuiEmail/{id}")]
        public async Task<ActionResult<List<GuiEmailDto>>> GetByGuiEmail(int id)
        {
            var leaveAllocations = await _mediator.Send(new GetGuiEmailDetailRequest { Id = id });
            return Ok(leaveAllocations);
        }

        [HttpPost("CreateGuiEmail")]
        public async Task<ActionResult<GuiEmailDto>> CreateGuiEmail([FromBody] CreateGuiEmailDto obj)
        {
            var command = new CreatGuiEmailCommand { GuiEmailDto = obj };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("UpdateGuiEmail")]
        public async Task<ActionResult<GuiEmailDto>> UpdateGuiEmail([FromBody] UpdateGuiEmailDto obj)
        {
            var command = new UpdateGuiEmailCommand { GuiEmailDto = obj };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("DeleteGuiEmail/{id}")]
        public async Task<ActionResult<List<GuiEmailDto>>> DeleteGuiEmail(int id)
        {
            var command = new DeleteGuiEmailCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
