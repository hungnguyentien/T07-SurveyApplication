using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.API.Attributes;
using SurveyApplication.API.Models;
using SurveyApplication.Application.DTOs.GuiEmail;
using SurveyApplication.Application.DTOs.PhieuKhaoSat;
using SurveyApplication.Application.Features.GuiEmail.Requests.Queries;
using SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class IpAddressController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IpAddressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetByCondition")]
        [HasPermission(new[] { (int)EnumModule.Code.QlIp }, new[] { (int)EnumPermission.Type.Read })]
        public async Task<ActionResult<BaseQuerieResponse<IpAddressDto>>> GetGuiEmailByCondition([FromQuery] Paging paging)
        {
            var lstGuiMail = await _mediator.Send(new GetIpAddressRequest { PageIndex = paging.PageIndex, PageSize = paging.PageSize, Keyword = paging?.Keyword ?? "" });
            return Ok(lstGuiMail);
        }
    }
}
