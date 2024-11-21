using BackEnd.Posts.Domain.Model.Aggregates;
using BackEnd.Posts.Domain.Model.Queries;

namespace BackEnd.Posts.Domain.Services;

public interface IPostQueryService
{
    Task<IEnumerable<Post>> Handle(GetAllPostsQuery query);
    Task<Post> Handle(GetPostByIdQuery query);
    Task<IEnumerable<Post>> Handle(GetPostByDishIdQuery query);
}