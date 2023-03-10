using EduSoft.Entities.Tutorials;
using EduSoft.Entities;

namespace EduSoft.Data.Managers.Interfaces;

public interface IChapterManager
{
    Task<ManagerResult<List<Chapter>>> GetAll();
    Task<ManagerResult<Chapter>> Get(Guid id);
    Task<ManagerResult<Chapter>> CreateOrUpdate(Chapter chapter);
    Task<ManagerResult> Remove(Guid id);
}