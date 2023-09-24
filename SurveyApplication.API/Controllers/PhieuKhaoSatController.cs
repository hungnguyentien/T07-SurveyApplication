using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SurveyApplication.API.Attributes;
using SurveyApplication.API.Models;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Application.DTOs.LinhVucHoatDong;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using SurveyApplication.Application.DTOs.PhieuKhaoSat;
using SurveyApplication.Application.Features.BaoCaoCauHoi.Requests.Commands;
using SurveyApplication.Application.Features.CauHoi.Requests.Queries;
using SurveyApplication.Application.Features.LinhVucHoatDong.Requests.Queries;
using SurveyApplication.Application.Features.LoaiHinhDonVi.Requests.Queries;
using SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Commands;
using SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Queries;
using SurveyApplication.Application.Features.QuanHuyens.Requests.Queries;
using SurveyApplication.Application.Features.TinhTps.Requests.Queries;
using SurveyApplication.Application.Features.XaPhuongs.Requests.Queries;
using SurveyApplication.Domain.Common;
using SurveyApplication.Utility;

namespace SurveyApplication.API.Controllers;

[ApiController]
[Route("api/[controller]")]
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
        var thongTinChung =
            JsonConvert.DeserializeObject<EmailThongTinChungDto>(
                StringUltils.DecryptWithKey(data, EmailSettings.SecretKey));
        var result = await _mediator.Send(new GetThongTinChungRequest { IdGuiEmail = thongTinChung?.IdGuiEmail ?? 0 });
        return Ok(result);
    }

    [HttpGet("GetConfigPhieuKhaoSat")]
    public async Task<ActionResult<PhieuKhaoSatDto>> GetConfigPhieuKhaoSat(string data)
    {
        var thongTinChung =
            JsonConvert.DeserializeObject<EmailThongTinChungDto>(
                StringUltils.DecryptWithKey(data, EmailSettings.SecretKey));
        var result = await _mediator.Send(new GetConfigCauHoiRequest
        {
            IdGuiEmail = thongTinChung?.IdGuiEmail ?? 0
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

    #region Tỉnh thành quận huyện

    [HttpGet("GetTinh")]
    public async Task<ActionResult> GetTinh()
    {
        var lstTinh = await _mediator.Send(new GetTinhTpListRequest());
        return Ok(lstTinh);
    }

    [HttpGet("GetQuanHuyen")]
    public async Task<ActionResult> GetQuanHuyen()
    {
        var quanHuyen = await _mediator.Send(new GetQuanHuyenListRequest());
        return Ok(quanHuyen);
    }

    [HttpGet("GetPhuongXa")]
    public async Task<ActionResult> GetPhuongXa()
    {
        var phuongXa = await _mediator.Send(new GetXaPhuongListRequest());
        return Ok(phuongXa);
    }

    #endregion

    [HttpGet("GetAllLoaiHinhDonVi")]
    public async Task<ActionResult<List<LoaiHinhDonViDto>>> GetAllLoaiHinhDonVi()
    {
        var leaveAllocations = await _mediator.Send(new GetLoaiHinhDonViListRequest());
        return Ok(leaveAllocations);
    }

    [HttpGet("GetAllLinhVucHoatDong")]
    public async Task<ActionResult<List<LinhVucHoatDongDto>>> GetAllLinhVucHoatDong()
    {
        var rs = await _mediator.Send(new GetLinhVucHoatDongListRequest());
        return Ok(rs);
    }

    [HttpPost("DongBoBaoCaoCauHoi")]
    public async Task<ActionResult> DongBoBaoCaoCauHoi(CreateBaoCaoCauHoiCommand data)
    {
        var command = new CreateBaoCaoCauHoiCommand { LstBaoCaoCauHoi = data.LstBaoCaoCauHoi, IdGuiEmail = data.IdGuiEmail };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [AllowAnonymous]
    [ValidSecretKey]
    [HttpPost("ScheduleSendEmail")]
    //[ApiExplorerSettings(IgnoreApi = true)]
    public async Task<ActionResult> ScheduleSendEmail()
    {
        var response = await _mediator.Send(new ScheduleSendMailCommand());
        return Ok(response);
    }

    [AllowAnonymous]
    [ValidSecretKey]
    [HttpPost("ScheduleUpdateStatus")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<ActionResult> ScheduleUpdateStatus()
    {
        var response = await _mediator.Send(new ScheduleUpdateStatusCommand());
        return Ok(response);
    }
}