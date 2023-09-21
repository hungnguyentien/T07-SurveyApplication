using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.API.Models;
using SurveyApplication.Application.DTOs.Role;
using SurveyApplication.Application.DTOs.TinhTp;
using SurveyApplication.Application.Features.Role.Requests.Commands;
using SurveyApplication.Application.Features.Role.Requests.Queries;
using SurveyApplication.Application.Features.TinhTps.Requests.Commands;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Utility;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<RoleDto>>> GetAll()
        {
            var response = await _mediator.Send(new GetRoleListRequest());
            return response;
        }

        [HttpGet("GetByCondition")]
        public async Task<ActionResult<BaseQuerieResponse<RoleDto>>> GetByCondition([FromQuery] Paging paging)
        {
            var response = await _mediator.Send(new GetConditionsRequest { PageIndex = paging.PageIndex, PageSize = paging.PageSize, Keyword = paging.Keyword, OrderBy = paging.OrderBy });
            return response;
        }

        [HttpGet("GetMatrixPermission")]
        public ActionResult<List<MatrixPermission>> GetMatrixPermission()
        {
            return MapEnum.MatrixPermission.Select(x => new MatrixPermission
            {
                Module = x.Key,
                NameModule = EnumUltils.GetDescriptionValue<EnumModule.Code>().GetValueOrDefault(x.Key, ""),
                LstPermission = x.Value.Select(v => new LstPermission
                {
                    Value = (int)v,
                    Name = EnumUltils.GetDescription<EnumPermission.Type>().GetValueOrDefault(v, "")
                }).ToList()
            }).ToList();
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<UpdateRoleDto>> GetById(string id)
        {
            var response = await _mediator.Send(new GetRoleDetailRequest { Id = id });
            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<BaseCommandResponse>> CreateRole(CreateRoleDto role)
        {
            var response = await _mediator.Send(new RoleCommand { CreateRoleDto = role });
            return response;
        }

        [HttpPost("Update")]
        public async Task<ActionResult<BaseCommandResponse>> UpdateRole(UpdateRoleDto role)
        {
            var response = await _mediator.Send(new UpdateRoleCommand { UpdateRoleDto = role });
            return response;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<List<BaseCommandResponse>>> DeleteRole(string id)
        {
            var command = new DeleteRoleCommand { Ids = new List<string> { id } };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

    }
}
