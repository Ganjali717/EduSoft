using AutoMapper;
using EduSoft.Data.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EduSoft.WebApplication.Controllers
{
    public class TutorialController : Controller
    {
        private readonly ITutorialManager _manager;
        private readonly IMapper _mapper;
        public TutorialController(ITutorialManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetTutorials()
        {
            var managerResult = _manager.GetAllTutorials();
            if (!managerResult.Result.Success)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(managerResult.Result.Message);
            }
            return Json(managerResult.Result);
        }
    }
}
