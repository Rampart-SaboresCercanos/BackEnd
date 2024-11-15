namespace BackEnd.Chefs.Domain.Model.Command;

public record UpdateChefCommand(
    string? Name = null,
    double? Rating = null,
    bool? Favorite = null,
    string? Gender = null)
{
    internal int ChefId { get; init; }
}