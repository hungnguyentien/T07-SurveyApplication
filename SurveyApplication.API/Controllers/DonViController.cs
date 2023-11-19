using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.API.Attributes;
using SurveyApplication.API.Models;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.DTOs.DonVi.Validators;
using SurveyApplication.Application.DTOs.DonViAndNguoiDaiDien;
using SurveyApplication.Application.Features.DonVis.Requests.Commands;
using SurveyApplication.Application.Features.DonVis.Requests.Queries;
using SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Commands;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DonViController : ControllerBase
{
    private readonly IMediator _mediator;

    public DonViController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetAll")]
    [HasPermission(new[] { (int)EnumModule.Code.QlDv }, new[] { (int)EnumPermission.Type.Read })]
    public async Task<ActionResult<List<DonViDto>>> GetAllDonVi()
    {
        var lstDv = await _mediator.Send(new GetDonViListRequest());
        return Ok(lstDv);
    }

    [HttpGet("GetByCondition")]
    [HasPermission(new[] { (int)EnumModule.Code.QlDv }, new[] { (int)EnumPermission.Type.Read })]
    public async Task<ActionResult<BaseQuerieResponse<DonViDto>>> GetByConditionDonVi([FromQuery] Paging paging)
    {
        var leaveAllocations = await _mediator.Send(new GetDonViConditionsRequest
            { PageIndex = paging.PageIndex, PageSize = paging.PageSize, Keyword = paging.Keyword });
        return leaveAllocations;
    }

    [HttpGet("GetById/{id}")]
    [HasPermission(new[] { (int)EnumModule.Code.QlDv }, new[] { (int)EnumPermission.Type.Read })]
    public async Task<ActionResult<List<DonViDto>>> GetByIdDonVi(int id)
    {
        var lstDonVi = await _mediator.Send(new GetDonViDetailRequest { Id = id });
        return Ok(lstDonVi);
    }

    [HttpPost("Create")]
    [HasPermission(new[] { (int)EnumModule.Code.QlDv }, new[] { (int)EnumPermission.Type.Create })]
    public async Task<ActionResult<DonViDto>> CreateDonVi([FromBody] CreateDonViAndNguoiDaiDienDto obj)
    {
        var command1 = new CreateDonViCommand { DonViDto = obj.DonViDto };
        var response1 = await _mediator.Send(command1);
        Debug.Assert(obj.NguoiDaiDienDto != null, "obj.NguoiDaiDienDto != null");
        obj.NguoiDaiDienDto.IdDonVi = response1.Id;
        var command2 = new CreateNguoiDaiDienCommand { NguoiDaiDienDto = obj.NguoiDaiDienDto };
        var response2 = await _mediator.Send(command2);
        return Ok(new
        {
            response_1 = response1,
            response_2 = response2
        });
    }

    [HttpPost("Update")]
    [HasPermission(new[] { (int)EnumModule.Code.QlDv }, new[] { (int)EnumPermission.Type.Update })]
    public async Task<ActionResult<DonViDto>> UpdateDonVi([FromBody] UpdateDonViAndNguoiDaiDienDto obj)
    {
        var command1 = new UpdateDonViCommand { DonViDto = obj.DonViDto };
        var response1 = await _mediator.Send(command1);

        var command2 = new UpdateNguoiDaiDienCommand { NguoiDaiDienDto = obj.NguoiDaiDienDto };
        var response2 = await _mediator.Send(command2);

        return Ok(new
        {
            response_1 = response1,
            response_2 = response2
        });
    }

    [HttpDelete("Delete/{id}")]
    [HasPermission(new[] { (int)EnumModule.Code.QlDv }, new[] { (int)EnumPermission.Type.Deleted })]
    public async Task<ActionResult<List<DonViDto>>> DeleteDonVi(int id)
    {
        var command = new DeleteDonViCommand { Ids = new List<int> { id } };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("DeleteMultiple")]
    [HasPermission(new[] { (int)EnumModule.Code.QlDv }, new[] { (int)EnumPermission.Type.Deleted })]
    public async Task<ActionResult> DeleteMultipleDonVi(List<int> ids)
    {
        var command = new DeleteDonViCommand { Ids = ids };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPost("Import")]
    [HasPermission(new[] { (int)EnumModule.Code.QlDv }, new[] { (int)EnumPermission.Type.Import })]
    public async Task<IActionResult> ImportDonVi([FromForm] ImportDonViDto obj)
    {
        var command = new ImportDonViCommand { File = obj.File };
        var response = await _mediator.Send(command);
        return Ok(response);
    }
}