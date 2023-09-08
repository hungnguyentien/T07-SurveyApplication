using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.API.Models;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Commands;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;

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

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<BangKhaoSatDto>>> GetAllBangKhaoSat()
        {
            var lstBangKhaoSat = await _mediator.Send(new GetBangKhaoSatListRequest());
            return Ok(lstBangKhaoSat);
        }

        [HttpGet("GetByCondition")]
        public async Task<ActionResult<BaseQuerieResponse<BangKhaoSatDto>>> GetBangKhaoSatByCondition([FromQuery] Paging paging)
        {
            var leaveAllocations = await _mediator.Send(new GetBangKhaoSatConditionsRequest { PageIndex = paging.PageIndex, PageSize = paging.PageSize, Keyword = paging.Keyword });
            return leaveAllocations;
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<BangKhaoSatDto>> GetByBangKhaoSat(int id)
        {
            var leaveAllocations = await _mediator.Send(new GetBangKhaoSatDetailRequest { Id = id });
            return Ok(leaveAllocations);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<BangKhaoSatDto>> CreateBangKhaoSat([FromBody] CreateBangKhaoSatDto obj)
        {
            obj.TrangThai = 1;
            var command = new CreateBangKhaoSatCommand { BangKhaoSatDto = obj };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("Update")]
        public async Task<ActionResult<BangKhaoSatDto>> UpdateBangKhaoSat([FromBody] UpdateBangKhaoSatDto obj)
        {
            var command = new UpdateBangKhaoSatCommand { BangKhaoSatDto = obj };
            await _mediator.Send(command);
            return Ok(new
            {
                Success = true,
            });
        }


        [HttpDelete("Delete/{id}")]
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
