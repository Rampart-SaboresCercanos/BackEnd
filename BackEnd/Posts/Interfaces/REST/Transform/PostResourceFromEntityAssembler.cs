using BackEnd.Posts.Domain.Model.Aggregates;
using BackEnd.Posts.Interfaces.REST.Resource;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BackEnd.Posts.Interfaces.REST.Transform;

public static class PostResourceFromEntityAssembler
{
    public static PostResource ToResourceFromEntity(Post entity)
    {
        return new PostResource(
            entity.id,
            entity.dishId,
            entity.publishDate,
            entity.stock,
            entity.pricePerUnit);
    }
}