﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SurveyApplication.API.Attributes;
using SurveyApplication.Application.DTOs.BaoCaoCauHoi;
using SurveyApplication.Application.Features.BaoCaoCauHoi.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BaoCaoCauHoiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BaoCaoCauHoiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetBaoCaoCauHoi")]
        [HasPermission(new[] { (int)EnumModule.Code.TkKs }, new[] { (int)EnumPermission.Type.Read })]
        public async Task<ActionResult<BaoCaoCauHoiDto>> GetBaoCaoCauHoi([FromQuery] GetBaoCaoCauHoiRequest data)
        {
            var result = await _mediator.Send(data);
            return Ok(result);
        }

        [HttpGet("GetBaoCaoCauHoiChiTiet")]
        [HasPermission(new[] { (int)EnumModule.Code.TkKs }, new[] { (int)EnumPermission.Type.Read })]
        public async Task<ActionResult<BaseQuerieResponse<BaoCaoCauHoiChiTietDto>>> GetBaoCaoCauHoiChiTiet([FromQuery] GetBaoCaoCauHoiChiTietRequest data)
        {
            var result = await _mediator.Send(data);
            return Ok(result);
        }

        [HttpGet("GetDashBoard")]
        [HasPermission(new[] { (int)EnumModule.Code.Dashboard }, new[] { (int)EnumPermission.Type.Read })]
        public async Task<ActionResult<DashBoardDto>> GetDashBoard([FromQuery] GetDashBoardRequest data)
        {
            var result = await _mediator.Send(data);
            return Ok(result);
        }
    }
}

