using AssignmentDNP1.Services;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcData;

public class UserDao:IUserService
{
    private readonly PostContext context;

    public UserDao(PostContext context)
    {
        this.context = context;
    }

    public Task<PostUser?> GetUserAsync(string username)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<PostUser>> GetAsync()
    {
        ICollection<PostUser> users = await context.users.ToListAsync();
        return users;
    }

    public async Task<PostUser> AddAsync(PostUser user)
    {
        EntityEntry<PostUser> added = await context.AddAsync(user);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public Task<PostUser?> AddUserAsync(string username, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<PostUser> GetByNamePasswordAsync(string userName, string password)
    {
        PostUser? userfound = await context.users.FindAsync(userName,password);
        
        if (userfound == null)
        {
            userfound = new PostUser("", "");
        }
        return userfound;
    }
}