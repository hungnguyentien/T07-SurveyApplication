using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Commands;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Queries;

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
        public async Task<ActionResult<List<BangKhaoSatDto>>> GetAllLoaiHinhDonVi()
        {
            var leaveAllocations = await _mediator.Send(new GetBangKhaoSatListRequest());
            return Ok(leaveAllocations);
        }

        [HttpGet("GetBangKhaoSatByCondition")]
        public async Task<ActionResult<List<BangKhaoSatDto>>> GetLoaiHinhDonViByCondition(int pageIndex = 1, int pageSize = 10, string? keyword = "")
        {
            var leaveAllocations = await _mediator.Send(new GetBangKhaoSatConditionsRequest { PageIndex = pageIndex, PageSize = pageSize, Keyword = keyword });
            return Ok(leaveAllocations);
        }

        [HttpGet("GetByBangKhaoSat/{id}")]
        public async Task<ActionResult<List<BangKhaoSatDto>>> GetByLoaiHinhDonVi(int id)
        {
            var leaveAllocations = await _mediator.Send(new GetBangKhaoSatDetailRequest { Id = id });
            return Ok(leaveAllocations);
        }

        [HttpPost("CreateBangKhaoSat")]
        public async Task<ActionResult<BangKhaoSatDto>> CreateBangKhaoSat([FromBody] CreateBangKhaoSatDto obj)
        {
            var command = new CreateBangKhaoSatCommand { BangKhaoSatDto = obj };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("UpdateBangKhaoSat")]
        public async Task<ActionResult<BangKhaoSatDto>> UpdateLoaiHinhDonVi([FromBody] BangKhaoSatDto obj)
        {
            var command = new UpdateBangKhaoSatCommand { BangKhaoSatDto = obj };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("DeleteBangKhaoSat/{id}")]
        public async Task<ActionResult<List<BangKhaoSatDto>>> DeleteLoaiHinhDonVi(int id)
        {
            var command = new DeleteBangKhaoSatCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
