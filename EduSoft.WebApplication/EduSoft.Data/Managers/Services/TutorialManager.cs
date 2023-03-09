using AutoMapper;
using EduSoft.Data.DatabaseContext;
using EduSoft.Entities.Tutorials;
using EduSoft.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduSoft.Data.Managers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EduSoft.Data.Managers.Services
{
    public class TutorialManager:ITutorialManager
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TutorialManager> _logger;
        public TutorialManager(AppDbContext context, ILogger<TutorialManager> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ManagerResult<List<Tutorial>>> GetAllTutorials()
        {
            var result = new ManagerResult<List<Tutorial>>();
            try
            {
                var context = await _context.Tutorials.ToListAsync();
                if (context != null)
                {
                    result.Data = context;
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
        public async Task<ManagerResult<Tutorial>> GetTutorial(Guid id)
        {
            var result = new ManagerResult<Tutorial>();
            try
            {
                var tutorial = await _context.Tutorials.FindAsync(id);
                if (tutorial != null)
                {
                    result.Data = tutorial;
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
        public async Task<ManagerResult<Tutorial>> CreateOrUpdateTutorial(Tutorial tutorial)
        {
            var result = new ManagerResult<Tutorial>();
            try
            {
                if (tutorial.Id == Guid.Empty)
                {
                    await _context.Tutorials.AddAsync(tutorial);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var oldTutorial = await _context.Tutorials.FindAsync(tutorial.Id); 
                    oldTutorial.Title = tutorial.Title;
                    oldTutorial.Description = tutorial.Description;
                    oldTutorial.CategoryId = tutorial.CategoryId;
                    oldTutorial.Category = tutorial.Category;
                    oldTutorial.Created = DateTime.Now;
                    oldTutorial.Chapters = tutorial.Chapters;
                    await _context.SaveChangesAsync();
                    result.Data = oldTutorial;
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
        public async Task<ManagerResult> DeleteTutorial(Guid id)
        {
            var result = new ManagerResult();
            try
            {
                var tutorial = await _context.Tutorials.FindAsync(id);
                if (tutorial != null)
                {
                    _context.Tutorials.Remove(tutorial);
                    await _context.SaveChangesAsync();
                    result.Success = true;
                }
                result.Success = false;
                result.Message = "Tutorial with this Id not found";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.GetBaseException().Message);
                result.Message = ex.GetBaseException().Message;
            }
            return result;
        }
    }
}
