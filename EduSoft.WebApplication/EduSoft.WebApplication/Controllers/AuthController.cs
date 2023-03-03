using Microsoft.AspNetCore.Mvc;

namespace EduSoft.WebApplication.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
