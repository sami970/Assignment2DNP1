using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Blazor.Services;

public interface IForumHome
{
    public Task<ICollection<Forum>> GetAsync();
    public Task<Forum> GetByIdAsync(int id);
    public Task<Forum> AddAsync(Forum todo);
    public Task DeleteAsync(int id);
    public Task UpdateAsync(Forum forum);
}