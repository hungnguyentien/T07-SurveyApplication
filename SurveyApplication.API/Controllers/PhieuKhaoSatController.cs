using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Application.DTOs.PhieuKhaoSat;
using SurveyApplication.Application.Features.CauHoi.Requests.Queries;
using SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Commands;
using SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Queries;

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

        [HttpGet("GetThongTinChung")]
        public async Task<ActionResult<ThongTinChungDto>> GetThongTinChung(int idDonVi)
        {
            var result = await _mediator.Send(new GetThongTinChungRequest { IdDonVi = idDonVi });
            return Ok(result);
        }

        [HttpPost("SavePhieuKhaoSat")]
        public async Task<ActionResult> SavePhieuKhaoSat([FromBody] CreateKetQuaDto obj)
        {
            var command = new CreateKetQuaCommand { CreateKetQuaDto = obj };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

    }
}
