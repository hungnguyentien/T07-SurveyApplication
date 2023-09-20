using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.API.Attributes;
using SurveyApplication.API.Models;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.DTOs.DonViAndNguoiDaiDien;
using SurveyApplication.Application.Features.DonVis.Requests.Commands;
using SurveyApplication.Application.Features.DonVis.Requests.Queries;
using SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Commands;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.API.Controllers
{
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
            var leaveAllocations = await _mediator.Send(new GetDonViConditionsRequest { PageIndex = paging.PageIndex, PageSize = paging.PageSize, Keyword = paging.Keyword });
            return leaveAllocations;
        }

        [HttpGet("GetById/{id}")]
        [HasPermission(new[] { (int)EnumModule.Code.QlDv }, new[] { (int)EnumPermission.Type.Read })]
        public async Task<ActionResult<List<DonViDto>>> GetByIdDonVi(int id)
        {
            var leaveAllocations = await _mediator.Send(new GetDonViDetailRequest { Id = id });
            return Ok(leaveAllocations);
        }

        [HttpPost("Create")]
        [HasPermission(new[] { (int)EnumModule.Code.QlDv }, new[] { (int)EnumPermission.Type.Create })]
        public async Task<ActionResult<DonViDto>> CreateDonVi([FromBody] CreateDonViAndNguoiDaiDienDto obj)
        {
            var command_1 = new CreateDonViCommand { DonViDto = obj.DonViDto };
            var response_1 = await _mediator.Send(command_1);
            obj.NguoiDaiDienDto.IdDonVi = response_1.Id;
            var command_2 = new CreateNguoiDaiDienCommand { NguoiDaiDienDto = obj.NguoiDaiDienDto };
            var response_2 = await _mediator.Send(command_2);
            return Ok(new
            {
                response_1 = response_1,
                response_2 = response_2,
            });
        }

        [HttpPost("Update")]
        [HasPermission(new[] { (int)EnumModule.Code.QlDv }, new[] { (int)EnumPermission.Type.Update })]
        public async Task<ActionResult<DonViDto>> UpdateDonVi([FromBody] UpdateDonViAndNguoiDaiDienDto obj)
        {
            var command_1 = new UpdateDonViCommand { DonViDto = obj.DonViDto };
            var response_1 = await _mediator.Send(command_1);

            var command_2 = new UpdateNguoiDaiDienCommand { NguoiDaiDienDto = obj.NguoiDaiDienDto };
            var response_2 = await _mediator.Send(command_2);

            return Ok(new
            {
                response_1 = response_1,
                response_2 = response_2,
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
    }
}
