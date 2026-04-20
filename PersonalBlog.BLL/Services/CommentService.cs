using PersonalBlog.BLL.Abstractions;
using PersonalBlog.BLL.Commands.Comments;
using PersonalBlog.BLL.Exceptions;
using PersonalBlog.DAL.Entities;
using PersonalBlog.DAL.Repositories.Interfaces;
using PersonalBlog.DAL.UnitOfWork;

namespace PersonalBlog.BLL.Services;

public class CommentService : ICommentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postRepository;

    public CommentService(IUnitOfWork unitOfWork, ICommentRepository commentRepository, IPostRepository postRepository)
    {
        _unitOfWork = unitOfWork;
        _commentRepository = commentRepository;
        _postRepository = postRepository;
    }

    public async Task<Guid> AddCommentAsync(AddCommentCommand command)
    {
        var post = await _postRepository.GetByIdAsync(command.PostId);
        if (post == null)
        {
            throw new KeyNotFoundException($"Post with ID {command.PostId} not found.");
        }
        
        var comment = CreateComment(command.Text, command.PostId, command.CurrentUserId);
        
        var result = await _commentRepository.AddAsync(comment);
        await _unitOfWork.SaveChangesAsync();
        
        return result;
    }

    public async Task DeleteCommentAsync(DeleteCommentCommand command)
    {
        var comment = await _commentRepository.GetByIdAsync(command.Id);
        
        if (comment == null)
            return;
        
        if (comment.UserId == null || comment.UserId != command.CurrentUserId)
            throw new ForbiddenAccessException("You do not have permission to delete this comment.");
        
        _commentRepository.Delete(comment);
        await _unitOfWork.SaveChangesAsync();
    }

    private static Comment CreateComment(string text, Guid postId, Guid authorId)
    {
        return new Comment
        {
            UserId = authorId,
            Text = text,
            PostId = postId,
        };
    }
}