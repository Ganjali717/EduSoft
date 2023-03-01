using EduSoft.Entities;
using EduSoft.Entities.Security;

namespace EduSoft.Data.Managers.Interfaces;

public interface IAccountManager
{
    Task<ManagerResult<List<AppUser>>> GetAllUsers();
    Task<ManagerResult<AppUser>> GetUserById(Guid id);
    Task<ManagerResult> RemoveUser(Guid id);
    Task<ManagerResult<AppUser>> GetUserBySecurityToken(string token);
    Task<ManagerResult<AppUser>> Login(string email, string password);
}