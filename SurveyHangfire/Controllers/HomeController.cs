using Microsoft.AspNetCore.Mvc;

namespace Hangfire.Controllers;

public class HomeController : ControllerBase
{
    public IActionResult Index()
    {
        return Redirect("/hangfire");
    }
}