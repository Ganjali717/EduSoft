using EduSoft.Entities;
using EduSoft.Entities.Security;

namespace EduSoft.Data.Managers.Interfaces;

public interface IAccountManager
{
    ManagerResult<List<AppUser>> GetAllUsers();
}