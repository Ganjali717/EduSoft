using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using EduSoft.Data.Managers.Interfaces;

namespace EduSoft.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITutorialManager _tutorialManager;
        public HomeController(ITutorialManager tutorialManager)
        {
            _tutorialManager = tutorialManager;
        }
        
    }
}