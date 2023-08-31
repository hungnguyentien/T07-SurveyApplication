using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Application.DTOs.LinhVucHoatDong;
using SurveyApplication.Application.Features.LinhVucHoatDongs.Requests.Queries;

namespace SurveyApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiHinhHoatDongController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoaiHinhHoatDongController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllLinhVucHoatDong")]
        public async Task<ActionResult<List<LinhVucHoatDongDto>>> GetAllLinhVucHoatDong()
        {
            var leaveAllocations = await _mediator.Send(new GetLinhVucHoatDongListRequest());
            return Ok(leaveAllocations);
        }
    }
}
