using BackEnd.Chefs.Domain.Model.Command;

namespace BackEnd.Chefs.Domain.Model.Aggregates;

public class Chef
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Rating { get; set; }
    public bool Favorite { get; set; }
    public string Gender { get; set; }

    protected Chef() { }

    public Chef(CreateChefCommand command)
    {
        Name = command.Name;
        Rating = command.Rating;
        Favorite = command.Favorite;
        Gender = command.Gender;
    }
}