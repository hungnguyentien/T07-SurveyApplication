using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.API.Attributes;
using SurveyApplication.API.Models;
using SurveyApplication.Application.DTOs.LinhVucHoatDong;
using SurveyApplication.Application.Features.LinhVucHoatDong.Requests.Commands;
using SurveyApplication.Application.Features.LinhVucHoatDong.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.API.Controllers;

[Authorize]
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
    [HasPermission(new[] { (int)EnumModule.Code.LvHd }, new[] { (int)EnumPermission.Type.Read })]
    public async Task<ActionResult<List<LinhVucHoatDongDto>>> GetAllLinhVucHoatDong()
    {
        var rs = await _mediator.Send(new GetLinhVucHoatDongListRequest());
        return Ok(rs);
    }

    [HttpGet("GenerateMaLinhVuc")]
    public async Task<ActionResult<string>> GetLastRecordByMaLinhVuc()
    {
        var record = await _mediator.Send(new GetLastRecordLinhVucRequest());
        return Ok(new
        {
            MaLinhVuc = record
        });
    }

    [HttpGet("GetByCondition")]
    [HasPermission(new[] { (int)EnumModule.Code.LvHd }, new[] { (int)EnumPermission.Type.Read })]
    public async Task<ActionResult<BaseQuerieResponse<LinhVucHoatDongDto>>> GetByConditionLinhVucHoatDong(
        [FromQuery] Paging paging)
    {
        var leaveAllocations = await _mediator.Send(new GetLinhVucHoatDongConditionsRequest
            { PageIndex = paging.PageIndex, PageSize = paging.PageSize, Keyword = paging.Keyword });
        return leaveAllocations;
    }

    [HttpGet("GetById/{id}")]
    [HasPermission(new[] { (int)EnumModule.Code.LvHd }, new[] { (int)EnumPermission.Type.Read })]
    public async Task<ActionResult<List<LinhVucHoatDongDto>>> GetByIdLinhVucHoatDong(int id)
    {
        var leaveAllocations = await _mediator.Send(new GetLinhVucHoatDongDetailRequest { Id = id });
        return Ok(leaveAllocations);
    }

    [HttpPost("Create")]
    [HasPermission(new[] { (int)EnumModule.Code.LvHd }, new[] { (int)EnumPermission.Type.Create })]
    public async Task<ActionResult<LinhVucHoatDongDto>> CreateLinhVucHoatDong([FromBody] CreateLinhVucHoatDongDto obj)
    {
        var command = new CreateLinhVucHoatDongCommand { LinhVucHoatDongDto = obj };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPost("Update")]
    [HasPermission(new[] { (int)EnumModule.Code.LvHd }, new[] { (int)EnumPermission.Type.Update })]
    public async Task<ActionResult<LinhVucHoatDongDto>> UpdateLinhVucHoatDong([FromBody] UpdateLinhVucHoatDongDto obj)
    {
        var command = new UpdateLinhVucHoatDongCommand { LinhVucHoatDongDto = obj };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("Delete/{id}")]
    [HasPermission(new[] { (int)EnumModule.Code.LvHd }, new[] { (int)EnumPermission.Type.Deleted })]
    public async Task<ActionResult<List<LinhVucHoatDongDto>>> DeleteLinhVucHoatDong(int id)
    {
        var command = new DeleteLinhVucHoatDongCommand { Ids = new List<int> { id } };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("DeleteMultiple")]
    [HasPermission(new[] { (int)EnumModule.Code.LvHd }, new[] { (int)EnumPermission.Type.Deleted })]
    public async Task<ActionResult> DeleteMultipleLinhVucHoatDong(List<int> ids)
    {
        var command = new DeleteLinhVucHoatDongCommand { Ids = ids };
        var response = await _mediator.Send(command);
        return Ok(response);
    }
}