﻿using EduSoft.Data.DatabaseContext;
using EduSoft.Data.Managers.Interfaces;
using EduSoft.Entities;
using EduSoft.Entities.Extentions;
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
    public async Task<ManagerResult<AppUser>> GetUserById(Guid id)
    {
        var result = new ManagerResult<AppUser>();
        try
        {
            var user = await _context.AppUsers.FindAsync(id);
            if (user != null) { result.Data = user; result.Success = true; }
        }
        catch (Exception ex)
        {
            result.Message = ex.GetBaseException().Message;
        }
        return result;
    }
    public async Task<ManagerResult> RemoveUser(Guid id)
    {
        var result = new ManagerResult();
        try
        {
            var user = await _context.AppUsers.FindAsync(id);
            if (user != null)
            {
                _context.AppUsers.Remove(user);
                await _context.SaveChangesAsync();
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
    public async Task<ManagerResult<AppUser>> GetUserBySecurityToken(string token)
    {
        var result = new ManagerResult<AppUser>();
        try
        {
            var user = await _context.AppUsers.FirstOrDefaultAsync(x => x.SecurityToken == token);
            if (user != null)
            {
                result.Data = user; 
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
    public async Task<ManagerResult<AppUser>> Login(string email, string password)
    {
        var result = new ManagerResult<AppUser>();
        try
        {
            var hash = GetPasswordHash(email, password);
            var account = await _context.AppUsers.FirstOrDefaultAsync(x => x.Email == email && x.Password == hash);
            if (account == null)
            {
                result.Success = false;
                result.Message = "User Name or Password isn't correct.";
                return result;
            }
            result.Success = true;
            result.Data = account;
        }
        catch (Exception ex)
        {
            result.Message = ex.GetBaseException().Message;
        }
        return result;
    }
    private string? GetPasswordHash(string? email, string? password)
    {
        var salt = email.GetSHA256Hash();
        return (password + salt).GetSHA256Hash();
    }
}