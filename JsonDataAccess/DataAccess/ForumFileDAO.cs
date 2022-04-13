using Blazor.Services;
using Domain.Models;

namespace JsonDataAccess.DataAccess;

public class ForumFileDAO : IForumHome
{
    private FileContext fileContext;

    public ForumFileDAO(FileContext fileContext)
    {
        this.fileContext = fileContext;
    }

    public async Task<ICollection<Forum>> GetAsync()
    {
        ICollection<Forum> forums = fileContext.Forums;
        return forums;
    }

    public async Task<Forum> GetByIdAsync(int id)
    {
        return fileContext.Forums.First(t => t.Id == id);
    }

    public async Task<Forum> AddAsync(Forum forum)
    {
        int largestId = fileContext.Forums.Max(t => t.Id);
        int nextId = largestId + 1;
        forum.Id = nextId;
        fileContext.Forums.Add(forum);
        fileContext.SaveChanges();
        return forum;
    }

    public async Task DeleteAsync(int id)
    {
        Forum toDelete = fileContext.Forums.First(t => t.Id == id);
        fileContext.Forums.Remove(toDelete);
        fileContext.SaveChanges();
    }

    

    public Task UpdateAsync(Forum forum)
    {
        Forum toUpdate = fileContext.Forums.First(t => t.Id == forum.Id);
        toUpdate.OwnerId = forum.OwnerId;
        toUpdate.Title = forum.Title;
        toUpdate.Comment = forum.Comment;
        fileContext.SaveChanges();
        return Task.CompletedTask;
    }
}