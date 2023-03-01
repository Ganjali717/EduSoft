using System.ComponentModel.DataAnnotations;
using EduSoft.Data.DatabaseContext;
using EduSoft.Data.Managers.Interfaces;
using EduSoft.Entities;
using EduSoft.Entities.Jobs;
using Microsoft.EntityFrameworkCore;

namespace EduSoft.Data.Managers.Services;

public class JobManager : IJobManager
{
    private readonly AppDbContext _context;

    public JobManager(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ManagerResult<List<Job>>> GetAllJobs()
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

            result.Success = false;
        }
        catch (Exception ex)
        {
            result.Message = ex.GetBaseException().Message;
        }

        return result;
    }

    public async Task<ManagerResult<Job>> GetJobById(Guid id)
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

            result.Success = false;
        }
        catch (Exception ex)
        {
            result.Message = ex.GetBaseException().Message;
        }

        return result;
    }

    public async Task<ManagerResult<Job>> CreateOrUpdateJob(Job job)
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
            result.Message = ex.GetBaseException().Message;
        }

        return result;
    }

    public async Task<ManagerResult<Job>> RemoveJob(Guid id)
    {
        var result = new ManagerResult<Job>();
        try
        {
            if (id != Guid.Empty)
            {
                     _context.Jobs.Remove(id);
                     await _context.SaveChangesAsync(); 
                     result.Success = true;
            }
        }
        catch (Exception ex)
        {
            result.Message = ex.GetBaseException().Message;
        }
        return result;
    }
}
   