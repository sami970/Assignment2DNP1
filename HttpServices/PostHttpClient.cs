using System.Text;
using System.Text.Json;
using Blazor.Services;
using Domain.Models;

namespace HttpServices;

public class PostHttpClient : IForumHome
{
    public async Task<ICollection<Forum>> GetAsync()
    {
        using HttpClient client = new ();
        HttpResponseMessage response = await client.GetAsync("https://localhost:7254/Forum");
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        ICollection<Forum> posts = JsonSerializer.Deserialize<ICollection<Forum>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return posts;
    }

    public async Task<Forum> GetByIdAsync(int id)
    {
        using HttpClient client = new ();
        HttpResponseMessage response = await client.GetAsync($"https://localhost:7254/Forum/{id}");
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }
        
        Forum post = JsonSerializer.Deserialize<Forum>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return post;
    }
    public async Task<Forum> AddAsync(Forum post)
    {
        using HttpClient client = new();

        string postAsJson = JsonSerializer.Serialize(post);

        StringContent content = new(postAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync($"https://localhost:7254/Forum", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {responseContent}");
        }
        
        Forum returned = JsonSerializer.Deserialize<Forum>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        
        return returned;
    }

    public async Task DeleteAsync(int id)
    {
        using HttpClient client = new();
        HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7254/Forum/{id}");
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }
    }

    public async Task UpdateAsync(Forum forum)
    {
        using HttpClient client = new();

        string postAsJson = JsonSerializer.Serialize(forum);

        StringContent content = new(postAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PatchAsync("https://localhost:7254/Forum", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {responseContent}");
        }
    }
}