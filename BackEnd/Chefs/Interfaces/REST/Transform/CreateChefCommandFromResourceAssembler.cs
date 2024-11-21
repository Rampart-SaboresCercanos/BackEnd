using BackEnd.Chefs.Domain.Model.Command;
using BackEnd.Chefs.Interfaces.REST.Resource;

namespace BackEnd.Chefs.Interfaces.REST.Transform;

public class CreateChefCommandFromResourceAssembler
{
    public static CreateChefCommand ToCommandFromResource(CreateChefResource resource)
    {
        return new CreateChefCommand(
            resource.Name,
            resource.Gender,
            resource.Rating,
            resource.IsFavorite
        );
    }
}
