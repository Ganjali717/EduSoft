using EduSoft.Data.DatabaseContext;
using EduSoft.Data.Managers.Interfaces;

namespace EduSoft.Data.Managers.Services;

public class AccountManager:IAccountManager
{
    private readonly AppDbContext _context;

    public AccountManager(AppDbContext context)
    {
        _context = context;
    }

    
}