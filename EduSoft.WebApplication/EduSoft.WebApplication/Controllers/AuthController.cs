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

            var tokenHandler = new JwtSecurityTokenHandler();
            var getKey = _configuration.GetSection("Jwt:Key").Value;
            var key = Encoding.ASCII.GetBytes(getKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Aud, _configuration.GetSection("Jwt:Audience").Value),
                    new Claim(JwtRegisteredClaimNames.Iss, _configuration.GetSection("Jwt:Issuer").Value),
                    new Claim(ClaimTypes.Name, data.Username),
                    new Claim(ClaimTypes.Country, "Azerbaijan"),
                    new Claim(ClaimTypes.DateOfBirth, "17.10.1998"), 
                    new Claim(ClaimTypes.Gender, "Male")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
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
