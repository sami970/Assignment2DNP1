using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class PostUser
{
    [Required, MaxLength(64)] public string UserName { get;  set; }
    [Required, MaxLength(64)] public string Password { get;  set; }

    public PostUser(String userName, String password)
    {
        this.UserName = userName;
        this.Password = password;
    }
}