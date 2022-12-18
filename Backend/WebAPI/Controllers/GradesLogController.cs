using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class GradesLogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
