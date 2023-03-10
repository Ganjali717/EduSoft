using System.Net;
using AutoMapper;
using EduSoft.Data.Managers.Interfaces;
using EduSoft.Entities;
using EduSoft.Entities.Tutorials;
using EduSoft.Model.DTO.Tutorials;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduSoft.WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChapterController : ControllerBase
    {
        private readonly IChapterManager _chapterManager;
        private readonly IMapper _mapper;
        public ChapterController(IChapterManager chapterManager, IMapper mapper)
        {
            _chapterManager = chapterManager;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var managerResult = await _chapterManager.GetAll();
            if (!managerResult.Success)
            {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return Ok(managerResult.Message);
            }
            var mappedResult = _mapper.Map<ManagerResult<List<Chapter>>>(managerResult);
            return Ok(mappedResult);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get([FromRoute]Guid id)
        {
            var managerResult = await _chapterManager.Get(id);
            if (!managerResult.Success)
            {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return Ok(managerResult.Message);
            }
            var mappedResult = _mapper.Map<ManagerResult<Chapter>>(managerResult); 
            return Ok(mappedResult);
        }

        [HttpPost("CreateOrUpdate")]
        public async Task<IActionResult> CreateOrUpdate([FromBody]ChapterDto model)
        {
            var chapter = _mapper.Map<Chapter>(model);
            var managerResult = await _chapterManager.CreateOrUpdate(chapter);
            if (managerResult.Success) return Ok(_mapper.Map<ManagerResult<ChapterDto>>(managerResult));
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Ok(managerResult.Message);
        }

        [HttpDelete("Remove/{id}")]
        public async Task<IActionResult> Remove([FromRoute]Guid id)
        {
            var managerResult = await _chapterManager.Remove(id);
            if (!managerResult.Success)
            {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return Ok(managerResult.Message);
            }
            return Ok(managerResult.Message);
        }

    }
}
