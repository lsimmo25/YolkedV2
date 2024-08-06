using Microsoft.AspNetCore.Mvc;

namespace YolkedWorkoutLogger.Server.Controllers
{
    public class BodyWeightController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
