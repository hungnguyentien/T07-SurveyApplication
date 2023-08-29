using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Commands;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Queries;

namespace SurveyApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiHinhDonViController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoaiHinhDonViController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllLoaiHinhDonVi")]
        public async Task<ActionResult<List<LoaiHinhDonViDto>>> GetAllLoaiHinhDonVi()
        {
            var leaveAllocations = await _mediator.Send(new GetLoaiHinhDonViListRequest());
            return Ok(leaveAllocations);
        }

        [HttpGet("GetLastRecordByMaLoaiHinh")]
        public async Task<ActionResult<string>> GetLastRecordByMaLoaiHinh()
        {
            var record = await _mediator.Send(new GetLastRecordLoaiHinhDonViRequest());
            return Ok(new
            {
                MaLoaiHinh = record,
            });
        }

        [HttpGet("GetLoaiHinhDonViByCondition")]
        public async Task<ActionResult<List<LoaiHinhDonViDto>>> GetLoaiHinhDonViByCondition(int pageIndex = 1, int pageSize = 5, string? keyword = "")
        {
            var leaveAllocations = await _mediator.Send(new GetLoaiHinhDonViConditionsRequest { PageIndex = pageIndex, PageSize = pageSize, Keyword = keyword });
            return Ok(leaveAllocations);
        }

        [HttpGet("GetByLoaiHinhDonVi/{id}")]
        public async Task<ActionResult<List<LoaiHinhDonViDto>>> GetByLoaiHinhDonVi(int id)
        {
            var leaveAllocations = await _mediator.Send(new GetLoaiHinhDonViDetailRequest { Id = id });
            return Ok(leaveAllocations);
        }

        [HttpPost("CreateLoaiHinhDonVi")]
        public async Task<ActionResult<LoaiHinhDonViDto>> CreateLoaiHinhDonVi([FromBody] CreateLoaiHinhDonViDto obj)
        {
            var command = new CreateLoaiHinhDonViCommand { LoaiHinhDonViDto = obj };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("UpdateLoaiHinhDonVi")]
        public async Task<ActionResult<LoaiHinhDonViDto>> UpdateLoaiHinhDonVi([FromBody] UpdateLoaiHinhDonViDto obj)
        {
            var command = new UpdateLoaiHinhDonViCommand { LoaiHinhDonViDto = obj };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("DeleteLoaiHinhDonVi/{id}")]
        public async Task<ActionResult<List<LoaiHinhDonViDto>>> DeleteLoaiHinhDonVi(int id)
        {
            var command = new DeleteLoaiHinhDonViCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
