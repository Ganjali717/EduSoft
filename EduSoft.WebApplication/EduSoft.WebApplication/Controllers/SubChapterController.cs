using EduSoft.Data.Managers.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using AutoMapper;
using EduSoft.Model.DTO.SubChapter;
using EduSoft.Entities;
using EduSoft.Model.DTO.Tutorials;
using EduSoft.Entities.Tutorials;
using EduSoft.Model.DTO.Jobs;

namespace EduSoft.WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubChapterController : ControllerBase
    {
        private readonly ISubChapterManager _manager;
        private readonly IMapper _mapper;
        public SubChapterController(ISubChapterManager subChapterManager, IMapper mapper)
        {
            _manager = subChapterManager;
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
            var mappedResult = _mapper.Map<ManagerResult<List<Subchapter>>>(managerResult);
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
            var mappedResult = _mapper.Map<ManagerResult<SubChapterDto>>(managerResult);
            return Ok(mappedResult.Data);
        }
        [HttpPost("CreateOrUpdate")]
        public async Task<IActionResult> CreateOrUpdate([FromBody]SubChapterDto model)
        {
            var mapped =  _mapper.Map<Subchapter>(model);
            var managerResult = await _manager.CreateOrUpdate(mapped);
            if (managerResult.Success) return Ok(_mapper.Map<ManagerResult<SubChapterDto>>(managerResult));
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
