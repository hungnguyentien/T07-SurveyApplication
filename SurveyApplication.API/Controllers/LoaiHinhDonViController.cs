using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Commands;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Queries;

namespace SurveyApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class LoaiHinhDonViController : Controller
    {
        private readonly IMediator _mediator;

        public LoaiHinhDonViController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetByLoaiHinhDonVi/{id}")]
        public async Task<ActionResult<List<LoaiHinhDonViDto>>> GetByLoaiHinhDonVi(string id)
        {
            var leaveAllocations = await _mediator.Send(new GetLoaiHinhDonViDetailRequest() { MaLoaiHinh = id });
            return Ok(leaveAllocations);
        }

        [HttpGet("GetAllLoaiHinhDonVi")]
        public async Task<ActionResult<List<LoaiHinhDonViDto>>> GetAllLoaiHinhDonVi()
        {
            var leaveAllocations = await _mediator.Send(new GetLoaiHinhDonViListRequest());
            return Ok(leaveAllocations);
        }

        [HttpPost("CreateLoaiHinhDonVi")]
        public async Task<ActionResult<LoaiHinhDonViDto>> CreateLoaiHinhDonVi([FromBody] LoaiHinhDonViDto obj)
        {
            var command = new CreateLoaiHinhDonViCommand { LoaiHinhDonViDto = obj };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("UpdateLoaiHinhDonVi")]
        public async Task<ActionResult<LoaiHinhDonViDto>> UpdateLoaiHinhDonVi([FromBody] LoaiHinhDonViDto obj)
        {
            var command = new UpdateLoaiHinhDonViCommand { LoaiHinhDonViDto = obj };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("DeleteLoaiHinhDonVi/{id}")]
        public async Task<ActionResult<List<LoaiHinhDonViDto>>> DeleteLoaiHinhDonVi(string id)
        {
            var command = new DeleteLoaiHinhDonViCommand { MaLoaiHinh = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
