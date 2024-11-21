namespace BackEnd.Chefs.Interfaces.REST.Resource;

public record CreateChefResource
(
    string Name,
    string Gender,
    double Rating,
    bool IsFavorite
);