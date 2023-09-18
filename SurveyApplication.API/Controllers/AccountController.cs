using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.API.Models;
using SurveyApplication.Application.DTOs.Account;
using SurveyApplication.Application.Features.Accounts.Requests.Commands;
using SurveyApplication.Application.Features.Accounts.Requests.Queries;
using SurveyApplication.Domain.Common.Identity;
using SurveyApplication.Domain.Common.Responses;

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
        public async Task<ActionResult<BaseCommandResponse>> Register(RegisterCommand request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("GetByCondition")]
        //[HasPermission(new[] { (int)EnumModule.Code.QlKs }, new[] { (int)EnumPermission.Type.Read })]
        public async Task<ActionResult<BaseQuerieResponse<AccountDto>>> GetByCondition([FromQuery] Paging paging)
        {
            var response = await _mediator.Send(new GetUserConditionsRequest { PageIndex = paging.PageIndex, PageSize = paging.PageSize, Keyword = paging.Keyword, OrderBy = paging.OrderBy });
            return response;
        }

        [HttpPost("CreateRole")]
        public async Task<ActionResult<BaseCommandResponse>> CreateRole(RoleCommand role)
        {
            var response = await _mediator.Send(role);
            return response;
        }
    }
}
