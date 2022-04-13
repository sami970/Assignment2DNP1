using System.Text.Json;
using Domain.Models;

namespace JsonDataAccess.DataAccess;

public class PostUserFileContext
{
    private string userFilePath = "users.json";

    private List<PostUser>? users;

    public List<PostUser> Users
    {
        get
        {
            if (users == null)
            {
                LoadData();
            }

            return users!;
        }
       
    }

    public PostUserFileContext()
    {
        if (!File.Exists(userFilePath))
        {
            Seed();
        }
    }

    private void Seed()
    {
        users = new List<PostUser>();
        
        PostUser us = new PostUser("Sami", "1234");
        users.Add(us);

        SaveChanges();
    }

    public void SaveChanges()
    {
        string serialize = JsonSerializer.Serialize(Users);
        File.WriteAllText(userFilePath, serialize);
        users = null;
    }

    public void addUser(PostUser user)
    {
        Users.Add(user);
        this.SaveChanges();
    }
    private void LoadData()
    {
        string content = File.ReadAllText(userFilePath);
        users = JsonSerializer.Deserialize<List<PostUser>>(content);
    }
}