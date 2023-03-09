using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using EduSoft.Data.DatabaseContext;
using EduSoft.Data.Managers.Interfaces;
using EduSoft.Entities;
using EduSoft.Entities.Extentions;
using EduSoft.Entities.Security;
using EduSoft.Model.DTO.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace EduSoft.Data.Managers.Services;

public class AccountManager:IAccountManager
{
    private readonly AppDbContext _context;
    private readonly ILogger<AccountManager> _logger;
    private readonly IConfiguration _configuration;
    public AccountManager(AppDbContext context, ILogger<AccountManager> logger, IConfiguration configuration)
    {
        _context = context;
        _logger = logger;
        _configuration = configuration;
    }
    public async Task<ManagerResult<List<AppUser>>> GetAllAccounts()
    {
        var result = new ManagerResult<List<AppUser>>();
        try
        {
            var allUsers = await _context.AppUsers.ToListAsync();
            if (allUsers != null)
            {
                result.Data = allUsers;
                result.Success = true;
            }
            result.Success = false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.GetBaseException().Message);
            result.Message = ex.GetBaseException().Message;
        }
        return result;
    }
    public async Task<ManagerResult<AppUser>> GetAccount(Guid id)
    {
        var result = new ManagerResult<AppUser>();
        try
        {
            var user = await _context.AppUsers.FindAsync(id);
            if (user != null) { result.Data = user; result.Success = true; }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.GetBaseException().Message);
            result.Message = ex.GetBaseException().Message;
        }
        return result;
    }
    public async Task<ManagerResult> RemoveAccount(Guid id)
    {
        var result = new ManagerResult();
        try
        {
            var user = await _context.AppUsers.FindAsync(id);
            if (user != null)
            {
                _context.AppUsers.Remove(user);
                await _context.SaveChangesAsync();
                result.Success = true;
            }
            result.Success = false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.GetBaseException().Message);
            result.Message = ex.GetBaseException().Message;
        }

        return result;
    }
    public async Task<ManagerResult<AppUser>> GetAccountBySecurityToken(string token)
    {
        var result = new ManagerResult<AppUser>();
        try
        {
            var user = await _context.AppUsers.FirstOrDefaultAsync(x => x.SecurityToken == token);
            if (user != null)
            {
                result.Data = user; 
                result.Success = true;
            }
            result.Success = false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.GetBaseException().Message);
            result.Message = ex.GetBaseException().Message;
        }
        return result;
    }
    public async Task<ManagerResult<AppUser>> Login(string email, string password)
    {
        var result = new ManagerResult<AppUser>();
        try
        {
            var hash = GetPasswordHash(email, password);
            var account = await _context.AppUsers.FirstOrDefaultAsync(x => x.Email == email && x.Password == hash);
            if (account == null)
            {
                result.Success = false;
                result.Message = "User Name or Password isn't correct.";
                return result;
            }
            result.Success = true;
            result.Data = account;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.GetBaseException().Message);
            result.Message = ex.GetBaseException().Message;
        }
        return result;
    }
    public async Task<ManagerResult<AppUser>> GetAccountByName(string username)
    {
        var result = new ManagerResult<AppUser>();
        try
        {
            var user = await _context.AppUsers.Where(x=>x.FirstName == username).FirstOrDefaultAsync();
            if (user != null)
            {
                result.Data = user;
                result.Success = true;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.GetBaseException().Message);
            result.Message = ex.GetBaseException().Message;
        }
        return result;
    }
    public String GenerateJwtToken(LoginDto data)
    {
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
        return tokenString;
    }
    private string? GetPasswordHash(string? email, string? password)
    {
        var salt = email.GetSHA256Hash();
        return (password + salt).GetSHA256Hash();
    }
}