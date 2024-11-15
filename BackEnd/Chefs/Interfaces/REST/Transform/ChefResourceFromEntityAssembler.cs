using BackEnd.Chefs.Domain.Model.Aggregates;
using BackEnd.Chefs.Interfaces.REST.Resource;

namespace BackEnd.Chefs.Interfaces.REST.Transform
{
    public static class ChefResourceFromEntityAssembler
    {
        public static ChefResource ToResourceFromEntity(Chef entity)
        {
            return new ChefResource(
                entity.Id,
                entity.Name,
                entity.Rating,
                entity.Favorite,
                entity.Gender
            );
        }
    }
}