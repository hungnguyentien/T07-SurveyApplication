﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SurveyApplication.API.Attributes;
using SurveyApplication.API.Models;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Application.DTOs.PhieuKhaoSat;
using SurveyApplication.Application.Features.BaoCaoCauHoi.Requests.Commands;
using SurveyApplication.Application.Features.CauHoi.Requests.Queries;
using SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Commands;
using SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Queries;
using SurveyApplication.Domain.Common;
using SurveyApplication.Utility;

namespace SurveyApplication.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PhieuKhaoSatController : ControllerBase
{
    private const string PathJsonTinh = @"TempData\tinh_tp.json";
    private const string PathJsonQuanHuyen = @"TempData\quan_huyen.json";
    private const string PathJsonPhuongXa = @"TempData\xa_phuong.json";
    private readonly IMediator _mediator;

    public PhieuKhaoSatController(IMediator mediator, IOptions<EmailSettings> emailSettings)
    {
        _mediator = mediator;
        EmailSettings = emailSettings.Value;
    }

    private EmailSettings EmailSettings { get; }

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
            IdGuiEmail = thongTinChung.IdGuiEmail ?? 0
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
    
    [AllowAnonymous]
    [ValidSecretKey]
    [HttpPost("ScheduleSendEmail")]
    //[ApiExplorerSettings(IgnoreApi = true)]
    public async Task<ActionResult> ScheduleSendEmail()
    {
        var command = new ScheduleSendMailCommand();
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpGet("GetTinh")]
    public ActionResult GetTinh()
    {
        using var r = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), PathJsonTinh));
        var datas = JsonConvert.DeserializeObject<Dictionary<string, HanhChinhVn>>(r.ReadToEnd());
        return Ok(datas.Values);
    }

    [HttpGet("GetQuanHuyen")]
    public ActionResult GetQuanHuyen(string idTinh)
    {
        using var r = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), PathJsonQuanHuyen));
        var datas = JsonConvert.DeserializeObject<Dictionary<string, HanhChinhVn>>(r.ReadToEnd());
        var quanHuyen = datas.Values.Where(x => x.parent_code == idTinh);
        return Ok(quanHuyen);
    }

    [HttpGet("GetPhuongXa")]
    public ActionResult GetPhuongXa(string idQuanHuyen)
    {
        using var r = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), PathJsonPhuongXa));
        var datas = JsonConvert.DeserializeObject<Dictionary<string, HanhChinhVn>>(r.ReadToEnd());
        var phuongXa = datas.Values.Where(x => x.parent_code == idQuanHuyen);
        return Ok(phuongXa);
    }

    [HttpPost("DongBoBaoCaoCauHoi")]
    public async Task<ActionResult> DongBoBaoCaoCauHoi(CreateBaoCaoCauHoiCommand data)
    {
        var command = new CreateBaoCaoCauHoiCommand { LstBaoCaoCauHoi = data.LstBaoCaoCauHoi, IdGuiEmail = data.IdGuiEmail };
        var response = await _mediator.Send(command);
        return Ok(response);
    }
}