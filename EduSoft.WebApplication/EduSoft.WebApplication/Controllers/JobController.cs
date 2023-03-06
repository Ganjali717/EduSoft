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
        public IActionResult GetAllVacancies()
        {
            var managerResult = _jobManager.GetAllJobs();
            if (!managerResult.Result.Success)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Ok(managerResult.Result.Message);
            }
            var mappedResult = _mapper.Map<ManagerResult<List<Job>>>(managerResult.Result);
            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("GetVacancy/{id}")]
        public IActionResult GetJobById(Guid id)
        {
            var managerResult = _jobManager.GetJobById(id);
            if (!managerResult.Result.Success)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Ok(managerResult.Result.Message);
            }
            var mapperResult = _mapper.Map<ManagerResult<JobDTO>>(managerResult.Result);
            return Ok(mapperResult.Data);
        }

        [HttpPost]
        [Route("addVacancy")]
        [DisableRequestSizeLimit]
        public IActionResult CreateOrUpdateVacancy(JobDTO model)
        {
            var jobs = _mapper.Map<Job>(model);
            var managerResult =  _jobManager.CreateOrUpdateJob(jobs).Result;
            if (managerResult.Success) return Ok(_mapper.Map<ManagerResult<JobDTO>>(managerResult));
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Ok(managerResult.Message);
        }

        [HttpDelete]
        [Route("RemoveVacancy/{id}")]
        public IActionResult DeleteJob(Guid id)
        {
            var managerResult = _jobManager.RemoveJob(id);
            if (!managerResult.Result.Success)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Ok(managerResult.Result.Message);
            }
            return Ok(managerResult.Result.Message);
        }
    }
}
