using BackEnd.Chefs.Domain.Model.Command;
using BackEnd.Chefs.Interfaces.REST.Resource;

namespace BackEnd.Chefs.Interfaces.REST.Transform;

public class UpdateChefCommandFromResourceAssembler
{
    public static UpdateChefCommand ToCommandFromResource(int id, UpdateChefResource resource)
    {
        return new UpdateChefCommand(
            id,
            resource.Name,
            resource.Gender,
            resource.Rating,
            resource.IsFavorite
        );
    }
}