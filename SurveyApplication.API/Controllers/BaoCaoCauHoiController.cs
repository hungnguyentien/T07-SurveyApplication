using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Application.DTOs.BaoCaoCauHoi;
using SurveyApplication.Application.Features.BaoCaoCauHoi.Requests.Queries;

namespace SurveyApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaoCaoCauHoiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BaoCaoCauHoiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetBaoCaoCauHoi")]
        public async Task<ActionResult<BaoCaoCauHoiDto>> GetBaoCaoCauHoi([FromQuery] GetBaoCaoCauHoiRequest data)
        {
            var result = await _mediator.Send(data);
            return Ok(result);
        }

        [HttpGet("GetDashBoard")]
        public async Task<ActionResult<DashBoardDto>> GetDashBoard([FromQuery] GetDashBoardRequest data)
        {
            var result = await _mediator.Send(data);
            return Ok(result);
        }
    }
}
