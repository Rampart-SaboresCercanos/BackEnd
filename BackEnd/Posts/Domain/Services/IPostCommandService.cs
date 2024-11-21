using BackEnd.Posts.Domain.Model.Aggregates;
using BackEnd.Posts.Domain.Model.Command;

namespace BackEnd.Posts.Domain.Services;

public interface IPostCommandService
{
    Task<Post?> Handle(CreatePostCommand command);
    
    Task<Post?> Handle(UpdatePostCommand command);
    
    Task<bool> DeletePostAsync(int id);
}