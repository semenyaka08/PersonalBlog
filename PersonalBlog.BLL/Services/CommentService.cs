using PersonalBlog.BLL.Abstractions;
using PersonalBlog.BLL.Commands.Comments;
using PersonalBlog.BLL.Dtos.Comments;
using PersonalBlog.DAL.Entities;
using PersonalBlog.DAL.Repositories.Interfaces;
using PersonalBlog.DAL.UnitOfWork;

namespace PersonalBlog.BLL.Services;

public class CommentService : ICommentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICommentRepository _commentRepository;

    public CommentService(IUnitOfWork unitOfWork, ICommentRepository commentRepository)
    {
        _unitOfWork = unitOfWork;
        _commentRepository = commentRepository;
    }

    public async Task<Guid> AddCommentAsync(AddCommentCommand command)
    {
        var comment = CreateComment(command.Text, command.PostId);
        
        var result = await _commentRepository.AddAsync(comment);
        await _unitOfWork.SaveChangesAsync();
        
        return result;
    }

    public async Task DeleteCommentAsync(Guid commentId)
    {
        var comment = await _commentRepository.GetByIdAsync(commentId);
        
        if (comment == null)
            return;
        
        _commentRepository.Delete(comment);
        await _unitOfWork.SaveChangesAsync();
    }

    private static Comment CreateComment(string text, Guid postId)
    {
        return new Comment
        {
            Text = text,
            PostId = postId,
        };
    }
}