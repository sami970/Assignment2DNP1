using System.Text;
using System.Text.Json;
using AssignmentDNP1.Services;
using Domain.Models;

namespace HttpServices;

public class UserHttpClient : IUserService
{
    public Task<PostUser?> GetUserAsync(string username)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<PostUser>> GetAsync()
    {
        throw new NotImplementedException();
    }

    public Task<PostUser> AddAsync(PostUser user)
    {
        throw new NotImplementedException();
    }

    public async Task<PostUser?> AddUserAsync(string username, string password)
    {
        using HttpClient client = new();
        PostUser user = new PostUser(username, password);
        string postAsJson = JsonSerializer.Serialize(user);

        StringContent content = new(postAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync($"https://localhost:7254/User/{username}/{password} ", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {responseContent}");
        }
        
        PostUser returned = JsonSerializer.Deserialize<PostUser>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        
        return returned;
    }

    public async Task<PostUser> GetByNamePasswordAsync(string userName, string password)
    {
        using HttpClient client = new ();
        HttpResponseMessage response = await client.GetAsync($"https://localhost:7254/User/{userName}/{password}");
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }
        
        PostUser user = JsonSerializer.Deserialize<PostUser>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return user; 
    }
}