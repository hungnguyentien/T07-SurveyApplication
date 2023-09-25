using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.API.Models;
using SurveyApplication.Application.DTOs.BackupRestore;
using SurveyApplication.Application.Features.BackupRestore.Requests.Commands;
using SurveyApplication.Application.Features.BackupRestore.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Utility.Enums;
using SurveyApplication.Utility;

namespace SurveyApplication.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BackupRestoreController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BackupRestoreController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetByCondition")]
        //[HasPermission(new[] { (int)EnumModule.Code.QlKs }, new[] { (int)EnumPermission.Type.Read })]
        public async Task<ActionResult<BaseQuerieResponse<BackupRestoreDto>>> GetByConditionBangKhaoSat([FromQuery] Paging paging)
        {
            var lstBackupRestore = await _mediator.Send(new GetBackupRestoreConditionsRequest { PageIndex = paging.PageIndex, PageSize = paging.PageSize, Keyword = paging.Keyword });
            return lstBackupRestore;
        }

        [HttpGet("GetConfigBackup")]
        public async Task<ActionResult<ConfigJobBackupDto>> GetConfigBackup()
        {
            var result = await _mediator.Send(new GetConfigBackUpRequest());
            return result;
        }

        [HttpPost("ConfigBackup")]
        public async Task<ActionResult<BaseCommandResponse>> ConfigBackup(ConfigJobBackupDto configJobBackup)
        {
            var result = await _mediator.Send(new ConfigBackupCommand { ConfigJobBackup = configJobBackup });
            return result;
        }

        [HttpPost("BackupNow")]
        public async Task<ActionResult<BaseCommandResponse>> BackupNow()
        {
            var result = await _mediator.Send(new BackupNowCommand());
            return result;
        }

        [HttpPost("RestoreData/{fileName}")]
        public async Task<ActionResult<BaseCommandResponse>> RestoreData(string fileName)
        {
            var result = await _mediator.Send(new RestoreDataCommand { FileNames = new[] { fileName } });
            return result;
        }

        [HttpGet("ScheduleDayofweek")]
        public ActionResult ScheduleDayofweek()
        {
            var result = EnumUltils.GetDescription<EnumBackupStore.ScheduleDayofweek>().Select(x => new { text = x.Value, value = ((int)x.Key).ToString() }).ToList();
            return Ok(result);
        }

        [HttpGet("ScheduleHour")]
        public ActionResult ScheduleHour()
        {
            var result = EnumUltils.GetDescription<EnumBackupStore.ScheduleHour>().Select(x => new { text = x.Value, value = ((int)x.Key).ToString() }).ToList();
            return Ok(result);
        }

        [HttpGet("ScheduleMinute")]
        public ActionResult ScheduleMinute()
        {
            var result = EnumUltils.GetDescription<EnumBackupStore.ScheduleMinute>().Select(x => new { text = x.Value, value = ((int)x.Key).ToString() }).ToList();
            return Ok(result);
        }
    }
}
