using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Application.Features.Accounts.Requests.Queries;
using SurveyApplication.Domain.Common.Identity;

namespace SurveyApplication.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
    {
        return Ok(await _mediator.Send(request));
    }
}