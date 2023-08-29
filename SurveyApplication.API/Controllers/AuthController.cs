using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.Features.Auths.Requests.Queries;
using SurveyApplication.Application.Models.Identity;

namespace SurveyApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authenticationService;
        public AuthController(IAuthRepository authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
        {
            return Ok(await _authenticationService.Login(request));
        }
    }
}
