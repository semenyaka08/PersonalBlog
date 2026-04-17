using Microsoft.AspNetCore.Mvc;
using PersonalBlog.API.RequestModels.Comments;
using PersonalBlog.BLL.Abstractions;
using PersonalBlog.BLL.Commands.Comments;

namespace PersonalBlog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost("post/{postId:guid}")]
    public async Task<IActionResult> AddComment(Guid postId, [FromBody] CreateCommentRequest request)
    {
        var command = new AddCommentCommand(postId, request.Text);
        var commentId = await _commentService.AddCommentAsync(command);
        
        return Ok(new { Id = commentId });
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteComment(Guid id)
    {
        await _commentService.DeleteCommentAsync(id);
        
        return NoContent();
    }
}