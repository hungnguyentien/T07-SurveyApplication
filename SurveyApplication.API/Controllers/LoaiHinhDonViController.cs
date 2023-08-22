using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Queries;

namespace SurveyApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class LoaiHinhDonViController : Controller
    {
        private readonly IMediator _mediator;

        public LoaiHinhDonViController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<LoaiHinhDonViController>
        [HttpGet("GetByLoaiHinhDonVi")]
        public async Task<ActionResult<List<LoaiHinhDonViDto>>> GetByLoaiHinhDonVi(string maLoaiHinh)
        {
            var leaveAllocations = await _mediator.Send(new GetLoaiHinhDonViDetailRequest() { MaLoaiHinh = maLoaiHinh });
            return Ok(leaveAllocations);
        }
    }
}
