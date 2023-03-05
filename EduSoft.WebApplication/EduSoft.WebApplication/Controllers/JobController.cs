using Microsoft.AspNetCore.Mvc;

namespace EduSoft.WebApplication.Controllers
{
    public class JobController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
