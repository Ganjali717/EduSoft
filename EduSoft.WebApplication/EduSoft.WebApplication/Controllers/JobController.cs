﻿using EduSoft.Data.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using AutoMapper;
using EduSoft.Entities;
using EduSoft.Entities.Jobs;
using EduSoft.Model.DTO.Jobs;
using Microsoft.AspNetCore.Authorization;

namespace EduSoft.WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobController : ControllerBase
    {
        private readonly IJobManager _jobManager;
        private readonly IMapper _mapper;

        public JobController(IJobManager jobManager, IMapper mapper)
        {
            _jobManager = jobManager;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var managerResult = await _jobManager.GetAll();
            if (!managerResult.Success)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Ok(managerResult.Message);
            }
            var mappedResult = _mapper.Map<ManagerResult<List<Job>>>(managerResult);
            return Ok(mappedResult);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var managerResult = await _jobManager.Get(id);
            if (!managerResult.Success)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Ok(managerResult.Message);
            }
            var mapperResult = _mapper.Map<ManagerResult<JobDTO>>(managerResult);
            return Ok(mapperResult.Data);
        }

        [HttpPost("CreateOrUpdate")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> CreateOrUpdate([FromBody]JobDTO model)
        {
            var jobs = _mapper.Map<Job>(model);
            var managerResult = await _jobManager.CreateOrUpdate(jobs);
            if (managerResult.Success) return Ok(_mapper.Map<ManagerResult<JobDTO>>(managerResult));
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Ok(managerResult.Message);
        }

        [HttpDelete("Remove/{id}")]
        public async Task<IActionResult> Remove([FromRoute]Guid id)
        { 
            var managerResult = await _jobManager.Remove(id);
            if (!managerResult.Success)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Ok(managerResult.Message);
            }
            return Ok(managerResult.Message);
        }
    }
}
