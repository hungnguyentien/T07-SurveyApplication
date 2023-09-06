﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.API.Models;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Application.Enums;
using SurveyApplication.Application.Features.CauHoi.Requests.Commands;
using SurveyApplication.Application.Features.CauHoi.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Utility;

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

        [HttpGet("GetByCondition")]
        public async Task<ActionResult<BaseQuerieResponse<CauHoiDto>>> GetCauHoiByCondition([FromQuery] Paging paging)
        {
            var result = await _mediator.Send(new GetCauHoiConditionsRequest { PageIndex = paging.PageIndex, PageSize = paging.PageSize, Keyword = paging.Keyword, OrderBy = paging.OrderBy });
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<CauHoiDto>> GetCauHoiById(int id)
        {
            var result = await _mediator.Send(new GetCauHoiDetailRequest { Id = id });
            return Ok(result);
        }

        [HttpGet("GetLoaiCauHoi")]
        public ActionResult GetLoaiCauHoi()
        {
            var result = EnumUltils.GetDescription<EnumCauHoi.Type>().Select(x => new { text = x.Value, value = ((int)x.Key).ToString() });
            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateCauHoi([FromBody] CreateCauHoiDto obj)
        {
            var command = new CreateCauHoiCommand { CauHoiDto = obj };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("Update")]
        public async Task<ActionResult> UpdateCauHoi([FromBody] UpdateCauHoiDto obj)
        {
            var command = new UpdateCauHoiCommand { CauHoiDto = obj };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteCauHoi(int id)
        {
            var command = new DeleteCauHoiCommand { Ids = new List<int> { id } };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("DeleteMultiple")]
        public async Task<ActionResult> DeleteMultipleCauHoi(List<int> ids)
        {
            var command = new DeleteCauHoiCommand { Ids = ids };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
