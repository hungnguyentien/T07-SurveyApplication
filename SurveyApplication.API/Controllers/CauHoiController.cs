using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Application.DTOs.CauHoi;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Queries;
using SurveyApplication.Application.Features.CauHoi.Requests.Queries;

namespace SurveyApplication.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CauHoiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CauHoiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetConfigPhieuKhaoSat")]
        public async Task<ActionResult<List<CauHoiDto>>> GetConfigPhieuKhaoSat(int idBangKhaoSat)
        {
            var result = await _mediator.Send(new GetConfigCauHoiRequest { IdBangKhaoSat = idBangKhaoSat });
            return Ok(result);
        }
    }
}
