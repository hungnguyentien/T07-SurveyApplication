using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Application.DTOs.NguoiDaiDien;
using SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Commands;
using SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Queries;

namespace SurveyApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NguoiDaiDienController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NguoiDaiDienController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllNguoiDaiDien")]
        public async Task<ActionResult<List<NguoiDaiDienDto>>> GetAllNguoiDaiDien()
        {
            var leaveAllocations = await _mediator.Send(new GetNguoiDaiDienListRequest());
            return Ok(leaveAllocations);
        }

        [HttpGet("GetNguoiDaiDienByCondition")]
        public async Task<ActionResult<List<NguoiDaiDienDto>>> GetNguoiDaiDienByCondition(int pageIndex = 1, int pageSize = 5, string? keyword = "")
        {
            var leaveAllocations = await _mediator.Send(new GetNguoiDaiDienConditionsRequest { PageIndex = pageIndex, PageSize = pageSize, Keyword = keyword });
            return Ok(leaveAllocations);
        }

        [HttpGet("GetByNguoiDaiDien/{id}")]
        public async Task<ActionResult<List<NguoiDaiDienDto>>> GetByNguoiDaiDien(int id)
        {
            var leaveAllocations = await _mediator.Send(new GetNguoiDaiDienDetailRequest { Id = id });
            return Ok(leaveAllocations);
        }

        [HttpPost("CreateNguoiDaiDien")]
        public async Task<ActionResult<NguoiDaiDienDto>> CreateNguoiDaiDien([FromBody] CreateNguoiDaiDienDto obj)
        {
            var command = new CreateNguoiDaiDienCommand { NguoiDaiDienDto = obj };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("UpdateNguoiDaiDien")]
        public async Task<ActionResult<NguoiDaiDienDto>> UpdateNguoiDaiDien([FromBody] UpdateNguoiDaiDienDto obj)
        {
            var command = new UpdateNguoiDaiDienCommand { NguoiDaiDienDto = obj };
            await _mediator.Send(command);
            return Ok(new
            {
                Success = true,
            });
        }

        [HttpDelete("DeleteNguoiDaiDien/{id}")]
        public async Task<ActionResult<List<NguoiDaiDienDto>>> DeleteNguoiDaiDien(int id)
        {
            var command = new DeleteNguoiDaiDienCommand { Id = id };
            await _mediator.Send(command);
            return Ok(new
            {
                Success = true,
            });
        }
    }
}
