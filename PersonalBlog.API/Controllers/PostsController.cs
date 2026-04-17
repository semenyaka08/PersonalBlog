using Microsoft.AspNetCore.Mvc;
using PersonalBlog.API.RequestModels.Posts;
using PersonalBlog.BLL.Abstractions;
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
    public async Task<IActionResult> GetPosts()
    {
        var query = new GetAllPostsQuery();
        var posts = await _postService.GetPostsAsync(query);
        
        return Ok(posts);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPost(Guid id)
    {
        var query = new GetPostByIdQuery(id);
        var post = await _postService.GetPostAsync(query);
        
        return Ok(post);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostRequest request)
    {
        var command = new CreatePostCommand(request.Title, request.Content);
        
        var post = await _postService.CreatePostAsync(command);
        return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdatePost(Guid id, [FromBody] UpdatePostRequest request)
    {
        var command = new UpdatePostCommand(id, request.Title, request.Content);
        
        await _postService.UpdatePostAsync(command);
        return NoContent(); 
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePost(Guid id)
    {
        var command = new DeletePostCommand(id);
        await _postService.DeletePostAsync(command);
        
        return NoContent();
    }
}