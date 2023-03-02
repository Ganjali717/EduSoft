using AutoMapper;
using EduSoft.Data.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using EduSoft.Entities;
using EduSoft.Model.DTO.Tutorials;

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

        [HttpGet]
        [Route("api/getalltut")]
        public async Task<IActionResult> GetTutorials()
        {
            var managerResult = _manager.GetAllTutorials();
            if (!managerResult.Result.Success)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(managerResult.Result.Message);
            }

            var mappedResult = _mapper.Map<ManagerResult<List<TutorialDto>>>(managerResult.Result);
            return Json(mappedResult);
        }
    }
}
