using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
