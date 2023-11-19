using System.Threading.Tasks;
using Hangfire.Application.Interfaces;
using Hangfire.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hangfire.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ScheduleController : ControllerBase
{
    /// <summary>
    ///     Cấu hình Schedule Job
    /// </summary>
    /// <param name="jobName"></param>
    /// <param name="service"></param>
    /// <param name="apiUrl"></param>
    /// <param name="cronString"></param>
    /// <returns></returns>
    /// <remarks>
    ///     Sample request:
    ///     Post /ConfigJob
    ///     {
    ///     "jobName":"ScheduleSendEmail",
    ///     "service":"SurveyDomain",
    ///     "apiUrl":"api/PhieuKhaoSat/ScheduleSendEmail",
    ///     "cronString":"*/10 * * * *"
    ///     }
    /// </remarks>
    [AllowAnonymous]
    [ValidSecretKey]
    [HttpPost("ConfigJob")]
    public async Task<IActionResult> ConfigJob(string jobName, string service, string apiUrl, string cronString)
    {
        var rs = await _schedule.UpdateOrAddJob(jobName, service, apiUrl, cronString);
        return new ObjectResult(rs);
    }

    #region Prop & Ctor

    private readonly IScheduleServices _schedule;

    public ScheduleController(IScheduleServices scheduleServices)
    {
        _schedule = scheduleServices;
    }

    #endregion
}