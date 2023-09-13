using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.API.Models;
using SurveyApplication.Application.DTOs.LinhVucHoatDong;
using SurveyApplication.Application.Features.LinhVucHoatDong.Requests.Commands;
using SurveyApplication.Application.Features.LinhVucHoatDong.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinhVucHoatDongController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LinhVucHoatDongController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<LinhVucHoatDongDto>>> GetAllLinhVucHoatDong()
        {
            var rs = await _mediator.Send(new GetLinhVucHoatDongListRequest());
            return Ok(rs);
        }

        [HttpGet("GetByCondition")]
        public async Task<ActionResult<BaseQuerieResponse<LinhVucHoatDongDto>>> GetByConditionLinhVucHoatDong([FromQuery] Paging paging)
        {
            var leaveAllocations = await _mediator.Send(new GetLinhVucHoatDongConditionsRequest { PageIndex = paging.PageIndex, PageSize = paging.PageSize, Keyword = paging.Keyword });
            return leaveAllocations;
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<List<LinhVucHoatDongDto>>> GetByIdLinhVucHoatDong(int id)
        {
            var leaveAllocations = await _mediator.Send(new GetLinhVucHoatDongDetailRequest { Id = id });
            return Ok(leaveAllocations);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<LinhVucHoatDongDto>> CreateLinhVucHoatDong([FromBody] CreateLinhVucHoatDongDto obj)
        {
            var command = new CreateLinhVucHoatDongCommand { LinhVucHoatDongDto = obj };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("Update")]
        public async Task<ActionResult<LinhVucHoatDongDto>> UpdateLinhVucHoatDong([FromBody] UpdateLinhVucHoatDongDto obj)
        {
            var command = new UpdateLinhVucHoatDongCommand { LinhVucHoatDongDto = obj };
            await _mediator.Send(command);
            return Ok(new
            {
                Success = true,
            });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<List<LinhVucHoatDongDto>>> DeleteLinhVucHoatDong(int id)
        {
            var command = new DeleteLinhVucHoatDongCommand { Id = id };
            await _mediator.Send(command);
            return Ok(new
            {
                Success = true,
            });
        }
    }
}
