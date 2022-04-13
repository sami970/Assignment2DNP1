
using AssignmentDNP1.Services;

using Domain.Models;


namespace JsonDataAccess.DataAccess;

// dummy database 
public class UserService : IUserService
{
    private List<PostUser> users; //= new();
    private PostUserFileContext postUserFileContext;
    
    public UserService(PostUserFileContext postUserFileContext)
    {
        this.postUserFileContext = postUserFileContext;
        createUser();
    }
    
    public Task<PostUser?> GetUserAsync(string username)
    {
        if (users.Count()== 0)
        {
            createUser();
        }

        PostUser find = users.Find(user => user.UserName.Equals(username));
        return Task.FromResult(find);
    }

    public async Task<ICollection<PostUser>> GetAsync()
    {
        ICollection<PostUser> users = postUserFileContext.Users;
        return users;
    }

    public Task<PostUser> AddAsync(PostUser user)
    {
        throw new NotImplementedException();
    }


    private void  createUser()
    {
        PostUserFileContext fl = new PostUserFileContext();
        users = fl.Users;
       // User us = new User("Sami", "1234");
        //users.Add(us);
    }
    
    public  Task<PostUser?> AddUserAsync(string username,string password)
    {
        PostUser us = new PostUser(username,password); // Get user from database
        users.Add(us);
        
        
        postUserFileContext.addUser(us);
        
        
        PostUser find = users.Find(user => user.UserName.Equals(username));
        return Task.FromResult(find);
       
    }

    public Task<PostUser> GetByNamePasswordAsync(string userName, string password)
    {
      
        PostUser userfound = users.Find(user => user.UserName.Equals(userName) && user.Password.Equals(password));
        if (userfound == null)
        {
            userfound = new PostUser("", "");
        }
        return Task.FromResult(userfound);
    }
}