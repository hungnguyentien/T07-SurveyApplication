using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SurveyApplication.API.Attributes;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Application.DTOs.LinhVucHoatDong;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using SurveyApplication.Application.DTOs.PhieuKhaoSat;
using SurveyApplication.Application.DTOs.StgFile;
using SurveyApplication.Application.Features.BaoCaoCauHoi.Requests.Commands;
using SurveyApplication.Application.Features.CauHoi.Requests.Queries;
using SurveyApplication.Application.Features.LinhVucHoatDong.Requests.Queries;
using SurveyApplication.Application.Features.LoaiHinhDonVi.Requests.Queries;
using SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Commands;
using SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Queries;
using SurveyApplication.Application.Features.QuanHuyens.Requests.Queries;
using SurveyApplication.Application.Features.TinhTps.Requests.Queries;
using SurveyApplication.Application.Features.XaPhuongs.Requests.Queries;
using SurveyApplication.Domain.Common.Configurations;
using SurveyApplication.Utility;
using SurveyApplication.Utility.LogUtils;

namespace SurveyApplication.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhieuKhaoSatController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILoggerManager _logger;

    public PhieuKhaoSatController(IMediator mediator, IOptions<EmailSettings> emailSettings, ILoggerManager logger)
    {
        _logger = logger;
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
        var command = new CreateBaoCaoCauHoiCommand
        { LstBaoCaoCauHoi = data.LstBaoCaoCauHoi, IdGuiEmail = data.IdGuiEmail };
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

    /// <summary>
    ///     Lấy chuỗi mã hóa gửi mail
    /// </summary>
    /// <param name="idGuiMail"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [ValidSecretKey]
    [HttpGet("GetKeyGuiMail")]
    public ActionResult GetKeyGuiMail(int idGuiMail)
    {
        var thongTinChung = new EmailThongTinChungDto
        {
            IdGuiEmail = idGuiMail
        };
        var result =
            $"\n {EmailSettings.LinkKhaoSat}{StringUltils.EncryptWithKey(JsonConvert.SerializeObject(thongTinChung), EmailSettings.SecretKey)}";
        return Ok(result);
    }

    [HttpPost("UploadFiles")]
    public async Task<ActionResult<List<StgFileDto>>> UploadFiles([FromForm] UploadFileDto uploadFileDto)
    {
        var command = new UploadFileCommand { UploadFileDto = uploadFileDto };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpGet("DownloadTemplateSurvey/{data}")]
    public async Task<ActionResult> DownloadTemplateSurvey(string data)
    {
        var result = await _mediator.Send(new DownloadPhieuRequest { Data = data });
        return Ok(result);
    }

    [HttpGet("SearchSurveyByDonVi/{keyword}")]
    public async Task<ActionResult> SearchSurveyByDonVi(string keyword)
    {
        var command = new GetPhieuKhaoSatRequest { Keyword = keyword, PageSize = 10 };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPost("UpdateDoanhNghiep")]
    public async Task<ActionResult> UpdateDoanhNghiep(UpdateDoanhNghiepDto doanhNghiepDto)
    {
        var command = new UpdateDoanhNghiepCommand { UpdateDoanhNghiepDto = doanhNghiepDto };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpGet("ConvertToCamelString")]
    public ActionResult ConvertToCamelString(string data)
    {
        return Ok(data.ConvertToCamelString());
    }

    [HttpPost("LogNhanMail")]
    public async Task<ActionResult> LogNhanMail(LogNhanMailDto logNhanMail)
    {
        if (!string.IsNullOrEmpty(logNhanMail.MaDoanhNghiep))
            _logger.LogInfo(JsonConvert.SerializeObject(logNhanMail));

        var command = new CreateLogNhanMailCommand { LogNhanMailDto = logNhanMail };
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
}