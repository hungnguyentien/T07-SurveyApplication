using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Application.Contracts.Persistence;
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

        //[HttpPost("login")]
        //public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
        //{
        //    return Ok(await _authenticationService.Login(request));
        //}

        //[HttpPost("register")]
        //public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
        //{
        //    return Ok(await _authenticationService.Register(request));
        //}
    }
}
