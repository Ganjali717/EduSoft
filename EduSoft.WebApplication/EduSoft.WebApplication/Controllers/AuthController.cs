using AutoMapper;
using EduSoft.Data.Managers.Interfaces;
using EduSoft.Data.Managers.Services;
using EduSoft.Model.DTO.Account;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using EduSoft.Entities.Security;

namespace EduSoft.WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private const string schema = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/";
        private readonly IAccountManager _accountManager;
        private readonly IMapper _mapper;
        public AuthController(IAccountManager accountManager, IMapper mapper)
        {
            _accountManager = accountManager;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto data)
        {
            var managerResult = _accountManager.Login(data.Username, data.Password);
            if (!managerResult.Result.Success)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Ok(managerResult.Result.Message);
            }
            await Authenticate(managerResult.Result.Data);
            managerResult.Result.Message = GetRedirectUrl(data.ReturnUrl, managerResult.Result.Data.Role);
            return Ok(managerResult.Result);
        }

        private async Task Authenticate(AppUser account)
        {
            var claims = new List<Claim>
            {
                new(ClaimsIdentity.DefaultNameClaimType, account.Email ?? string.Empty),
                new(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new(ClaimTypes.Email, account.Email ?? string.Empty),
                new(ClaimTypes.GivenName, account.FirstName ?? string.Empty),
                new(ClaimTypes.Surname, account.LastName ?? string.Empty),
                new(schema+"fullname", $"{account.FirstName} {account.LastName}"),
                new(ClaimTypes.Role, account.Role.ToString())
            };
            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        private string GetRedirectUrl(string? returnUrl, Role role)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return returnUrl;
            }
            else
            {
                switch (role)
                {
                    case Role.Admin:
                        return Url.Action("index", "accounts", null, Request.Scheme, null) ?? string.Empty;
                        break;
                    case Role.Manager:
                        return Url.Action("index", "applications", null, Request.Scheme, null) ?? string.Empty;
                        break;
                    default:
                        return Url.Action("index", "accounts", null, Request.Scheme, null) ?? string.Empty;
                        break;
                }

            }
        }
    }
}
