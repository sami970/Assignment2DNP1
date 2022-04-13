using System.Text.Json;
using Domain.Models;

namespace JsonDataAccess.DataAccess;

public class FileContext
{
    private string forumFilePath = "forums.json";

    private ICollection<Forum>? forums;

    public ICollection<Forum> Forums
    {
        get
        {
            if (forums == null)
            {
                LoadData();
            }

            return forums!;
        }
    }

    public FileContext()
    {
        if (!File.Exists(forumFilePath))
        {
            Seed();
        }
    }

    private void Seed()
    {
        Forum[] ts =
        {
            new Forum(1, "pass the ball")
            {
                Id = 1,
            },
            new Forum(2, "shoot the ball")
            {
                Id = 2,
            },
            new Forum(3, "defend the attacks")
            {
                Id = 3,
            },
        };
        forums = ts.ToList();
        SaveChanges();
    }

    public void SaveChanges()
    {
        string serialize = JsonSerializer.Serialize(Forums);
        File.WriteAllText(forumFilePath, serialize);
        forums = null;
    }

    private void LoadData()
    {
        string content = File.ReadAllText(forumFilePath);
        forums = JsonSerializer.Deserialize<List<Forum>>(content);
    }
}