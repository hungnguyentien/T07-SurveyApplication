using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.API.Models;
using SurveyApplication.Application.DTOs.QuanHuyen;
using SurveyApplication.Application.DTOs.XaPhuong;
using SurveyApplication.Application.Features.QuanHuyens.Requests.Commands;
using SurveyApplication.Application.Features.XaPhuongs.Requests.Commands;
using SurveyApplication.Application.Features.XaPhuongs.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XaPhuongController : ControllerBase
    {
        private readonly IMediator _mediator;

        public XaPhuongController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<XaPhuongDto>>> GetAllXaPhuong()
        {
            var leaveAllocations = await _mediator.Send(new GetXaPhuongListRequest());
            return Ok(leaveAllocations);
        }

        [HttpGet("GetByCondition")]
        public async Task<ActionResult<BaseQuerieResponse<XaPhuongDto>>> GetXaPhuongByCondition([FromQuery] Paging paging)
        {
            var leaveAllocations = await _mediator.Send(new GetXaPhuongConditionsRequest { PageIndex = paging.PageIndex, PageSize = paging.PageSize, Keyword = paging.Keyword });
            return leaveAllocations;
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<List<XaPhuongDto>>> GetByXaPhuong(int id)
        {
            var leaveAllocations = await _mediator.Send(new GetXaPhuongDetailRequest { Id = id });
            return Ok(leaveAllocations);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<XaPhuongDto>> CreateXaPhuong([FromBody] CreateXaPhuongDto obj)
        {
            var command = new CreateXaPhuongCommand { XaPhuongDto = obj };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("Update")]
        public async Task<ActionResult<XaPhuongDto>> UpdateXaPhuong([FromBody] UpdateXaPhuongDto obj)
        {
            var command = new UpdateXaPhuongCommand { XaPhuongDto = obj };
            await _mediator.Send(command);
            return Ok(new
            {
                Success = true,
            });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<List<XaPhuongDto>>> DeleteXaPhuong(int id)
        {
            var command = new DeleteXaPhuongCommand { Id = id };
            await _mediator.Send(command);
            return Ok(new
            {
                Success = true,
            });
        }

        [HttpPost("Import")]
        public async Task<IActionResult> ImportJsonFile([FromForm] ImportXaPhuongDto obj)
        {
            var command = new ImportXaPhuongCommand { File = obj.File };
            await _mediator.Send(command);
            return Ok(new
            {
                Success = true,
            });
        }
    }
}
