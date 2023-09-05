using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.DTOs.DonViAndNguoiDaiDien;
using SurveyApplication.Application.Features.DonVis.Requests.Commands;
using SurveyApplication.Application.Features.DonVis.Requests.Queries;
using SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Commands;
using SurveyApplication.Application.Responses;

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

        [HttpGet("GetAllDonVi")]
        public async Task<ActionResult<List<DonViDto>>> GetAllDonVi()
        {
            var leaveAllocations = await _mediator.Send(new GetDonViListRequest());
            return Ok(leaveAllocations);
        }

        [HttpGet("GetDonViByCondition")]
        public async Task<ActionResult<PageCommandResponse<DonViDto>>> GetDonViByCondition(int pageIndex = 1, int pageSize = 5, string? keyword = "")
        {
            keyword ??= "";

            var leaveAllocations = await _mediator.Send(new GetDonViConditionsRequest { PageIndex = pageIndex, PageSize = pageSize, Keyword = keyword });
            return leaveAllocations;
        }

        [HttpGet("GetByDonVi/{id}")]
        public async Task<ActionResult<List<DonViDto>>> GetByDonVi(int id)
        {
            var leaveAllocations = await _mediator.Send(new GetDonViDetailRequest { Id = id });
            return Ok(leaveAllocations);
        }

        [HttpPost("CreateDonVi")]
        public async Task<ActionResult<DonViDto>> CreateDonVi([FromBody] CreateDonViAndNguoiDaiDienDto obj)
        {
            obj.DonViDto.MaDonVi = Guid.NewGuid();
            var command_1 = new CreateDonViCommand { DonViDto = obj.DonViDto };
            var response_1 = await _mediator.Send(command_1);

            obj.NguoiDaiDienDto.MaDonVi = response_1.Id;
            obj.NguoiDaiDienDto.MaNguoiDaiDien = Guid.NewGuid();

            var command_2 = new CreateNguoiDaiDienCommand { NguoiDaiDienDto = obj.NguoiDaiDienDto };
            var response_2 = await _mediator.Send(command_2);
            return Ok(new
            {
                response_1 = response_1,
                response_2 = response_2,
            });
        }

        [HttpPost("UpdateDonVi")]
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

        [HttpDelete("DeleteDonVi/{id}")]
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
