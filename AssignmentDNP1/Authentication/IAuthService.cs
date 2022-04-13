using System.Security.Claims;


namespace AssignmentDNP1.Authentication;

public interface IAuthService
{
    public Task LoginAsync(string username, string password);
    public Task LogoutAsync();
    public Task<ClaimsPrincipal> GetAuthAsync();

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
    public Task AddUser(string username, string password);

  
}