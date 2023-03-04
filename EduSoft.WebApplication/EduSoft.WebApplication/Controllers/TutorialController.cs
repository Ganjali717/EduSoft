using AutoMapper;
using EduSoft.Data.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using EduSoft.Entities;
using EduSoft.Model.DTO.Tutorials;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using EduSoft.Entities.Tutorials;
using System.Xml.Linq;

namespace EduSoft.WebApplication.Controllers
{
    public class TutorialController : Controller
    {
        private readonly ITutorialManager _manager;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        public TutorialController(ITutorialManager manager, IMapper mapper, IMemoryCache memoryCache)
        {
            _manager = manager;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }
        
        [HttpGet]
        [Route("api/getalltut")]
        public IActionResult Dictionary()
        {
            var managerResult = _manager.GetAllTutorials().Result.Data;
            /*if (!managerResult.Result.Success)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(managerResult.Result.Message);
            }
            var mappedResult = _mapper.Map<ManagerResult<List<TutorialDto>>>(managerResult.Result);*/
            return Json(managerResult);
        }
    }
}
