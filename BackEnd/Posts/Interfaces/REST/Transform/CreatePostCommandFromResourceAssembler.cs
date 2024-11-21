using BackEnd.Posts.Domain.Model.Command;
using BackEnd.Posts.Interfaces.REST.Resource;

namespace BackEnd.Posts.Interfaces.REST.Transform;

public class CreatePostCommandFromResourceAssembler
{
    public static CreatePostCommand ToCommandFromResource(CreatePostResource resource)
    {
        return new CreatePostCommand(resource.dishId, resource.publishDate, resource.stock, resource.pricePerUnit);
    }
}