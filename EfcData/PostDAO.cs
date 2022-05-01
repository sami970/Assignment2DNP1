using Blazor.Services;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcData;

public class PostDAO:IForumHome
{

    private readonly PostContext context;

    public PostDAO(PostContext context)
    {
        this.context = context;
    }

    public async Task<ICollection<Forum>> GetAsync()
    {
        ICollection<Forum> posts = await context.posts.ToListAsync();
        return posts;
    }

    public async Task<Forum> GetByIdAsync(int id)
    {
        Forum? postfound = await context.posts.FindAsync(id);
        
        if (postfound == null)
        {

            throw new Exception("Post not found");
        }
        return postfound;
    }

    public async Task<Forum> AddAsync(Forum post)
    {
        EntityEntry<Forum> added = await context.AddAsync(post);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task DeleteAsync(int id)
    {
        Forum? existing = await context.posts.FindAsync(id);
        if (existing is null)
        {
            throw new Exception($"Could not find post with id {id}. Nothing was deleted");
        }

        context.posts.Remove(existing);
        await context.SaveChangesAsync();
    }

    public Task UpdateAsync(Forum forum)
    {
        context.posts.Update(forum);
        return context.SaveChangesAsync();
    }
}