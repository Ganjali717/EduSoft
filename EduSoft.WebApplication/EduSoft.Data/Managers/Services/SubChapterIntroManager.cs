﻿using EduSoft.Data.DatabaseContext;
using EduSoft.Data.Managers.Interfaces;
using EduSoft.Entities;
using EduSoft.Entities.Tutorials;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EduSoft.Data.Managers.Services
{
    public class SubChapterIntroManager:ISubChapterIntroManager
    {
        private readonly AppDbContext _context;
        private readonly ILogger<SubChapterIntroManager> _logger;

        public SubChapterIntroManager(AppDbContext context, ILogger<SubChapterIntroManager> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ManagerResult<SubChapterIntro>> CreateOrUpdate(SubChapterIntro subchapter)
        {
            var manager = new ManagerResult<SubChapterIntro>();
            try
            {
                if (subchapter.Id == Guid.Empty)
                {
                    await _context.AddAsync(subchapter);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var oldChapter = await _context.SubchIntros.FindAsync(subchapter.Id);
                    oldChapter.Content = subchapter.Content;
                    oldChapter.Created = DateTime.UtcNow;
                    oldChapter.Name = subchapter.Name;
                    oldChapter.SubChapter = subchapter.SubChapter;
                    oldChapter.SubChapterId = subchapter.SubChapterId;
                    oldChapter.Images = subchapter.Images;
                }
                manager.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.GetBaseException().Message);
                manager.Message = ex.GetBaseException().Message;
            }
            return manager;
        }
        public async Task<ManagerResult> Remove(Guid id)
        {
            var manager = new ManagerResult();
            try
            {
                var chapter = await _context.SubchIntros.FindAsync(id);
                _context.Remove(chapter);
                await _context.SaveChangesAsync();
                manager.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.GetBaseException().Message);
                manager.Message = ex.GetBaseException().Message;
            }
            return manager;
        }
        public async Task<ManagerResult<List<SubChapterIntro>>> GetAll()
        {
            var manager = new ManagerResult<List<SubChapterIntro>>();
            try
            {
                var chapter = await _context.SubchIntros.Include(x => x.SubChapter.Chapter.Tutorial.Category).ToListAsync();
                if (chapter != null)
                {
                    manager.Data = chapter;
                    manager.Success = true;
                }
                else
                {
                    manager.Success = false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.GetBaseException().Message);
                manager.Message = ex.GetBaseException().Message;
            }
            return manager;
        }
        public async Task<ManagerResult<SubChapterIntro>> Get(Guid id)
        {
            var manager = new ManagerResult<SubChapterIntro>();
            try
            {
                var chapter = await _context.SubchIntros.FindAsync(id);
                if (chapter != null)
                {
                    manager.Data = chapter; manager.Success = true;
                }
                else
                {
                    manager.Success = false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.GetBaseException().Message);
                manager.Message = ex.GetBaseException().Message;
            }
            return manager;
        }
    }
}
