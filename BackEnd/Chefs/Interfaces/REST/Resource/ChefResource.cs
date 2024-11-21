namespace BackEnd.Chefs.Interfaces.REST.Resource;

public record ChefResource(
    int Id,
    string Name,
    string Gender,
    double Rating,
    bool IsFavorite
);