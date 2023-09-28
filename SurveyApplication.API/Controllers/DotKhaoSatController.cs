using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.API.Attributes;
using SurveyApplication.API.Models;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Application.DTOs.DotKhaoSat;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Queries;
using SurveyApplication.Application.Features.DotKhaoSats.Requests.Commands;
using SurveyApplication.Application.Features.DotKhaoSats.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DotKhaoSatController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DotKhaoSatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        [HasPermission(new[] { (int)EnumModule.Code.QlDks, (int)EnumModule.Code.TkKs }, new[] { (int)EnumPermission.Type.Read })]
        public async Task<ActionResult<List<DotKhaoSatDto>>> GetAllBangKhaoSat()
        {
            var lstDks = await _mediator.Send(new GetDotKhaoSatListRequest());
            return Ok(lstDks);
        }

        [HttpGet("GetByCondition")]
        [HasPermission(new[] { (int)EnumModule.Code.QlDks }, new[] { (int)EnumPermission.Type.Read })]
        public async Task<ActionResult<BaseQuerieResponse<DotKhaoSatDto>>> GetBangKhaoSatByCondition(
            [FromQuery] Paging paging)
        {
            var leaveAllocations = await _mediator.Send(new GetDotKhaoSatConditionsRequest
            { PageIndex = paging.PageIndex, PageSize = paging.PageSize, Keyword = paging.Keyword });
            return leaveAllocations;
        }

        [HttpGet("GetById/{id}")]
        [HasPermission(new[] { (int)EnumModule.Code.QlDks }, new[] { (int)EnumPermission.Type.Read })]
        public async Task<ActionResult<List<DotKhaoSatDto>>> GetByBangKhaoSat(int id)
        {
            var leaveAllocations = await _mediator.Send(new GetDotKhaoSatDetailRequest { Id = id });
            return Ok(leaveAllocations);
        }

        [HttpGet("GetByLoaiHinh")]
        [HasPermission(new[] { (int)EnumModule.Code.QlKs }, new[] { (int)EnumPermission.Type.Read })]
        public async Task<ActionResult<List<DotKhaoSatDto>>> GetByDotKhaoSat(int id)
        {
            var leaveAllocations = await _mediator.Send(new GetDotKhaoSatByLoaiHinhRequest { Id = id });
            return Ok(leaveAllocations);
        }

        [HttpPost("Create")]
        [HasPermission(new[] { (int)EnumModule.Code.QlDks }, new[] { (int)EnumPermission.Type.Create })]
        public async Task<ActionResult<DotKhaoSatDto>> CreateBangKhaoSat([FromBody] CreateDotKhaoSatDto obj)
        {
            obj.TrangThai = 1;
            var command = new CreateDotKhaoSatCommand { DotKhaoSatDto = obj };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("Update")]
        [HasPermission(new[] { (int)EnumModule.Code.QlDks }, new[] { (int)EnumPermission.Type.Update })]
        public async Task<ActionResult<DotKhaoSatDto>> UpdateBangKhaoSat([FromBody] UpdateDotKhaoSatDto obj)
        {
            var command = new UpdateDotKhaoSatCommand { DotKhaoSatDto = obj };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("Delete/{id}")]
        [HasPermission(new[] { (int)EnumModule.Code.QlDks }, new[] { (int)EnumPermission.Type.Deleted })]
        public async Task<ActionResult> DeleteDotKhaoSat(int id)
        {
            var command = new DeleteDotKhaoSatCommand { Ids = new List<int> { id } };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("DeleteMultiple")]
        [HasPermission(new[] { (int)EnumModule.Code.QlDks }, new[] { (int)EnumPermission.Type.Deleted })]
        public async Task<ActionResult> DeleteMultipleDotKhaoSat(List<int> ids)
        {
            var command = new DeleteDotKhaoSatCommand { Ids = ids };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
