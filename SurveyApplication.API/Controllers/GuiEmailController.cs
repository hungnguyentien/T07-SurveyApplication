using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.API.Attributes;
using SurveyApplication.API.Models;
using SurveyApplication.Application.DTOs.GuiEmail;
using SurveyApplication.Application.Features.GuiEmail.Requests.Commands;
using SurveyApplication.Application.Features.GuiEmail.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GuiEmailController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GuiEmailController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        [HasPermission(new[] { (int)EnumModule.Code.QlGm }, new[] { (int)EnumPermission.Type.Read })]
        public async Task<ActionResult<List<GuiEmailDto>>> GetAllGuiEmail()
        {
            var leaveAllocations = await _mediator.Send(new GetGuiEmailListRequest());
            return Ok(leaveAllocations);
        }

        [HttpGet("GetByCondition")]
        [HasPermission(new[] { (int)EnumModule.Code.QlGm }, new[] { (int)EnumPermission.Type.Read })]
        public async Task<ActionResult<List<GuiEmailBksDto>>> GetGuiEmailByCondition([FromQuery] Paging paging)
        {
            var lstGuiMail = await _mediator.Send(new GetGuiEmailBksConditionsRequest { PageIndex = paging.PageIndex, PageSize = paging.PageSize, Keyword = paging.Keyword });
            return Ok(lstGuiMail);
        }

        [HttpGet("GetByIdBangKhaoSat")]
        [HasPermission(new[] { (int)EnumModule.Code.QlGm }, new[] { (int)EnumPermission.Type.Read })]
        public async Task<ActionResult<List<GuiEmailDto>>> GetByIdBangKhaoSat([FromQuery] PagingGuiEmailBks paging)
        {
            var leaveAllocations = await _mediator.Send(new GetGuiEmailBksDetailRequest
            {
                PageIndex = paging.PageIndex,
                PageSize = paging.PageSize,
                IdBanhgKhaoSat = paging.IdBanhgKhaoSat,
                TrangThaiGuiEmail = paging.TrangThaiGuiEmail,
            });
            return Ok(leaveAllocations);
        }

        [HttpGet("GetById/{id}")]
        [HasPermission(new[] { (int)EnumModule.Code.QlGm }, new[] { (int)EnumPermission.Type.Read })]
        public async Task<ActionResult<List<GuiEmailDto>>> GetByGuiEmail(int id)
        {
            var leaveAllocations = await _mediator.Send(new GetGuiEmailDetailRequest { Id = id });
            return Ok(leaveAllocations);
        }

        [HttpPost("Create")]
        [HasPermission(new[] { (int)EnumModule.Code.QlGm }, new[] { (int)EnumPermission.Type.Create })]
        public async Task<ActionResult<GuiEmailDto>> CreateGuiEmail([FromBody] CreateGuiEmailDto obj)
        {
            var command = new CreateGuiEmailCommand { GuiEmailDto = obj };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("Update")]
        [HasPermission(new[] { (int)EnumModule.Code.QlGm }, new[] { (int)EnumPermission.Type.Update })]
        public async Task<ActionResult<GuiEmailDto>> UpdateGuiEmail([FromBody] UpdateGuiEmailDto obj)
        {
            var command = new UpdateGuiEmailCommand { GuiEmailDto = obj };
            await _mediator.Send(command);
            return Ok(new
            {
                Success = true,
            });
        }

        [HttpDelete("Delete/{id}")]
        [HasPermission(new[] { (int)EnumModule.Code.QlGm }, new[] { (int)EnumPermission.Type.Deleted })]
        public async Task<ActionResult<List<GuiEmailDto>>> DeleteGuiEmail(int id)
        {
            var command = new DeleteGuiEmailCommand { Id = id };
            await _mediator.Send(command);
            return Ok(new
            {
                Success = true,
            });
        }

        [HttpPost("CreateByDonVi")]
        [HasPermission(new[] { (int)EnumModule.Code.QlGm }, new[] { (int)EnumPermission.Type.Create })]
        public async Task<ActionResult<BaseCommandResponse>> CreateByDonVi([FromBody] CreateGuiEmailDto obj)
        {
            var command = new SendKhaoSatCommand { GuiEmailDto = obj };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("GuiLaiEmail")]
        [HasPermission(new[] { (int)EnumModule.Code.QlGm }, new[] { (int)EnumPermission.Type.Update })]
        public async Task<ActionResult<BaseCommandResponse>> GuiLaiEmail([FromBody] GuiLaiGuiEmailCommand obj)
        {
            var command = new GuiLaiGuiEmailCommand { GuiEmailDto = obj.GuiEmailDto, LstIdGuiMail = obj.LstIdGuiMail };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("ThuHoiEmail")]
        [HasPermission(new[] { (int)EnumModule.Code.QlGm }, new[] { (int)EnumPermission.Type.Update })]
        public async Task<ActionResult<BaseCommandResponse>> ThuHoiEmail([FromBody] ThuHoiGuiEmailCommand obj)
        {
            var response = await _mediator.Send(obj);
            return Ok(response);
        }
    }
}
