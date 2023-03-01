using EduSoft.Data.DatabaseContext;
using EduSoft.Data.Managers.Interfaces;
using EduSoft.Entities;
using EduSoft.Entities.Tutorials;
using Microsoft.EntityFrameworkCore;

namespace EduSoft.Data.Managers.Services;

public class SubChapterManager:ISubChapterManager
{
    private readonly AppDbContext _context; 
    public SubChapterManager(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ManagerResult<List<Subchapter>>> GetAllSubChapterAsync()
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
            Console.WriteLine(ex);
            result.Message = ex.GetBaseException().Message;
        }

        return result;
    }

    public async Task<ManagerResult<Subchapter>> GetSubChapterbyIdAsync(Guid id)
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
            Console.WriteLine(ex);
            result.Message = ex.GetBaseException().Message;
        }

        return result;
    }

    public async Task<ManagerResult<Subchapter>> CreateOrUpdateSubChapter(Subchapter subchapter)
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
            Console.WriteLine(ex);
            result.Message = ex.GetBaseException().Message;
        }
        return result;
    }

    public async Task<ManagerResult> DeleteSubChapterAsync(Guid id)
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
           result.Message = ex.GetBaseException().Message;
        }
        return result;
    }
}