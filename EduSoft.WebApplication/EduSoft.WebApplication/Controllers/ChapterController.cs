using System.Net;
using AutoMapper;
using EduSoft.Data.Managers.Interfaces;
using EduSoft.Entities;
using EduSoft.Entities.Tutorials;
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
    }
}
