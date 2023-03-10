using AutoMapper;
using EduSoft.Data.Managers.Interfaces;
using EduSoft.Entities.Tutorials;
using EduSoft.Entities;
using EduSoft.Model.DTO.SubChapter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EduSoft.WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubChapterIntroController : ControllerBase
    {
        private readonly ISubChapterIntroManager _manager;
        private readonly IMapper _mapper;
        public SubChapterIntroController(ISubChapterIntroManager subChapterManager, IMapper mapper)
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
            var mappedResult = _mapper.Map<ManagerResult<List<SubChapterIntroDto>>>(managerResult);
            return Ok(mappedResult.Data);
        }
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get([FromRoute]Guid id)
        {
            var managerResult = await _manager.Get(id);
            if (!managerResult.Success)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Ok(managerResult.Message);
            }
            var mappedResult = _mapper.Map<ManagerResult<SubChapterIntroDto>>(managerResult);
            return Ok(mappedResult.Data);
        }
        [HttpPost("CreateOrUpdate")]
        public async Task<IActionResult> CreateOrUpdate([FromBody]SubChapterDto model)
        {
            var mapped = _mapper.Map<SubChapterIntro>(model);
            var managerResult = await _manager.CreateOrUpdate(mapped);
            if (managerResult.Success) return Ok(_mapper.Map<ManagerResult<SubChapterIntroDto>>(managerResult));
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
