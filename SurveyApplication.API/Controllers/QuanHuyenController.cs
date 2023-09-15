﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SurveyApplication.API.Models;
using SurveyApplication.Application.DTOs.QuanHuyen;
using SurveyApplication.Application.DTOs.XaPhuong;
using SurveyApplication.Application.Features.QuanHuyens.Requests.Commands;
using SurveyApplication.Application.Features.QuanHuyens.Requests.Queries;
using SurveyApplication.Application.Features.XaPhuongs.Requests.Commands;
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
        public async Task<ActionResult<BaseQuerieResponse<QuanHuyenDto>>> GetByConditionQuanHuyen([FromQuery] Paging paging)
        {
            var leaveAllocations = await _mediator.Send(new GetQuanHuyenConditionsRequest { PageIndex = paging.PageIndex, PageSize = paging.PageSize, Keyword = paging.Keyword });
            return leaveAllocations;
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<List<QuanHuyenDto>>> GetByIdQuanHuyen(int id)
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
            var command = new DeleteQuanHuyenCommand { Ids = new List<int> { id } };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("DeleteMultiple")]
        public async Task<ActionResult> DeleteMultipleCauHoi(List<int> ids)
        {
            var command = new DeleteQuanHuyenCommand { Ids = ids };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("Import")]
        public async Task<IActionResult> ImportQuanHuyen([FromForm] ImportQuanHuyenDto obj)
        {
            var command = new ImportQuanHuyenCommand { File = obj.File };
            await _mediator.Send(command);
            return Ok(new
            {
                Success = true,
            });
        }
    }
}
