using Microsoft.AspNetCore.Mvc;

namespace SurveyApplication.API.Controllers
{
    public class CauHoiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
