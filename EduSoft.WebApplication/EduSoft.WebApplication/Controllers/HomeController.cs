using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using EduSoft.Data.Managers.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace EduSoft.WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HomeController : ControllerBase
    {
        private readonly ITutorialManager _tutorialManager;
        public HomeController(ITutorialManager tutorialManager)
        {
            _tutorialManager = tutorialManager;
        }
        
    }
}