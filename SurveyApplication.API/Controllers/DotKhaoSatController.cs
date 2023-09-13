using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.API.Models;
using SurveyApplication.Application.DTOs.DotKhaoSat;
using SurveyApplication.Application.Features.DotKhaoSats.Requests.Commands;
using SurveyApplication.Application.Features.DotKhaoSats.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;

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

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<DotKhaoSatDto>>> GetAllDotKhaoSat()
        {
            var lstDks = await _mediator.Send(new GetDotKhaoSatListRequest());
            return Ok(lstDks);
        }

        [HttpGet("GetByCondition")]
        public async Task<ActionResult<BaseQuerieResponse<DotKhaoSatDto>>> GetByConditionDotKhaoSat([FromQuery] Paging paging)
        {
            var leaveAllocations = await _mediator.Send(new GetDotKhaoSatConditionsRequest { PageIndex = paging.PageIndex, PageSize = paging.PageSize, Keyword = paging.Keyword });
            return leaveAllocations;
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<List<DotKhaoSatDto>>> GetByIdDotKhaoSat(int id)
        {
            var leaveAllocations = await _mediator.Send(new GetDotKhaoSatDetailRequest { Id = id });
            return Ok(leaveAllocations);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<DotKhaoSatDto>> CreateDotKhaoSat([FromBody] CreateDotKhaoSatDto obj)
        {
            obj.TrangThai = 1;
            var command = new CreateDotKhaoSatCommand { DotKhaoSatDto = obj };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("Update")]
        public async Task<ActionResult<DotKhaoSatDto>> UpdateDotKhaoSat([FromBody] UpdateDotKhaoSatDto obj)
        {
            var command = new UpdateDotKhaoSatCommand { DotKhaoSatDto = obj };
            await _mediator.Send(command);
            return Ok(new
            {
                Success = true,
            });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<List<DotKhaoSatDto>>> DeleteDotKhaoSat(int id)
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
