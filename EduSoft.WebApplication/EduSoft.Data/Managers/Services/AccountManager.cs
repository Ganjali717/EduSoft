using EduSoft.Data.DatabaseContext;
using EduSoft.Data.Managers.Interfaces;

namespace EduSoft.Data.Managers.Services;

public class AccountManager:IAccountManager
{
    private readonly AppDbContext context;

    public AccountManager(AppDbContext _context)
    {
        _context = context;
    }
}