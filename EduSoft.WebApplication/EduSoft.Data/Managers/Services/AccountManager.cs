using EduSoft.Data.DatabaseContext;
using EduSoft.Data.Managers.Interfaces;
using EduSoft.Entities;
using EduSoft.Entities.Security;
using Microsoft.EntityFrameworkCore;

namespace EduSoft.Data.Managers.Services;

public class AccountManager:IAccountManager
{
    private readonly AppDbContext _context;

    public AccountManager(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ManagerResult<List<AppUser>>> GetAllUsers()
    {
        var result = new ManagerResult<List<AppUser>>();
        try
        {
            var allUsers = await _context.AppUsers.ToListAsync();
            if (allUsers != null)
            {
                result.Data = allUsers;
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
}