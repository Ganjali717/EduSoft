using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using EduSoft.Data.Managers.Interfaces;
using EduSoft.Data.Managers.Services;
using EduSoft.Model.DTO.Account;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using System.Text;
using EduSoft.Entities.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

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
