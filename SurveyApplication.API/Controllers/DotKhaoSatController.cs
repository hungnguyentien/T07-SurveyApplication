using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Application.DTOs.DotKhaoSat;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Commands;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Queries;
using SurveyApplication.Application.Features.DotKhaoSats.Requests.Commands;
using SurveyApplication.Application.Features.DotKhaoSats.Requests.Queries;

namespace SurveyApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DotKhaoSatController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DotKhaoSatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllDotKhaoSat")]
        public async Task<ActionResult<List<DotKhaoSatDto>>> GetAllBangKhaoSat()
        {
            var leaveAllocations = await _mediator.Send(new GetGuiEmailListRequest());
            return Ok(leaveAllocations);
        }

        [HttpGet("GetDotKhaoSatByCondition")]
        public async Task<ActionResult<List<DotKhaoSatDto>>> GetBangKhaoSatByCondition(int pageIndex = 1, int pageSize = 10, string? keyword = "")
        {
            var leaveAllocations = await _mediator.Send(new GetGuiEmailConditionsRequest { PageIndex = pageIndex, PageSize = pageSize, Keyword = keyword });
            return Ok(leaveAllocations);
        }

        [HttpGet("GetByBangKhaoSat/{id}")]
        public async Task<ActionResult<List<DotKhaoSatDto>>> GetByBangKhaoSat(int id)
        {
            var leaveAllocations = await _mediator.Send(new GetGuiEmailDetailRequest { Id = id });
            return Ok(leaveAllocations);
        }

        [HttpPost("CreateDotKhaoSat")]
        public async Task<ActionResult<DotKhaoSatDto>> CreateBangKhaoSat([FromBody] CreateDotKhaoSatDto obj)
        {
            var command = new CreatGuiEmailCommand { DotKhaoSatDto = obj };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("UpdateDotKhaoSat")]
        public async Task<ActionResult<DotKhaoSatDto>> UpdateBangKhaoSat([FromBody] UpdateDotKhaoSatDto obj)
        {
            var command = new UpdateGuiEmailCommand { DotKhaoSatDto = obj };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("DeleteDotKhaoSat/{id}")]
        public async Task<ActionResult<List<DotKhaoSatDto>>> DeleteBangKhaoSat(int id)
        {
            var command = new DeleteGuiEmailCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
