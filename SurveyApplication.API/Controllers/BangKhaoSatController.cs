using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.API.Attributes;
using SurveyApplication.API.Models;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Commands;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BangKhaoSatController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BangKhaoSatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        [HasPermission(new[] { (int)EnumModule.Code.QlKs, (int)EnumModule.Code.TkKs }, new[] { (int)EnumPermission.Type.Read })]
        public async Task<ActionResult<List<BangKhaoSatDto>>> GetAllBangKhaoSat()
        {
            var lstBangKhaoSat = await _mediator.Send(new GetBangKhaoSatListRequest());
            return Ok(lstBangKhaoSat);
        }

        [HttpGet("GetByCondition")]
        [HasPermission(new[] { (int)EnumModule.Code.QlKs }, new[] { (int)EnumPermission.Type.Read })]
        public async Task<ActionResult<BaseQuerieResponse<BangKhaoSatDto>>> GetByConditionBangKhaoSat([FromQuery] Paging paging)
        {
            var leaveAllocations = await _mediator.Send(new GetBangKhaoSatConditionsRequest { PageIndex = paging.PageIndex, PageSize = paging.PageSize, Keyword = paging.Keyword });
            return leaveAllocations;
        }

        [HttpGet("GetById/{id}")]
        [HasPermission(new[] { (int)EnumModule.Code.QlKs }, new[] { (int)EnumPermission.Type.Read })]
        public async Task<ActionResult<BangKhaoSatDto>> GetByIdBangKhaoSat(int id)
        {
            var leaveAllocations = await _mediator.Send(new GetBangKhaoSatDetailRequest { Id = id });
            return Ok(leaveAllocations);
        }

        [HttpGet("GetByDotKhaoSat/{id}")]
        [HasPermission(new[] { (int)EnumModule.Code.QlKs }, new[] { (int)EnumPermission.Type.Read })]
        public async Task<ActionResult<List<BangKhaoSatDto>>> GetByDotKhaoSat(int id)
        {
            var leaveAllocations = await _mediator.Send(new GetBangKhaoSatByDotKhaoSatRequest { Id = id });
            return Ok(leaveAllocations);
        }

        [HttpPost("Create")]
        [HasPermission(new[] { (int)EnumModule.Code.QlKs }, new[] { (int)EnumPermission.Type.Create })]
        public async Task<ActionResult<BangKhaoSatDto>> CreateBangKhaoSat([FromBody] CreateBangKhaoSatDto obj)
        {
            obj.TrangThai = 1;
            var command = new CreateBangKhaoSatCommand { BangKhaoSatDto = obj };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("Update")]
        [HasPermission(new[] { (int)EnumModule.Code.QlKs }, new[] { (int)EnumPermission.Type.Update })]
        public async Task<ActionResult<BangKhaoSatDto>> UpdateBangKhaoSat([FromBody] UpdateBangKhaoSatDto obj)
        {
            var command = new UpdateBangKhaoSatCommand { BangKhaoSatDto = obj };
            await _mediator.Send(command);
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpDelete("Delete/{id}")]
        [HasPermission(new[] { (int)EnumModule.Code.QlKs }, new[] { (int)EnumPermission.Type.Deleted })]
        public async Task<ActionResult<List<BangKhaoSatDto>>> DeleteBangKhaoSat(int id)
        {
            var command = new DeleteBangKhaoSatCommand { Ids = new List<int> { id } };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("DeleteMultiple")]
        [HasPermission(new[] { (int)EnumModule.Code.QlKs }, new[] { (int)EnumPermission.Type.Deleted })]
        public async Task<ActionResult> DeleteMultipleBangKhaoSat(List<int> ids)
        {
            var command = new DeleteBangKhaoSatCommand { Ids = ids };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
