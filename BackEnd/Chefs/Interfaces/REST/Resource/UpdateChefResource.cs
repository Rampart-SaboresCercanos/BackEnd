namespace BackEnd.Chefs.Interfaces.REST.Resource;

public record UpdateChefResource(
    string Name,
    string Gender,
    double Rating,
    bool IsFavorite
);