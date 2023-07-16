using EduSoft.Data.DatabaseContext;
using EduSoft.Data.Managers.Interfaces;
using EduSoft.Entities;
using EduSoft.Entities.Jobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EduSoft.Data.Managers.Services;

public class JobManager : IJobManager
{
    private readonly AppDbContext _context;
    private readonly ILogger<JobManager> _logger;

    public JobManager(AppDbContext context, ILogger<JobManager> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ManagerResult<List<Job>>> GetAll()
    {
        var result = new ManagerResult<List<Job>>();
        try
        {
            var allJobs = await _context.Jobs.ToListAsync();
            if (allJobs != null)
            {
                result.Data = allJobs;
                result.Success = true;
            }
            else
            {
                result.Success = false;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.GetBaseException().Message);
            result.Message = ex.GetBaseException().Message;
        }

        return result;
    }
    public async Task<ManagerResult<Job>> Get(Guid id)
    {
        var result = new ManagerResult<Job>();
        try
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job != null)
            {
                result.Data = job;
                result.Success = true;
            }
            else
            {
                result.Success = false;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.GetBaseException().Message);
            result.Message = ex.GetBaseException().Message;
        }

        return result;
    }
    public async Task<ManagerResult<Job>> CreateOrUpdate(Job job)
    {
        var result = new ManagerResult<Job>();
        try
        {
            if (job.Id == Guid.Empty)
            {
                await _context.Jobs.AddAsync(job);
                await _context.SaveChangesAsync();
            }
            else
            {
                var oldJob = await _context.Jobs.FindAsync(job);
                oldJob.Company = job.Company;
                oldJob.Created = DateTime.Now;
                oldJob.Description = job.Description;
                oldJob.Location = job.Location;
                oldJob.Title = job.Title;
                await _context.SaveChangesAsync();
            }

            result.Success = true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.GetBaseException().Message);
            result.Message = ex.GetBaseException().Message;
        }

        return result;
    }
    public async Task<ManagerResult> Remove(Guid id)
    {
        var result = new ManagerResult();
        try
        {
            if (id != Guid.Empty)
            {
                var job = await _context.Jobs.FindAsync(id);
                _context.Jobs.Remove(job);
                await _context.SaveChangesAsync(); 
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
}
   