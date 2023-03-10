using EduSoft.Entities.Tutorials;
using EduSoft.Entities;

namespace EduSoft.Data.Managers.Interfaces;

public interface ITutorialManager
{
    Task<ManagerResult<List<Tutorial>>> GetAll();
    Task<ManagerResult<Tutorial>> Get(Guid id);
    Task<ManagerResult<Tutorial>> CreateOrUpdate(Tutorial tutorial);
    Task<ManagerResult> Remove(Guid id);

}