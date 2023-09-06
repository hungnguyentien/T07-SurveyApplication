using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Application.DTOs.PhieuKhaoSat;
using SurveyApplication.Application.Features.CauHoi.Requests.Queries;
using SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Commands;
using SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Queries;
using SurveyApplication.Domain.Common;
using SurveyApplication.Utility;

namespace SurveyApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuKhaoSatController : ControllerBase
    {
        private readonly IMediator _mediator;
        private EmailSettings EmailSettings { get; }

        public PhieuKhaoSatController(IMediator mediator, IOptions<EmailSettings> emailSettings)
        {
            _mediator = mediator;
            EmailSettings = emailSettings.Value;
        }

        [HttpGet("GetThongTinChung")]
        public async Task<ActionResult<ThongTinChungDto>> GetThongTinChung(string data)
        {
            var thongTinChung = JsonConvert.DeserializeObject<EmailThongTinChungDto>(StringUltils.DecryptWithKey(data, EmailSettings.SecretKey));
            var result = await _mediator.Send(new GetThongTinChungRequest { IdDonVi = thongTinChung.IdDonVi ?? 0, IdBangKhaoSat = thongTinChung.IdBangKhaoSat ?? 0 });
            return Ok(result);
        }

        [HttpGet("GetConfigPhieuKhaoSat")]
        public async Task<ActionResult<PhieuKhaoSatDto>> GetConfigPhieuKhaoSat(int idBangKhaoSat, int idDonVi, int idNguoiKhaoSat)
        {
            var result = await _mediator.Send(new GetConfigCauHoiRequest
            {
                IdBangKhaoSat = idBangKhaoSat,
                IdDonVi = idDonVi,
                IdNguoiDaiDien = idNguoiKhaoSat
            });
            return Ok(result);
        }

        [HttpPost("SavePhieuKhaoSat")]
        public async Task<ActionResult> SavePhieuKhaoSat([FromBody] CreateKetQuaDto obj)
        {
            var command = new CreateKetQuaCommand { CreateKetQuaDto = obj };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("SendEmail")]
        public async Task<ActionResult> SendEmail(int id)
        {
            var command = new SendMailCommand { Id = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

    }
}
