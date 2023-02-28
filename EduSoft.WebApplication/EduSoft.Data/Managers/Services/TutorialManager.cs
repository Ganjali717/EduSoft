using AutoMapper;
using EduSoft.Data.DatabaseContext;
using EduSoft.Entities.Tutorials;
using EduSoft.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EduSoft.Data.Managers.Services
{
    public class TutorialManager
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public TutorialManager(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ManagerResult<List<Tutorial>>> GetAllTutorials()
        {
            var result = new ManagerResult<List<Tutorial>>();
            try
            {
                var context = await _context.Tutorials.ToListAsync();
                result.Data = context;
                result.Success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                result.Message = ex.GetBaseException().Message;
            }

            return result;
        }

        public async Task<ManagerResult<Tutorial>> GetTutorial(Guid id)
        {
            var result = new ManagerResult<Tutorial>();
            try
            {
                var tutorialbyId = await _context.Tutorials.FindAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
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
                Console.WriteLine(ex);
                result.Message = ex.GetBaseException().Message;
            }
            return result;
        }
    }
}
