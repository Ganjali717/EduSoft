using AutoMapper;
using EduSoft.Data.Managers.Interfaces;
using EduSoft.Model.DTO.Account;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace EduSoft.WebApplication.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAccountManager _accountManager;
        private readonly IMapper _mapper;
        public AuthController(IAccountManager accountManager, IMapper mapper, IConfiguration configuration)
        {
            _accountManager = accountManager;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto data)
        {
            var managerResult = await _accountManager.Login(data.Username, data.Password);
            if (!managerResult.Success)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Ok(managerResult.Message);
            }
            var tokenString = _accountManager.GenerateJwtToken(data);
            return Ok(new { Token = tokenString });
        }

        [HttpGet("GetAccount")]
        [Authorize]
        public async Task<IActionResult> GetAccount()
        {
            var user = await _accountManager.GetAccountByName("Ganjali");
            return Ok(new {user.Data.Id, user.Data.FirstName, user.Data.LastName});
        }
    }
}
