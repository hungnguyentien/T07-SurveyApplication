using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Application.DTOs.DotKhaoSat;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Commands;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Queries;
using SurveyApplication.Application.Features.DotKhaoSats.Requests.Commands;
using SurveyApplication.Application.Features.DotKhaoSats.Requests.Queries;
using SurveyApplication.Application.Responses;

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
            var leaveAllocations = await _mediator.Send(new GetDotKhaoSatListRequest());
            return Ok(leaveAllocations);
        }

        [HttpGet("GetDotKhaoSatByCondition")]
        public async Task<ActionResult<PageCommandResponse<DotKhaoSatDto>>> GetBangKhaoSatByCondition(int pageIndex = 1, int pageSize = 5, string? keyword = "")
        {
            var leaveAllocations = await _mediator.Send(new GetDotKhaoSatConditionsRequest { PageIndex = pageIndex, PageSize = pageSize, Keyword = keyword });
            return leaveAllocations;
        }

        [HttpGet("GetByBangKhaoSat/{id}")]
        public async Task<ActionResult<List<DotKhaoSatDto>>> GetByBangKhaoSat(int id)
        {
            var leaveAllocations = await _mediator.Send(new GetDotKhaoSatDetailRequest { Id = id });
            return Ok(leaveAllocations);
        }

        [HttpPost("CreateDotKhaoSat")]
        public async Task<ActionResult<DotKhaoSatDto>> CreateBangKhaoSat([FromBody] CreateDotKhaoSatDto obj)
        {
            obj.TrangThai = 1;
            var command = new CreateDotKhaoSatCommand { DotKhaoSatDto = obj };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("UpdateDotKhaoSat")]
        public async Task<ActionResult<DotKhaoSatDto>> UpdateBangKhaoSat([FromBody] UpdateDotKhaoSatDto obj)
        {
            var command = new UpdateDotKhaoSatCommand { DotKhaoSatDto = obj };
            await _mediator.Send(command);
            return Ok(new
            {
                Success = true,
            });
        }

        [HttpDelete("DeleteDotKhaoSat/{id}")]
        public async Task<ActionResult<List<DotKhaoSatDto>>> DeleteBangKhaoSat(int id)
        {
            var command = new DeleteDotKhaoSatCommand { Id = id };
            await _mediator.Send(command);
            return Ok(new
            {
                Success = true,
            });
        }
    }
}
