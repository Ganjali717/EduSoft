using EduSoft.Entities;
using EduSoft.Entities.Jobs;

namespace EduSoft.Data.Managers.Interfaces;

public interface IJobManager
{
    Task<ManagerResult<List<Job>>> GetAll();
    Task<ManagerResult<Job>> Get(Guid id);
    Task<ManagerResult<Job>> CreateOrUpdate(Job job);
    Task<ManagerResult> Remove(Guid id);
}