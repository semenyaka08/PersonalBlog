using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.API.Extensions;
using PersonalBlog.API.RequestModels.Posts;
using PersonalBlog.BLL.Abstractions;
using PersonalBlog.BLL.Constants;
using PersonalBlog.BLL.Queries.Comments;
using PersonalBlog.BLL.Queries.Posts;

namespace PersonalBlog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostService _postService;

    public PostsController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetPosts()
    {
        var query = new GetAllPostsQuery();
        var posts = await _postService.GetPostsAsync(query);
        
        return Ok(posts);
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPost(Guid id)
    {
        var query = new GetPostByIdQuery(id);
        var post = await _postService.GetPostAsync(query);
        
        return Ok(post);
    }

    [HttpPost]
    [Authorize(Policy = AppPolicies.CanWritePosts)]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostRequest request)
    {
        var currentUserId = User.GetUserId();
        var command = new CreatePostCommand(request.Title, request.Content, currentUserId);
        
        var post = await _postService.CreatePostAsync(command);
        return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = AppPolicies.CanWritePosts)]
    public async Task<IActionResult> UpdatePost(Guid id, [FromBody] UpdatePostRequest request)
    {
        var currentUserId = User.GetUserId();
        var command = new UpdatePostCommand(id, request.Title, request.Content, currentUserId);
        
        await _postService.UpdatePostAsync(command);
        return NoContent(); 
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = AppPolicies.CanWritePosts)]
    public async Task<IActionResult> DeletePost(Guid id)
    {
        var currentUserId = User.GetUserId();
        var command = new DeletePostCommand(id, currentUserId);
        
        await _postService.DeletePostAsync(command);
        
        return NoContent();
    }
}