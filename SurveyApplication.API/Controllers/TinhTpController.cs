using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.API.Models;
using SurveyApplication.Application.DTOs.TinhTp;
using SurveyApplication.Application.Features.TinhTps.Requests.Commands;
using SurveyApplication.Application.Features.TinhTps.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TinhTpController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TinhTpController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<TinhTpDto>>> GetAllTinhTp()
        {
            var leaveAllocations = await _mediator.Send(new GetTinhTpListRequest());
            return Ok(leaveAllocations);
        }

        [HttpGet("GetByCondition")]
        public async Task<ActionResult<BaseQuerieResponse<TinhTpDto>>> GetTinhTpByCondition([FromQuery] Paging paging)
        {
            var leaveAllocations = await _mediator.Send(new GetTinhTpConditionsRequest { PageIndex = paging.PageIndex, PageSize = paging.PageSize, Keyword = paging.Keyword });
            return leaveAllocations;
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<List<TinhTpDto>>> GetByTinhTp(int id)
        {
            var leaveAllocations = await _mediator.Send(new GetTinhTpDetailRequest { Id = id });
            return Ok(leaveAllocations);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<TinhTpDto>> CreateTinhTp([FromBody] CreateTinhTpDto obj)
        {
            var command = new CreateTinhTpCommand { TinhTpDto = obj };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("Update")]
        public async Task<ActionResult<TinhTpDto>> UpdateTinhTp([FromBody] UpdateTinhTpDto obj)
        {
            var command = new UpdateTinhTpCommand { TinhTpDto = obj };
            await _mediator.Send(command);
            return Ok(new
            {
                Success = true,
            });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<List<TinhTpDto>>> DeleteTinhTp(int id)
        {
            var command = new DeleteTinhTpCommand { Id = id };
            await _mediator.Send(command);
            return Ok(new
            {
                Success = true,
            });
        }
    }
}
