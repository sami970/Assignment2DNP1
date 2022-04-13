using AssignmentDNP1.Services;
using Blazor.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class UserController :ControllerBase
{
    private IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<PostUser>>> GetAll()
    {
        try
        {
            ICollection<PostUser> users = await userService.GetAsync();

            return Ok(users);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    [HttpPost]
    [Route("{username}/{password}")]
    public async Task<ActionResult<PostUser>> AddUser([FromRoute] string username,string password)
    {
        try
        {
            PostUser added =  await userService.AddUserAsync(username,password);
            return Created($"/forums/{added.UserName}", added);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    /*[HttpPost]
    public async Task<ActionResult<PostUser>> AddPost([FromBody] PostUser user)
    {
        try
        {
            PostUser added = await userService.AddAsync(user);
            return Created($"/forums/{added.UserName}", added);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }*/

  
    [HttpGet]
    [Route("{username}/{password}")]
    public async Task<ActionResult<PostUser>> GetPostByUsernamePassword([FromRoute] String username,String password)
    {
        try
        {
            PostUser postUser = await userService.GetByNamePasswordAsync(username,password);
            return postUser;
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    } 
    
}