using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SurveyApplication.API.Models;
using SurveyApplication.Application.DTOs.QuanHuyen;
using SurveyApplication.Application.Features.QuanHuyens.Requests.Commands;
using SurveyApplication.Application.Features.QuanHuyens.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuanHuyenController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuanHuyenController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<QuanHuyenDto>>> GetAllQuanHuyen()
        {
            var leaveAllocations = await _mediator.Send(new GetQuanHuyenListRequest());
            return Ok(leaveAllocations);
        }

        [HttpGet("GetByCondition")]
        public async Task<ActionResult<BaseQuerieResponse<QuanHuyenDto>>> GetQuanHuyenByCondition([FromQuery] Paging paging)
        {
            var leaveAllocations = await _mediator.Send(new GetQuanHuyenConditionsRequest { PageIndex = paging.PageIndex, PageSize = paging.PageSize, Keyword = paging.Keyword });
            return leaveAllocations;
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<List<QuanHuyenDto>>> GetByQuanHuyen(int id)
        {
            var leaveAllocations = await _mediator.Send(new GetQuanHuyenDetailRequest { Id = id });
            return Ok(leaveAllocations);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<QuanHuyenDto>> CreateQuanHuyen([FromBody] CreateQuanHuyenDto obj)
        {
            var command = new CreateQuanHuyenCommand { QuanHuyenDto = obj };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("Update")]
        public async Task<ActionResult<QuanHuyenDto>> UpdateQuanHuyen([FromBody] UpdateQuanHuyenDto obj)
        {
            var command = new UpdateQuanHuyenCommand { QuanHuyenDto = obj };
            await _mediator.Send(command);
            return Ok(new
            {
                Success = true,
            });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<List<QuanHuyenDto>>> DeleteQuanHuyen(int id)
        {
            var command = new DeleteQuanHuyenCommand { Id = id };
            await _mediator.Send(command);
            return Ok(new
            {
                Success = true,
            });
        }

        //[HttpPost("Import")]
        //public async Task<IActionResult> ImportJsonFile([FromBody] IFormFile file)
        //{
        //    var command = new DeleteQuanHuyenCommand { Id = id };
        //    await _mediator.Send(command);
        //    return Ok(new
        //    {
        //        Success = true,
        //    });
        //}
    }
}
