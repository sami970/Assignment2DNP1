
using Domain.Models;

namespace AssignmentDNP1.Services;

public interface IUserService
{
    public Task<PostUser?> GetUserAsync(string username);
    public Task<ICollection<PostUser>> GetAsync();
    public Task<PostUser> AddAsync(PostUser user);
    public Task<PostUser?> AddUserAsync(string username, string password);
    public Task<PostUser> GetByNamePasswordAsync(string userName,string password);
}