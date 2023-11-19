using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.API.Attributes;
using SurveyApplication.API.Models;
using SurveyApplication.Application.DTOs.QuanHuyen;
using SurveyApplication.Application.DTOs.XaPhuong;
using SurveyApplication.Application.Features.XaPhuongs.Requests.Commands;
using SurveyApplication.Application.Features.XaPhuongs.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class XaPhuongController : ControllerBase
{
    private readonly IMediator _mediator;

    public XaPhuongController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetAll")]
    [HasPermission(new[] { (int)EnumModule.Code.QlPx }, new[] { (int)EnumPermission.Type.Read })]
    public async Task<ActionResult<List<XaPhuongDto>>> GetAllXaPhuong()
    {
        var leaveAllocations = await _mediator.Send(new GetXaPhuongListRequest());
        return Ok(leaveAllocations);
    }

    [HttpGet("GetByCondition")]
    [HasPermission(new[] { (int)EnumModule.Code.QlPx }, new[] { (int)EnumPermission.Type.Read })]
    public async Task<ActionResult<BaseQuerieResponse<XaPhuongDto>>> GetByConditionXaPhuong([FromQuery] Paging paging)
    {
        var leaveAllocations = await _mediator.Send(new GetXaPhuongConditionsRequest
            { PageIndex = paging.PageIndex, PageSize = paging.PageSize, Keyword = paging.Keyword });
        return leaveAllocations;
    }

    [HttpGet("GetById/{id}")]
    [HasPermission(new[] { (int)EnumModule.Code.QlPx }, new[] { (int)EnumPermission.Type.Read })]
    public async Task<ActionResult<List<XaPhuongDto>>> GetByIdXaPhuong(int id)
    {
        var leaveAllocations = await _mediator.Send(new GetXaPhuongDetailRequest { Id = id });
        return Ok(leaveAllocations);
    }

    [HttpGet("GetByQuanHuyen")]
    [HasPermission(new[] { (int)EnumModule.Code.QlPx }, new[] { (int)EnumPermission.Type.Read })]
    public async Task<ActionResult<List<QuanHuyenDto>>> GetByQuanHuyen(string id)
    {
        var leaveAllocations = await _mediator.Send(new GetXaPhuonByQuanHuyenRequest { Id = id });
        return Ok(leaveAllocations);
    }

    [HttpPost("Create")]
    [HasPermission(new[] { (int)EnumModule.Code.QlPx }, new[] { (int)EnumPermission.Type.Create })]
    public async Task<ActionResult<XaPhuongDto>> CreateXaPhuong([FromBody] CreateXaPhuongDto obj)
    {
        var command = new CreateXaPhuongCommand { XaPhuongDto = obj };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPost("Update")]
    [HasPermission(new[] { (int)EnumModule.Code.QlPx }, new[] { (int)EnumPermission.Type.Update })]
    public async Task<ActionResult<XaPhuongDto>> UpdateXaPhuong([FromBody] UpdateXaPhuongDto obj)
    {
        var command = new UpdateXaPhuongCommand { XaPhuongDto = obj };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("Delete/{id}")]
    [HasPermission(new[] { (int)EnumModule.Code.QlPx }, new[] { (int)EnumPermission.Type.Deleted })]
    public async Task<ActionResult<List<XaPhuongDto>>> DeleteXaPhuong(int id)
    {
        var command = new DeleteXaPhuongCommand { Ids = new List<int> { id } };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("DeleteMultiple")]
    [HasPermission(new[] { (int)EnumModule.Code.QlPx }, new[] { (int)EnumPermission.Type.Deleted })]
    public async Task<ActionResult> DeleteMultipleXaPhuong(List<int> ids)
    {
        var command = new DeleteXaPhuongCommand { Ids = ids };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPost("Import")]
    [HasPermission(new[] { (int)EnumModule.Code.QlPx }, new[] { (int)EnumPermission.Type.Import })]
    public async Task<IActionResult> ImportXaPhuong([FromForm] ImportXaPhuongDto obj)
    {
        var command = new ImportXaPhuongCommand { File = obj.File };
        var response = await _mediator.Send(command);
        return Ok(response);
    }
}