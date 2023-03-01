using EduSoft.Entities;
using EduSoft.Entities.Security;

namespace EduSoft.Data.Managers.Interfaces;

public interface IAccountManager
{
    Task<ManagerResult<List<AppUser>>> GetAllUsers();
    Task<ManagerResult<AppUser>> GetUserById(Guid id);
    Task<ManagerResult> RemoveUser(Guid id);
    Task<ManagerResult<AppUser>> GetUserBySecuretyToken(string token);
}