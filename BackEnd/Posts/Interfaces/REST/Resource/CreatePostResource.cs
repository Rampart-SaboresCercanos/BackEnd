namespace BackEnd.Posts.Interfaces.REST.Resource;

public record CreatePostResource(
    int dishId,
    DateTime publishDate,
    int stock,
    float pricePerUnit
    );