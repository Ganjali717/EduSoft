using EduSoft.Data.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using AutoMapper;
using EduSoft.Entities;
using EduSoft.Entities.Jobs;
using EduSoft.Model.DTO.Jobs;

namespace EduSoft.WebApplication.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobManager _jobManager;
        private readonly IMapper _mapper;

        public JobController(IJobManager jobManager, IMapper mapper)
        {
            _jobManager = jobManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/GetAllVacancies")]
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
        [Route("api/GetVacancy/{id}")]
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
        [Route("api/addVacancy")]
        [DisableRequestSizeLimit]
        public IActionResult AddVacancy(JobDTO model)
        {
            var jobs = _mapper.Map<Job>(model);
            var managerResult =  _jobManager.CreateOrUpdateJob(jobs).Result;
            if (managerResult.Success) return Json(_mapper.Map<ManagerResult<JobDTO>>(managerResult));
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Ok(managerResult.Message);
        }

        [HttpDelete]
        [Route("api/RemoveVacancy")]
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
