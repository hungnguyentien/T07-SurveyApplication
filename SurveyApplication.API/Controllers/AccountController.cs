using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.API.Attributes;
using SurveyApplication.API.Models;
using SurveyApplication.Application.DTOs.Account;
using SurveyApplication.Application.Features.Accounts.Requests.Commands;
using SurveyApplication.Application.Features.Accounts.Requests.Queries;
using SurveyApplication.Domain.Common.Identity;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost("register")]
        [HasPermission(new[] { (int)EnumModule.Code.QlTk }, new[] { (int)EnumPermission.Type.Create })]
        public async Task<ActionResult<BaseCommandResponse>> Register([FromForm] RegisterDto request)
        {
            return Ok(await _mediator.Send(new RegisterCommand{Register = request} ));
        }

        [HttpGet("GetByCondition")]
        [HasPermission(new[] { (int)EnumModule.Code.QlTk }, new[] { (int)EnumPermission.Type.Read })]
        public async Task<ActionResult<BaseQuerieResponse<AccountDto>>> GetByCondition([FromQuery] Paging paging)
        {
            var response = await _mediator.Send(new GetAccountConditionsRequest { PageIndex = paging.PageIndex, PageSize = paging.PageSize, Keyword = paging.Keyword, OrderBy = paging.OrderBy });
            return response;
        }


        [HttpGet("GetById/{id}")]
        [HasPermission(new[] { (int)EnumModule.Code.QlTk }, new[] { (int)EnumPermission.Type.Read })]
        public async Task<ActionResult<List<AccountDto>>> GetByAccount(string id)
        {
            var leaveAllocations = await _mediator.Send(new GetAccountDetailRequest { Id = id });
            return Ok(leaveAllocations);
        }

        [HttpGet("ForgotPassword/{email}")]
        [HasPermission(new[] { (int)EnumModule.Code.QlTk }, new[] { (int)EnumPermission.Type.Read })]
        public async Task<ActionResult<ForgotPasswordDto>> ForgotPassword(string email)
        {
            var leaveAllocations = await _mediator.Send(new GetForgotPasswordRequest { Email = email });
            return Ok(leaveAllocations);
        }

        [HttpPost("Update")]
        [HasPermission(new[] { (int)EnumModule.Code.QlTk }, new[] { (int)EnumPermission.Type.Update })]
        public async Task<ActionResult<AccountDto>> UpdateAccount([FromForm] UpdateAccountDto obj)
        {
            var command = new UpdateAccountCommand { AccountDto = obj };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("Delete/{id}")]
        [HasPermission(new[] { (int)EnumModule.Code.QlTk }, new[] { (int)EnumPermission.Type.Deleted })]
        public async Task<ActionResult> DeleteAccount(string id)
        {
            var command = new DeleteAccountCommand { Ids = new List<string> { id } };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("DeleteMultiple")]
        [HasPermission(new[] { (int)EnumModule.Code.QlTk }, new[] { (int)EnumPermission.Type.Deleted })]
        public async Task<ActionResult> DeleteMultipleAccount(List<string> ids)
        {
            var command = new DeleteAccountCommand { Ids = ids };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
