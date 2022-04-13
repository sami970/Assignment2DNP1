using System.ComponentModel.DataAnnotations;
namespace Domain.Models;

public class Forum
{
    public int Id { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
    public int OwnerId { get; set; }
    [ MaxLength(64)] public string Title { get; set; }
    public string Comment { get; set; }

    public Forum(int ownerId, string title)
    {
        OwnerId = ownerId;
        Title = title;
    }

    public Forum()
    {
        
    }
}