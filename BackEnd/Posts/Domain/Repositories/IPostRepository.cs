using BackEnd.Posts.Domain.Model.Aggregates;
using BackEnd.Shared.Domain.Repositories;

namespace BackEnd.Posts.Domain.Repositories;

public interface IPostRepository : IBaseRepository<Post>
{
    Task<IEnumerable<Post>> FindAllAsync();
    Task<Post?> FindByIdAsync(int id);
    Task<IEnumerable<Post>> FindByDishIdAsync(int dishId);
    
    Task UpdateAsync(Post post);
    Task RemoveAsync(Post post);
}