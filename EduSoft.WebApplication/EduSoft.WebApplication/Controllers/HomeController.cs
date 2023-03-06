using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using EduSoft.Data.Managers.Interfaces;

namespace EduSoft.WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ITutorialManager _tutorialManager;
        public HomeController(ITutorialManager tutorialManager)
        {
            _tutorialManager = tutorialManager;
        }
        
    }
}