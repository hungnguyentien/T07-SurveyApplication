using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Application.DTOs.LinhVucHoatDong;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using SurveyApplication.Application.Features.LinhVucHoatDong.Requests.Queries;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Queries;

namespace SurveyApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinhVucHoatDongController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LinhVucHoatDongController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<LinhVucHoatDongDto>>> GetAllLoaiHinhDonVi()
        {
            var rs = await _mediator.Send(new GetLinhVucHoatDongAllRequest());
            return Ok(rs);
        }
    }
}
