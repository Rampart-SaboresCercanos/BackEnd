namespace BackEnd.Posts.Interfaces.REST.Resource;

public record PostResource(
    int id,
    int dishId,
    DateTime publishDate,
    int stock,
    float pricePerUnit
    );