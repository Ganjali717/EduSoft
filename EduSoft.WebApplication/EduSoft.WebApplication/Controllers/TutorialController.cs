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
using Microsoft.AspNetCore.Authorization;

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
        
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var managerResult = await _manager.GetAll();
            if (!managerResult.Success)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Ok(managerResult.Message);
            }
            var mappedResult = _mapper.Map<ManagerResult<List<TutorialDto>>>(managerResult);
            return Ok(mappedResult.Data);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var managerResult = await _manager.Get(id);
            if (!managerResult.Success)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Ok(managerResult.Message);
            }
            var mappedResult = _mapper.Map<ManagerResult<TutorialDto>>(managerResult);
            return Ok(mappedResult.Data);
        }

        [HttpPost("CreateOrUpdate")]
        public async Task<IActionResult> CreateOrUpdate([FromBody]TutorialDto model)
        {
            var jobs = _mapper.Map<Tutorial>(model);
            var managerResult = await _manager.CreateOrUpdate(jobs);
            if (managerResult.Success) return Ok(_mapper.Map<ManagerResult<JobDTO>>(managerResult));
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Ok(managerResult.Message);
        }

        [HttpDelete("Remove/{id}")]
        public async Task<IActionResult> Remove([FromRoute] Guid id)
        {
            var managerResult = await _manager.Remove(id);
            if (!managerResult.Success)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Ok(managerResult.Message);
            }
            return Ok(managerResult.Message);
        }
    }
}
