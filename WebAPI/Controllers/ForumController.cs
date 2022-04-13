using Blazor.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ForumController : ControllerBase
{
    private IForumHome forumHome;

    public ForumController(IForumHome forumHome)
    {
        this.forumHome = forumHome;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Forum>>> GetAll()
    {
        try
        {
            ICollection<Forum> forums = await forumHome.GetAsync();

            return Ok(forums);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Forum>> AddPost([FromBody] Forum forum)
    {
        try
        {
            Forum added = await forumHome.AddAsync(forum);
            return Created($"/forums/{added.Id}", added);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Forum>> GetPostById([FromRoute] int id)
    {
        try
        {
            Forum forum = await forumHome.GetByIdAsync(id);
            return forum;
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult> DeletePostById([FromRoute] int id)
    {
        try
        {
            await forumHome.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        } 
    }

    [HttpPatch]
    public async Task<ActionResult> Update([FromBody] Forum forum)
    {
        try
        {
            await forumHome.UpdateAsync(forum);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }  
    }
}