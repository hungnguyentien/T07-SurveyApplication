using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Application.Features.CauHoi.Requests.Queries;

namespace SurveyApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuKhaoSatController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PhieuKhaoSatController(IMediator mediator)
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
