using BackEnd.Posts.Domain.Model.Aggregates;
using BackEnd.Posts.Domain.Model.Queries;
using BackEnd.Posts.Domain.Repositories;
using BackEnd.Posts.Domain.Services;

namespace BackEnd.Posts.Application.Internal.QueryServices;

public class PostQueryService(IPostRepository postRepository) : IPostQueryService
{
    public async Task<IEnumerable<Post>> Handle(GetAllPostsQuery query)
    {
        return await postRepository.FindAllAsync();
    }

    public async Task<Post> Handle(GetPostByIdQuery query)
    {
        var post = await postRepository.FindByIdAsync(query.id);
        if (post == null)
        {
            throw new Exception("Post not found.");
        }

        return post;
    }

    public async Task<IEnumerable<Post>> Handle(GetPostByDishIdQuery query)
    {
        return await postRepository.FindByDishIdAsync(query.dishId);
    }
}