using EduSoft.Entities;
using EduSoft.Entities.Tutorials;

namespace EduSoft.Data.Managers.Interfaces;

public interface ISubChapterManager
{
    Task<ManagerResult<List<Subchapter>>> GetAll();
    Task<ManagerResult<Subchapter>> Get(Guid id);
    Task<ManagerResult<Subchapter>> CreateOrUpdate(Subchapter subchapter);
    Task<ManagerResult> Remove(Guid id);
}