using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Application.DTOs.LinhVucHoatDong;
using SurveyApplication.Application.Features.LinhVucHoatDong.Requests.Queries;

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
            var rs = await _mediator.Send(new GetLinhVucHoatDongAllRequest());
            return Ok(rs);
        }
    }
}
