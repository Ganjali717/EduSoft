﻿using EduSoft.Data.DatabaseContext;
using EduSoft.Data.Managers.Interfaces;
using EduSoft.Entities.Tutorials;
using EduSoft.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EduSoft.Data.Managers.Services;

public class ChapterManager:IChapterManager
{
    private readonly AppDbContext _context;
    private readonly ILogger<ChapterManager> _logger;
    public ChapterManager(AppDbContext context, ILogger<ChapterManager> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ManagerResult<List<Chapter>>> GetAll()
    {
        var result = new ManagerResult<List<Chapter>>();
        try
        {
            var context = await _context.Chapters.Include(x=> x.Tutorial.Category).ToListAsync();
            result.Data = context;
            result.Success = true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.GetBaseException().Message);
            result.Message = ex.GetBaseException().Message;
        }

        return result;
    }

    public async Task<ManagerResult<Chapter>> Get(Guid id)
    {
        var result = new ManagerResult<Chapter>();
        try
        {
            var chapter = await _context.Chapters.FindAsync(id);
            if (chapter != null) { result.Data = chapter;  result.Success = true; }
            else { result.Success = false; }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.GetBaseException().Message);
            result.Message = ex.GetBaseException().Message;
        }
        return result;
    }

    public async Task<ManagerResult<Chapter>> CreateOrUpdate(Chapter chapter)
    {
        var result = new ManagerResult<Chapter>();
        try
        {
            if (chapter.Id == Guid.Empty)
            {
                await _context.Chapters.AddAsync(chapter);
                await _context.SaveChangesAsync();
            }
            else
            {
                var oldChapter = await _context.Chapters.FindAsync(chapter.Id);
                oldChapter.Title = chapter.Title; 
                oldChapter.Tutorial = chapter.Tutorial;
                oldChapter.TutorialId = chapter.TutorialId;
                oldChapter.Created = DateTime.Now;
                await _context.SaveChangesAsync();
                result.Data = oldChapter;
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
            var chapter = await _context.Chapters.FindAsync(id);
            if (chapter != null)
            {
                _context.Chapters.Remove(chapter);
                await _context.SaveChangesAsync();
                result.Success = true;
            }
            result.Success = false;
            result.Message = "Chapter with this Id not found";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.GetBaseException().Message);
            result.Message = ex.GetBaseException().Message;
        }
        return result;
    }
}