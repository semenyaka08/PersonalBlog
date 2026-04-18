using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.API.Extensions;
using PersonalBlog.API.RequestModels.Comments;
using PersonalBlog.BLL.Abstractions;
using PersonalBlog.BLL.Commands.Comments;
using PersonalBlog.BLL.Constants;

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
    [Authorize(Policy = AppPolicies.CanWriteComments)]
    public async Task<IActionResult> AddComment(Guid postId, [FromBody] CreateCommentRequest request)
    {
        var command = new AddCommentCommand(postId, request.Text);
        var commentId = await _commentService.AddCommentAsync(command);
        
        return Ok(new { Id = commentId });
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = AppPolicies.CanWriteComments)]
    public async Task<IActionResult> DeleteComment(Guid id)
    {
        var userId = User.GetUserId();
        var deleteCommentCommand = new DeleteCommentCommand(id, userId);
        
        await _commentService.DeleteCommentAsync(deleteCommentCommand);
        
        return NoContent();
    }
}