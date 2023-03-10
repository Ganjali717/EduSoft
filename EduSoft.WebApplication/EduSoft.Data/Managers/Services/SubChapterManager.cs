using EduSoft.Data.DatabaseContext;
using EduSoft.Data.Managers.Interfaces;
using EduSoft.Entities;
using EduSoft.Entities.Tutorials;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EduSoft.Data.Managers.Services;

public class SubChapterManager:ISubChapterManager
{
    private readonly AppDbContext _context; 
    private readonly ILogger<SubChapterManager> _logger;
    public SubChapterManager(AppDbContext context, ILogger<SubChapterManager> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ManagerResult<List<Subchapter>>> GetAll()
    {
        var result = new ManagerResult<List<Subchapter>>();
        try
        {
            var allSubChapters = await _context.Subchapters.ToListAsync();
            if (allSubChapters != null)
            {
                result.Data = allSubChapters;
                result.Success = true;
            }
            else
            {
                result.Success = false;
                result.Message = "Not have any SubChapter in database";
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.GetBaseException().Message);
            result.Message = ex.GetBaseException().Message;
        }

        return result;
    }
    public async Task<ManagerResult<Subchapter>> Get(Guid id)
    {
        var result = new ManagerResult<Subchapter>();
        try
        {
            var subChapter = await _context.Subchapters.FindAsync(id);
            if (subChapter != null)
            {
                result.Data = subChapter;
                result.Success = true;
            }
            else
            {
                result.Success = false;
                result.Message = "Not found subchapter with this id";
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.GetBaseException().Message);
            result.Message = ex.GetBaseException().Message;
        }

        return result;
    }
    public async Task<ManagerResult<Subchapter>> CreateOrUpdate(Subchapter subchapter)
    {
        var result = new ManagerResult<Subchapter>();
        try
        {
            if (subchapter.Id == Guid.Empty)
            {
                if (subchapter != null)
                {
                    await _context.Subchapters.AddAsync(subchapter);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                var oldSubChap = await _context.Subchapters.FindAsync(subchapter);
                oldSubChap.Content = subchapter.Content;
                oldSubChap.Created = DateTime.Now;
                oldSubChap.Title = subchapter.Title;
                oldSubChap.Chapter = subchapter.Chapter;
                oldSubChap.ChapterId = subchapter.ChapterId;
                await _context.SaveChangesAsync();
            }
            result.Success = false;
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
            var subChapter =  await  _context.Subchapters.FindAsync(id);
            if (subChapter != null)
            {
                _context.Subchapters.Remove(subChapter);
                await _context.SaveChangesAsync();
                result.Success = true;
            }
            result.Success = false;
            result.Message = "SubChapter with this Id not found";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.GetBaseException().Message);
            result.Message = ex.GetBaseException().Message;
        }
        return result;
    }
}