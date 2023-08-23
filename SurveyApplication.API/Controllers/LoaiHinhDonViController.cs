using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Commands;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Queries;
using SurveyApplication.Application.Responses;

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
        public async Task<ActionResult<List<LoaiHinhDonViDto>>> GetByMaLoaiHinhDonVi(string maLoaiHinh)
        {
            var result = await _mediator.Send(new GetLoaiHinhDonViDetailRequest { MaLoaiHinh = maLoaiHinh });
            return Ok(result);
        }

        // GET: api/<LoaiHinhDonViController>
        [HttpGet("GetByConditions")]
        public async Task<ActionResult<List<LoaiHinhDonViDto>>> GetByConditions(int pageIndex = 1, int pageSize = 10, string? keyword = "")
        {
            var result = await _mediator.Send(new GetLoaiHinhDonViConditionsRequest { PageIndex = pageIndex, PageSize = pageSize, Keyword = keyword });
            return Ok(result);
        }

        // POST api/<CreateLoaiHinhDonVi>
        [HttpPost("CreateLoaiHinhDonVi")]
        public async Task<ActionResult<BaseCommandResponse>> CreateLoaiHinhDonVi([FromBody] CreateLoaiHinhDonViDto loaiHinhDonVi)
        {
            var command = new CreateLoaiHinhDonViCommand { LoaiHinhDonViDto = loaiHinhDonVi };
            var repsonse = await _mediator.Send(command);
            return Ok(repsonse);
        }
    }
}
