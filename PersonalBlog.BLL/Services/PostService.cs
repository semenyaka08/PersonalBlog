using PersonalBlog.BLL.Abstractions;
using PersonalBlog.BLL.Dtos.Posts;
using PersonalBlog.BLL.Exceptions;
using PersonalBlog.BLL.Mappers;
using PersonalBlog.BLL.Queries.Comments;
using PersonalBlog.BLL.Queries.Posts;
using PersonalBlog.DAL.Entities;
using PersonalBlog.DAL.Repositories.Interfaces;
using PersonalBlog.DAL.UnitOfWork;

namespace PersonalBlog.BLL.Services;

public class PostService : IPostService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPostRepository _postRepository;

    public PostService(IUnitOfWork unitOfWork, IPostRepository postRepository)
    {
        _unitOfWork = unitOfWork;
        _postRepository = postRepository;
    }
    
    public async Task<IEnumerable<PostDto>> GetPostsAsync(GetAllPostsQuery query)
    {
        var posts = await _postRepository.GetAllAsync();
        
        return posts.Select(p => p.ToDto());
    }

    public async Task<PostDto> GetPostAsync(GetPostByIdQuery query)
    {
        var post = await _postRepository.GetByIdAsync(query.Id);
        
        if (post == null)
            throw new KeyNotFoundException($"Post with id {query.Id} not found.");
            
        return post.ToDto();
    }

    public async Task<PostDto> CreatePostAsync(CreatePostCommand command)
    {
        var post = CreatePost(command.Title, command.Content, command.CurrentUserId);
        
        await _postRepository.AddAsync(post);
        await _unitOfWork.SaveChangesAsync();
        
        return post.ToDto();
    }

    public async Task UpdatePostAsync(UpdatePostCommand command)
    {
        var post = await _postRepository.GetByIdAsync(command.Id);
        
        if (post == null)
            throw new KeyNotFoundException($"Post with id {command.Id} not found.");
        
        if (post.AuthorId != command.CurrentUserId)
        {
            throw new ForbiddenAccessException("You do not have permission to edit this post.");
        }
        
        post.Title = command.Title;
        post.Content = command.Content;
        
        _postRepository.Update(post);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeletePostAsync(DeletePostCommand command)
    {
        var post = await _postRepository.GetByIdAsync(command.Id);
        
        if (post == null)
            return;
        
        if (post.AuthorId != command.CurrentUserId)
        {
            throw new ForbiddenAccessException("You do not have permission to edit this post.");
        }
        
        _postRepository.Delete(post);
        await _unitOfWork.SaveChangesAsync();
    }

    private static Post CreatePost(string title, string content, Guid authorId)
    {
        return new Post { Title = title, Content = content, AuthorId = authorId };
    }
}