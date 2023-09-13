using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Application.DTOs.Account;
using SurveyApplication.Application.Features.Accounts.Requests.Commands;
using SurveyApplication.Application.Features.Accounts.Requests.Queries;
using SurveyApplication.Domain.Common.Identity;

namespace SurveyApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<AuthResponse>> AccountLogin([FromForm] LoginRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost("AccountRegister")]
        public async Task<ActionResult<RegistrationResponse>> AccountRegister(RegistrationCommand request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
