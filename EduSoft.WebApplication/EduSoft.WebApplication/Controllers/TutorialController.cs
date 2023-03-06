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
    public class TutorialController : ControllerBase
    {
        private readonly ITutorialManager _manager;
        private readonly IMapper _mapper;
        public TutorialController(ITutorialManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }
        
        [HttpGet]
        [Route("api/GetAllTutorials")]
        public IActionResult GetAllTutorials()
        {
            var managerResult = _manager.GetAllTutorials();
            if (!managerResult.Result.Success)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Ok(managerResult.Result.Message);
            }
            var mappedResult = _mapper.Map<ManagerResult<List<TutorialDto>>>(managerResult.Result);
            return Ok(mappedResult);
        }
    }
}
