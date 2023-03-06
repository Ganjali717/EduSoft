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

        [HttpPost]
        [Route("api/addVacancy")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> AddVacancy(JobDTO model)
        {
            var jobs = _mapper.Map<Job>(model);
            var managerResult = _jobManager.CreateOrUpdateJob(jobs).Result;
            if (managerResult.Success) return Json(_mapper.Map<ManagerResult<JobDTO>>(managerResult));
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Ok(managerResult.Message);
        }
    }
}
