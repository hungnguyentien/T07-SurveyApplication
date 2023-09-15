using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.API.Models;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using SurveyApplication.Application.DTOs.NguoiDaiDien;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Commands;
using SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Commands;
using SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NguoiDaiDienController : ControllerBase
{
    private readonly IMediator _mediator;

    public NguoiDaiDienController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<List<NguoiDaiDienDto>>> GetAllNguoiDaiDien()
    {
        var leaveAllocations = await _mediator.Send(new GetNguoiDaiDienListRequest());
        return Ok(leaveAllocations);
    }

    [HttpGet("GetByCondition")]
    public async Task<ActionResult<BaseQuerieResponse<NguoiDaiDienDto>>> GetNguoiDaiDienByCondition(
        [FromQuery] Paging paging)
    {
        var leaveAllocations = await _mediator.Send(new GetNguoiDaiDienConditionsRequest
            { PageIndex = paging.PageIndex, PageSize = paging.PageSize, Keyword = paging.Keyword });
        return Ok(leaveAllocations);
    }

    [HttpGet("GetById/{id}")]
    public async Task<ActionResult<List<NguoiDaiDienDto>>> GetByNguoiDaiDien(int id)
    {
        var leaveAllocations = await _mediator.Send(new GetNguoiDaiDienDetailRequest { Id = id });
        return Ok(leaveAllocations);
    }

    [HttpPost("Create")]
    public async Task<ActionResult<NguoiDaiDienDto>> CreateNguoiDaiDien([FromBody] CreateNguoiDaiDienDto obj)
    {
        var command = new CreateNguoiDaiDienCommand { NguoiDaiDienDto = obj };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPost("Update")]
    public async Task<ActionResult<NguoiDaiDienDto>> UpdateNguoiDaiDien([FromBody] UpdateNguoiDaiDienDto obj)
    {
        var command = new UpdateNguoiDaiDienCommand { NguoiDaiDienDto = obj };
        await _mediator.Send(command);
        return Ok(new
        {
            Success = true
        });
    }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<List<NguoiDaiDienDto>>> DeleteNguoiDaiDien(int id)
        {
            var command = new DeleteNguoiDaiDienCommand { Ids = new List<int> { id } };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("DeleteMultiple")]
        public async Task<ActionResult> DeleteMultipleCauHoi(List<int> ids)
        {
            var command = new DeleteNguoiDaiDienCommand { Ids = ids };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
