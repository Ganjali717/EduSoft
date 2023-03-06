using System.Net;
using AutoMapper;
using EduSoft.Data.Managers.Interfaces;
using EduSoft.Entities;
using EduSoft.Entities.Tutorials;
using EduSoft.Model.DTO.Tutorials;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduSoft.WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChapterController : ControllerBase
    {
        private readonly IChapterManager _chapterManager;
        private readonly IMapper _mapper;
        public ChapterController(IChapterManager chapterManager, IMapper mapper)
        {
            _chapterManager = chapterManager;
            _mapper = mapper;
        }

        [HttpGet("GetAllChapters")]
        public async Task<IActionResult> GetAll()
        {
            var managerResult = await _chapterManager.GetAllChapters();
            if (!managerResult.Success)
            {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return Ok(managerResult.Message);
            }
            var mappedResult = _mapper.Map<ManagerResult<List<Chapter>>>(managerResult);
            return Ok(mappedResult);
        }

        [HttpGet("GetChapterById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var managerResult = await _chapterManager.GetChapter(id);
            if (!managerResult.Success)
            {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return Ok(managerResult.Message);
            }
            var mappedResult = _mapper.Map<ManagerResult<Chapter>>(managerResult); 
            return Ok(mappedResult);
        }

        [HttpPost("CreateOrUpdateChapter")]
        public async Task<IActionResult> CreateOrUpdateChapter(ChapterDto model)
        {
            var chapter = _mapper.Map<Chapter>(model);
            var managerResult = await _chapterManager.CreateOrUpdateChapter(chapter);
            if (managerResult.Success) return Ok(_mapper.Map<ManagerResult<ChapterDto>>(managerResult));
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Ok(managerResult.Message);
        }

        [HttpDelete("RemoveChapter/{id}")]
        public async Task<IActionResult> RemoveChapter(Guid id)
        {
            var managerResult = await _chapterManager.DeleteChapter(id);
            if (!managerResult.Success)
            {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return Ok(managerResult.Message);
            }
            return Ok(managerResult.Message);
        }

    }
}
