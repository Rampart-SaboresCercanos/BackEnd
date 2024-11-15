namespace BackEnd.Chefs.Domain.Model.Command;

public record CreateChefCommand(
    string Name,
    double Rating,
    bool Favorite,
    string Gender);