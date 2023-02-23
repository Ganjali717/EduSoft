using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EduSoft.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}