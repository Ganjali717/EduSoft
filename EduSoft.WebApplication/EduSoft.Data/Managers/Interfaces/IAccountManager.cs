using EduSoft.Entities;
using EduSoft.Entities.Security;
using EduSoft.Model.DTO.Account;

namespace EduSoft.Data.Managers.Interfaces;

public interface IAccountManager
{
    Task<ManagerResult<List<AppUser>>> GetAllAccounts();
    Task<ManagerResult<AppUser>> GetAccount(Guid id);
    Task<ManagerResult> RemoveAccount(Guid id);
    Task<ManagerResult<AppUser>> GetAccountBySecurityToken(string token);
    Task<ManagerResult<AppUser>> Login(string email, string password);
    Task<ManagerResult<AppUser>> GetAccountByName(string userName);
    String GenerateJwtToken(LoginDto data);
}