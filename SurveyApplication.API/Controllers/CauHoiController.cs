using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.API.Attributes;
using SurveyApplication.API.Models;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Application.Features.CauHoi.Requests.Commands;
using SurveyApplication.Application.Features.CauHoi.Requests.Queries;
using SurveyApplication.Application.Features.LoaiHinhDonVi.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Utility;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CauHoiController : ControllerBase
{
    private readonly IMediator _mediator;

    public CauHoiController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetByCondition")]
    [HasPermission(new[] { (int)EnumModule.Code.QlCh }, new[] { (int)EnumPermission.Type.Read })]
    public async Task<ActionResult<BaseQuerieResponse<CauHoiDto>>> GetCauHoiByCondition([FromQuery] Paging paging)
    {
        var result = await _mediator.Send(new GetCauHoiConditionsRequest
        {
            PageIndex = paging.PageIndex,
            PageSize = paging.PageSize,
            Keyword = paging.Keyword,
            OrderBy = paging.OrderBy
        });
        return Ok(result);
    }

    [HttpGet("GetById/{id}")]
    [HasPermission(new[] { (int)EnumModule.Code.QlCh }, new[] { (int)EnumPermission.Type.Read })]
    public async Task<ActionResult<CauHoiDto>> GetCauHoiById(int id)
    {
        var result = await _mediator.Send(new GetCauHoiDetailRequest { Id = id });
        return Ok(result);
    }

    [HttpGet("GetLoaiCauHoi")]
    [HasPermission(new[] { (int)EnumModule.Code.QlCh }, new[] { (int)EnumPermission.Type.Read })]
    public ActionResult GetLoaiCauHoi()
    {
        var result = EnumUltils.GetDescription<EnumCauHoi.Type>()
            .Select(x => new { text = x.Value, value = ((int)x.Key).ToString() });
        return Ok(result);
    }

    [HttpPost("GenerateMaCauTraLoi")]
    [HasPermission(new[] { (int)EnumModule.Code.QlCh }, new[] { (int)EnumPermission.Type.Read })]
    public async Task<ActionResult<string>> GetLastRecordByMaCauTraLoi([FromBody] GenCauHoiDto ojb)
    {
        var record = await _mediator.Send(new GetLastRecordCauTraLoiRequest { GenCauHoiDto = ojb });
        return Ok(new
        {
            MaCauTraLoi = record
        });
    }

    [HttpPost("GenerateMaHang")]
    [HasPermission(new[] { (int)EnumModule.Code.QlCh }, new[] { (int)EnumPermission.Type.Read })]
    public async Task<ActionResult<string>> GetLastRecordByMaHang([FromBody] GenCauHoiDto ojb)
    {
        var record = await _mediator.Send(new GetLastRecordHangRequest { GenCauHoiDto = ojb });
        return Ok(new
        {
            MaHang = record
        });
    }

    [HttpPost("GenerateMaCot")]
    [HasPermission(new[] { (int)EnumModule.Code.QlCh }, new[] { (int)EnumPermission.Type.Read })]
    public async Task<ActionResult<string>> GetLastRecordByMaCot([FromBody] GenCauHoiDto ojb)
    {
        var record = await _mediator.Send(new GetLastRecordCotRequest { GenCauHoiDto = ojb });
        return Ok(new
        {
            MaCot = record
        });
    }

    [HttpPost("Create")]
    [HasPermission(new[] { (int)EnumModule.Code.QlCh }, new[] { (int)EnumPermission.Type.Create })]
    public async Task<ActionResult> CreateCauHoi([FromBody] CreateCauHoiDto obj)
    {
        var command = new CreateCauHoiCommand { CauHoiDto = obj };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPost("Update")]
    [HasPermission(new[] { (int)EnumModule.Code.QlCh }, new[] { (int)EnumPermission.Type.Update })]
    public async Task<ActionResult> UpdateCauHoi([FromBody] UpdateCauHoiDto obj)
    {
        var command = new UpdateCauHoiCommand { CauHoiDto = obj };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("Delete/{id}")]
    [HasPermission(new[] { (int)EnumModule.Code.QlCh }, new[] { (int)EnumPermission.Type.Deleted })]
    public async Task<ActionResult> DeleteCauHoi(int id)
    {
        var command = new DeleteCauHoiCommand { Ids = new List<int> { id } };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("DeleteMultiple")]
    [HasPermission(new[] { (int)EnumModule.Code.QlCh }, new[] { (int)EnumPermission.Type.Deleted })]
    public async Task<ActionResult> DeleteMultipleCauHoi(List<int> ids)
    {
        var command = new DeleteCauHoiCommand { Ids = ids };
        var response = await _mediator.Send(command);
        return Ok(response);
    }
}