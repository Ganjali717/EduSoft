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
using EduSoft.Data.Managers.Services;
using EduSoft.Entities.Jobs;
using EduSoft.Model.DTO.Jobs;

namespace EduSoft.WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [Route("GetAllTutorials")]
        public async Task<IActionResult> GetAllTutorials()
        {
            var managerResult = await _manager.GetAllTutorials();
            if (!managerResult.Success)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Ok(managerResult.Message);
            }
            var mappedResult = _mapper.Map<ManagerResult<List<TutorialDto>>>(managerResult);
            return Ok(mappedResult.Data);
        }

        [HttpGet]
        [Route("GetTutorial/{id}")]
        public async Task<IActionResult> GetTutorialById(Guid id)
        {
            var managerResult = await _manager.GetTutorial(id);
            if (!managerResult.Success)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Ok(managerResult.Message);
            }
            var mappedResult = _mapper.Map<ManagerResult<TutorialDto>>(managerResult);
            return Ok(mappedResult.Data);
        }

        [HttpPost]
        [Route("CreateOrUpdateTutorial")]
        public async Task<IActionResult> CreateOrUpdateTutorial(TutorialDto model)
        {
            var jobs = _mapper.Map<Tutorial>(model);
            var managerResult = await _manager.CreateOrUpdateTutorial(jobs);
            if (managerResult.Success) return Ok(_mapper.Map<ManagerResult<JobDTO>>(managerResult));
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Ok(managerResult.Message);
        }

        [HttpDelete]
        [Route("RemoveTutorial/{id}")]
        public async Task<IActionResult> RemoveTutorial(Guid id)
        {
            var managerResult = await _manager.DeleteTutorial(id);
            if (!managerResult.Success)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Ok(managerResult.Message);
            }
            return Ok(managerResult.Message);
        }
    }
}
