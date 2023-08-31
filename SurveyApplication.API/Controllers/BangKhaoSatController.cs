using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Commands;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Queries;
using SurveyApplication.Application.Responses;

namespace SurveyApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BangKhaoSatController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BangKhaoSatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllBangKhaoSat")]
        public async Task<ActionResult<List<BangKhaoSatDto>>> GetAllBangKhaoSat()
        {
            var leaveAllocations = await _mediator.Send(new GetBangKhaoSatListRequest());
            return Ok(leaveAllocations);
        }

        [HttpGet("GetBangKhaoSatByCondition")]
        public async Task<ActionResult<PageCommandResponse<BangKhaoSatDto>>> GetBangKhaoSatByCondition(int pageIndex = 1, int pageSize = 5, string? keyword = "")
        {
            var leaveAllocations = await _mediator.Send(new GetBangKhaoSatConditionsRequest { PageIndex = pageIndex, PageSize = pageSize, Keyword = keyword });
            return leaveAllocations;
        }

        [HttpGet("GetByBangKhaoSat/{id}")]
        public async Task<ActionResult<List<BangKhaoSatDto>>> GetByBangKhaoSat(int id)
        {
            var leaveAllocations = await _mediator.Send(new GetBangKhaoSatDetailRequest { Id = id });
            return Ok(leaveAllocations);
        }

        [HttpPost("CreateBangKhaoSat")]
        public async Task<ActionResult<BangKhaoSatDto>> CreateBangKhaoSat([FromBody] CreateBangKhaoSatDto obj)
        {
            obj.TrangThai = 1;
            var command = new CreateBangKhaoSatCommand { BangKhaoSatDto = obj };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("UpdateBangKhaoSat")]
        public async Task<ActionResult<BangKhaoSatDto>> UpdateBangKhaoSat([FromBody] UpdateBangKhaoSatDto obj)
        {
            var command = new UpdateBangKhaoSatCommand { BangKhaoSatDto = obj };
            await _mediator.Send(command);
            return Ok(new
            {
                Success = true, 
            });
        }

        [HttpDelete("DeleteBangKhaoSat/{id}")]
        public async Task<ActionResult<List<BangKhaoSatDto>>> DeleteBangKhaoSat(int id)
        {
            var command = new DeleteBangKhaoSatCommand { Id = id };
            await _mediator.Send(command);
            return Ok(new
            {
                Success = true,
            });
        }
    }
}
