using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.API.Models;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Commands;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;

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

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<LoaiHinhDonViDto>>> GetAllLoaiHinhDonVi()
        {
            var leaveAllocations = await _mediator.Send(new GetLoaiHinhDonViListRequest());
            return Ok(leaveAllocations);
        }

        [HttpGet("GenerateMaLoaiHinh")]
        public async Task<ActionResult<string>> GetLastRecordByMaLoaiHinh()
        {
            var record = await _mediator.Send(new GetLastRecordLoaiHinhDonViRequest());
            return Ok(new
            {
                MaLoaiHinh = record,
            });
        }

        [HttpGet("GetByCondition")]
        public async Task<ActionResult<BaseQuerieResponse<LoaiHinhDonViDto>>> GetLoaiHinhDonViByCondition([FromQuery] Paging paging)
        {
            var leaveAllocations = await _mediator.Send(new GetLoaiHinhDonViConditionsRequest { PageIndex = paging.PageIndex, PageSize = paging.PageSize, Keyword = paging.Keyword });
            return leaveAllocations;
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<List<LoaiHinhDonViDto>>> GetByLoaiHinhDonVi(int id)
        {
            var leaveAllocations = await _mediator.Send(new GetLoaiHinhDonViDetailRequest { Id = id });
            return Ok(leaveAllocations);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<LoaiHinhDonViDto>> CreateLoaiHinhDonVi([FromBody] CreateLoaiHinhDonViDto obj)
        {
            var command = new CreateLoaiHinhDonViCommand { LoaiHinhDonViDto = obj };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("Update")]
        public async Task<ActionResult<LoaiHinhDonViDto>> UpdateLoaiHinhDonVi([FromBody] UpdateLoaiHinhDonViDto obj)
        {
            var command = new UpdateLoaiHinhDonViCommand { LoaiHinhDonViDto = obj };
            await _mediator.Send(command);
            return Ok(new
            {
                Success = true,
            });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<List<LoaiHinhDonViDto>>> DeleteLoaiHinhDonVi(int id)
        {
            var command = new DeleteLoaiHinhDonViCommand { Id = id };
            await _mediator.Send(command);
            return Ok(new
            {
                Success = true,
            });
        }
    }
}
