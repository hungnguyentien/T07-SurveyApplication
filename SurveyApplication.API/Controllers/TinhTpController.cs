using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.API.Attributes;
using SurveyApplication.API.Models;
using SurveyApplication.Application.DTOs.QuanHuyen;
using SurveyApplication.Application.DTOs.TinhTp;
using SurveyApplication.Application.Features.QuanHuyens.Requests.Commands;
using SurveyApplication.Application.Features.TinhTps.Requests.Commands;
using SurveyApplication.Application.Features.TinhTps.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TinhTpController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TinhTpController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        [HasPermission(new[] { (int)EnumModule.Code.QlTt }, new[] { (int)EnumPermission.Type.Read })]
        public async Task<ActionResult<List<TinhTpDto>>> GetAllTinhTp()
        {
            var leaveAllocations = await _mediator.Send(new GetTinhTpListRequest());
            return Ok(leaveAllocations);
        }

        [HttpGet("GetByCondition")]
        [HasPermission(new[] { (int)EnumModule.Code.QlTt }, new[] { (int)EnumPermission.Type.Read })]
        public async Task<ActionResult<BaseQuerieResponse<TinhTpDto>>> GetByConditionTinhTp([FromQuery] Paging paging)
        {
            var leaveAllocations = await _mediator.Send(new GetTinhTpConditionsRequest { PageIndex = paging.PageIndex, PageSize = paging.PageSize, Keyword = paging.Keyword });
            return leaveAllocations;
        }

        [HttpGet("GetById/{id}")]
        [HasPermission(new[] { (int)EnumModule.Code.QlTt }, new[] { (int)EnumPermission.Type.Read })]
        public async Task<ActionResult<List<TinhTpDto>>> GetByIdTinhTp(int id)
        {
            var leaveAllocations = await _mediator.Send(new GetTinhTpDetailRequest { Id = id });
            return Ok(leaveAllocations);
        }

        [HttpPost("Create")]
        [HasPermission(new[] { (int)EnumModule.Code.QlTt }, new[] { (int)EnumPermission.Type.Create })]
        public async Task<ActionResult<TinhTpDto>> CreateTinhTp([FromBody] CreateTinhTpDto obj)
        {
            var command = new CreateTinhTpCommand { TinhTpDto = obj };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("Update")]
        [HasPermission(new[] { (int)EnumModule.Code.QlTt }, new[] { (int)EnumPermission.Type.Update })]
        public async Task<ActionResult<TinhTpDto>> UpdateTinhTp([FromBody] UpdateTinhTpDto obj)
        {
            var command = new UpdateTinhTpCommand { TinhTpDto = obj };
            await _mediator.Send(command);
            return Ok(new
            {
                Success = true,
            });
        }

        [HttpDelete("Delete/{id}")]
        [HasPermission(new[] { (int)EnumModule.Code.QlTt }, new[] { (int)EnumPermission.Type.Deleted })]
        public async Task<ActionResult<List<TinhTpDto>>> DeleteTinhTp(int id)
        {
            var command = new DeleteTinhTpCommand { Ids = new List<int> { id } };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("DeleteMultiple")]
        [HasPermission(new[] { (int)EnumModule.Code.QlTt }, new[] { (int)EnumPermission.Type.Deleted })]
        public async Task<ActionResult> DeleteMultipleTinhTp(List<int> ids)
        {
            var command = new DeleteTinhTpCommand { Ids = ids };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("Import")]
        [HasPermission(new[] { (int)EnumModule.Code.QlTt }, new[] { (int)EnumPermission.Type.Import })]
        public async Task<IActionResult> ImportTinhTp([FromForm] ImportTinhTpDto obj)
        {
            var command = new ImportTinhTpCommand { File = obj.File };
            await _mediator.Send(command);
            return Ok(new
            {
                Success = true,
            });
        }
    }
}
