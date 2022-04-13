using System.Security.Claims;
using System.Text.Json;
using AssignmentDNP1.Services;
using Domain.Models;
using Microsoft.JSInterop;

namespace AssignmentDNP1.Authentication;

public class AuthServiceImpl : IAuthService
{
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!; // assigning to null! to suppress null warning.
    private readonly IUserService userService;
    private readonly IJSRuntime jsRuntime;
    public static PostUser currentUser;

    public AuthServiceImpl(IUserService userService, IJSRuntime jsRuntime)
    {
        this.userService = userService;
        this.jsRuntime = jsRuntime;
    }

    public async Task LoginAsync(string username, string password)
    {
        PostUser? postuser = await userService.GetByNamePasswordAsync(username,password); // Get user from database
        
        ValidateLoginCredentials(password, postuser); // Validate input data against data from database
        // validation success
        await CacheUserAsync(postuser!); // Cache the user object in the browser 

        ClaimsPrincipal principal = CreateClaimsPrincipal(postuser); // convert user object to ClaimsPrincipal

        OnAuthStateChanged?.Invoke(principal); // notify interested classes in the change of authentication state
        currentUser = postuser;
      
    }

    public async Task LogoutAsync()
    {
        await ClearUserFromCacheAsync(); // remove the user object from browser cache
        ClaimsPrincipal principal = CreateClaimsPrincipal(null); // create a new ClaimsPrincipal with nothing.
        OnAuthStateChanged?.Invoke(principal); // notify about change in authentication state
        currentUser = null;
    }

    public async Task<ClaimsPrincipal> GetAuthAsync() // this method is called by the authentication framework, whenever user credentials are reguired
    {
        PostUser? user =  await GetUserFromCacheAsync(); // retrieve cached user, if any

        ClaimsPrincipal principal = CreateClaimsPrincipal(user); // create ClaimsPrincipal

        return principal;
    }

    private async Task<PostUser?> GetUserFromCacheAsync()
    {
        string userAsJson = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
        if (string.IsNullOrEmpty(userAsJson)) return null;
        PostUser user = JsonSerializer.Deserialize<PostUser>(userAsJson)!;
        return user;
    }

    private static void ValidateLoginCredentials(string password, PostUser? user)
    {
        if (user == null)
        {
            throw new Exception("Username not found");
        }

        if (!string.Equals(password,user.Password))
        {
            throw new Exception("Password incorrect");
        }
    }

    private static ClaimsPrincipal CreateClaimsPrincipal(PostUser? user)
    {
        if (user != null)
        {
            ClaimsIdentity identity = ConvertUserToClaimsIdentity(user);
            return new ClaimsPrincipal(identity);
        }

        return new ClaimsPrincipal();
    }

    private async Task CacheUserAsync(PostUser user)
    {
        string serialisedData = JsonSerializer.Serialize(user);
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serialisedData);
    }

    private async Task ClearUserFromCacheAsync()
    {
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
    }

    private static ClaimsIdentity ConvertUserToClaimsIdentity(PostUser user)
    {
        // here we take the information of the User object and convert to Claims
        // this is (probably) the only method, which needs modifying for your own user type
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.Name, user.UserName),
           
        };

        return new ClaimsIdentity(claims, "apiauth_type");
    }

    public static PostUser getCurrentUser()
    {
        
        return currentUser;
    } 
    
    public async Task AddUser(string username,string password)
    {
        Task<PostUser?> users =  userService.AddUserAsync(username,password); // Get user from database
       
        //return users;
    }
    
   
    
}