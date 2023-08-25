using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SurveyApplication.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CauHoiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CauHoiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return Ok();
        }
    }
}
