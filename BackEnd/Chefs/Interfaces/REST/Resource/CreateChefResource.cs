namespace BackEnd.Chefs.Interfaces.REST.Resource;

public record CreateChefResource(
    string Name,
    double InitialRating,
    bool IsFavorite,
    string Gender
);