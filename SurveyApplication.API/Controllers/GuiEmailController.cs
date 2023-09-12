using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.API.Models;
using SurveyApplication.Application.DTOs.GuiEmail;
using SurveyApplication.Application.Features.GuiEmail.Requests.Commands;
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

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<GuiEmailDto>>> GetAllGuiEmail()
        {
            var leaveAllocations = await _mediator.Send(new GetGuiEmailListRequest());
            return Ok(leaveAllocations);
        }

        [HttpGet("GetByCondition")]
        public async Task<ActionResult<List<GuiEmailDto>>> GetGuiEmailByCondition([FromQuery] Paging paging)
        {
            var lstGuiMail = await _mediator.Send(new GetGuiEmailConditionsRequest { PageIndex = paging.PageIndex, PageSize = paging.PageSize, Keyword = paging.Keyword });
            return Ok(lstGuiMail);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<List<GuiEmailDto>>> GetByGuiEmail(int id)
        {
            var leaveAllocations = await _mediator.Send(new GetGuiEmailDetailRequest { Id = id });
            return Ok(leaveAllocations);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<GuiEmailDto>> CreateGuiEmail([FromBody] CreateGuiEmailDto obj)
        {
            var command = new CreateGuiEmailCommand { GuiEmailDto = obj };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("Update")]
        public async Task<ActionResult<GuiEmailDto>> UpdateGuiEmail([FromBody] UpdateGuiEmailDto obj)
        {
            var command = new UpdateGuiEmailCommand { GuiEmailDto = obj };
            await _mediator.Send(command);
            return Ok(new
            {
                Success = true,
            });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<List<GuiEmailDto>>> DeleteGuiEmail(int id)
        {
            var command = new DeleteGuiEmailCommand { Id = id };
            await _mediator.Send(command);
            return Ok(new
            {
                Success = true,
            });
        }
    }
}
