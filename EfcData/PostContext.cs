using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EfcData;

public class PostContext:DbContext
{
    public DbSet<Forum> posts { get; set; }
    public DbSet<PostUser> users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"Data Source = C:\Software Technology Engineering\3rd Semester\DNP1\AssignmentDNP1\EfcData\Post.db");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Forum>().HasKey(post => post.Id);
        modelBuilder.Entity<PostUser>().HasKey(users => users.UserName);
    }
    
    public void SeedPost()
    {
        
        if (posts.Any()) return;
        
        Forum[] ts =
        {
            new Forum(1, "pass the ball")
            {
                Id = 1,
                OwnerId = 1,
                Comment = "good",
            },
            new Forum(2, "shoot the ball")
            {
                Id = 2,
                OwnerId = 2,
                Comment = "bad",
            },
            new Forum(3, "defend the attacks")
            {
                Id = 3,
                OwnerId = 3,
                Comment = "good",
            },
        };
        posts.AddRange(ts);
        SaveChanges();
    }
    
    public void SeedUser()
    {
        if (users.Any()) return;

        PostUser[] ts =
        {
            new PostUser("Sami", "1234")
            {
                UserName = "Sami",
                Password = "1234",
            }
        };
        users.AddRange(ts);
        SaveChanges();

    }
    
}