namespace BackEnd.Chefs.Interfaces.REST.Resource;

public record ChefResource(
    int Id,
    string Name,
    double Rating,
    bool IsFavorite,
    string Gender
);