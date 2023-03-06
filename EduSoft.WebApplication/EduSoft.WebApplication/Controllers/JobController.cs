using EduSoft.Data.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using AutoMapper;
using EduSoft.Entities;
using EduSoft.Entities.Jobs;
using EduSoft.Model.DTO.Jobs;

namespace EduSoft.WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobManager _jobManager;
        private readonly IMapper _mapper;

        public JobController(IJobManager jobManager, IMapper mapper)
        {
            _jobManager = jobManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllVacancies")]
        public async Task<IActionResult> GetAllVacancies()
        {
            var managerResult = await _jobManager.GetAllJobs();
            if (!managerResult.Success)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Ok(managerResult.Message);
            }
            var mappedResult = _mapper.Map<ManagerResult<List<Job>>>(managerResult);
            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("GetVacancy/{id}")]
        public async Task<IActionResult> GetJobById(Guid id)
        {
            var managerResult = await _jobManager.GetJobById(id);
            if (!managerResult.Success)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Ok(managerResult.Message);
            }
            var mapperResult = _mapper.Map<ManagerResult<JobDTO>>(managerResult);
            return Ok(mapperResult.Data);
        }

        [HttpPost]
        [Route("addVacancy")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> CreateOrUpdateVacancy(JobDTO model)
        {
            var jobs = _mapper.Map<Job>(model);
            var managerResult = await _jobManager.CreateOrUpdateJob(jobs);
            if (managerResult.Success) return Ok(_mapper.Map<ManagerResult<JobDTO>>(managerResult));
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Ok(managerResult.Message);
        }

        [HttpDelete]
        [Route("RemoveVacancy/{id}")]
        public async Task<IActionResult> DeleteJob(Guid id)
        { 
            var managerResult = await _jobManager.RemoveJob(id);
            if (!managerResult.Success)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Ok(managerResult.Message);
            }
            return Ok(managerResult.Message);
        }
    }
}
