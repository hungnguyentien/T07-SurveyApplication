using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.API.Models;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.DTOs.DonViAndNguoiDaiDien;
using SurveyApplication.Application.Features.DonVis.Requests.Commands;
using SurveyApplication.Application.Features.DonVis.Requests.Queries;
using SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Commands;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonViController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DonViController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<DonViDto>>> GetAllDonVi()
        {
            var lstDv = await _mediator.Send(new GetDonViListRequest());
            return Ok(lstDv);
        }

        [HttpGet("GetByCondition")]
        public async Task<ActionResult<BaseQuerieResponse<DonViDto>>> GetByConditionDonVi([FromQuery] Paging paging)
        {
            var leaveAllocations = await _mediator.Send(new GetDonViConditionsRequest { PageIndex = paging.PageIndex, PageSize = paging.PageSize, Keyword = paging.Keyword });
            return leaveAllocations;
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<List<DonViDto>>> GetByIdDonVi(int id)
        {
            var leaveAllocations = await _mediator.Send(new GetDonViDetailRequest { Id = id });
            return Ok(leaveAllocations);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<DonViDto>> CreateDonVi([FromBody] CreateDonViAndNguoiDaiDienDto obj)
        {
            var command_1 = new CreateDonViCommand { DonViDto = obj.DonViDto };
            var response_1 = await _mediator.Send(command_1);
            obj.NguoiDaiDienDto.IdDonVi = response_1.Id;
            var command_2 = new CreateNguoiDaiDienCommand { NguoiDaiDienDto = obj.NguoiDaiDienDto };
            var response_2 = await _mediator.Send(command_2);
            return Ok(new
            {
                response_1 = response_1,
                response_2 = response_2,
            });
        }

        [HttpPost("Update")]
        public async Task<ActionResult<DonViDto>> UpdateDonVi([FromBody] UpdateDonViAndNguoiDaiDienDto obj)
            {
            var command_1 = new UpdateDonViCommand { DonViDto = obj.DonViDto };
            var response_1 = await _mediator.Send(command_1);

            var command_2 = new UpdateNguoiDaiDienCommand { NguoiDaiDienDto = obj.NguoiDaiDienDto };
            var response_2 = await _mediator.Send(command_2);

            return Ok(new
            {
                response_1 = response_1,
                response_2 = response_2,
            });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<List<DonViDto>>> DeleteDonVi(int id)
        {
            var command = new DeleteDonViCommand { Id = id };
            await _mediator.Send(command);
            return Ok(new
            {
                Success = true,
            });
        }
    }
}
