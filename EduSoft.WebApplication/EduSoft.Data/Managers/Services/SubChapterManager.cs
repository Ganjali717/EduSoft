﻿using EduSoft.Data.DatabaseContext;
using EduSoft.Entities;
using EduSoft.Entities.Tutorials;
using Microsoft.EntityFrameworkCore;

namespace EduSoft.Data.Managers.Services;

public class SubChapterManager
{
    private readonly AppDbContext _context; 
    public SubChapterManager(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ManagerResult<List<Subchapter>>> GetAllSubChapter()
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
}