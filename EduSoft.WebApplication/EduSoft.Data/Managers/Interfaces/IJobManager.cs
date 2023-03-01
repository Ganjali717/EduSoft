using EduSoft.Entities;
using EduSoft.Entities.Jobs;

namespace EduSoft.Data.Managers.Interfaces;

public interface IJobManager
{
    Task<ManagerResult<List<Job>>> GetAllJobs();
    Task<ManagerResult<Job>> GetJobById(Guid id);
    Task<ManagerResult<Job>> CreateOrUpdateJob(Job job);
    Task<ManagerResult> RemoveJob(Guid id);
}