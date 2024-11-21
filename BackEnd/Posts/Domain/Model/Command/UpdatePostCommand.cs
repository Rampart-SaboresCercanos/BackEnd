namespace BackEnd.Posts.Domain.Model.Command;

public record UpdatePostCommand(
    int? dishId = null,
    DateTime? publishDate = null,
    int? stock = null,
    float? price = null)
{
    internal int PostId { get; init; }
};