using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.Features.DonVis.Requests.Commands;
using SurveyApplication.Application.Features.DonVis.Requests.Queries;

namespace SurveyApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonViController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DonViController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllDonVi")]
        public async Task<ActionResult<List<DonViDto>>> GetAllDonVi()
        {
            var leaveAllocations = await _mediator.Send(new GetDonViListRequest());
            return Ok(leaveAllocations);
        }

        [HttpGet("GetDonViByCondition")]
        public async Task<ActionResult<List<DonViDto>>> GetDonViByCondition(int pageIndex = 1, int pageSize = 5, string? keyword = "")
        {
            var leaveAllocations = await _mediator.Send(new GetDonViConditionsRequest { PageIndex = pageIndex, PageSize = pageSize, Keyword = keyword });
            return Ok(leaveAllocations);
        }

        [HttpGet("GetByDonVi/{id}")]
        public async Task<ActionResult<List<DonViDto>>> GetByDonVi(int id)
        {
            var leaveAllocations = await _mediator.Send(new GetDonViDetailRequest { Id = id });
            return Ok(leaveAllocations);
        }

        [HttpPost("CreateDonVi")]
        public async Task<ActionResult<DonViDto>> CreateDonVi([FromBody] CreateDonViDto obj)
        {
            var command = new CreateDonViCommand { DonViDto = obj };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("UpdateDonVi")]
        public async Task<ActionResult<DonViDto>> UpdateDonVi([FromBody] UpdateDonViDto obj)
        {
            var command = new UpdateDonViCommand { DonViDto = obj };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("DeleteDonVi/{id}")]
        public async Task<ActionResult<List<DonViDto>>> DeleteDonVi(int id)
        {
            var command = new DeleteDonViCommand { Id = id };
            await _mediator.Send(command);
            return Ok(new
            {
                success = true
            });
        }
    }
}
