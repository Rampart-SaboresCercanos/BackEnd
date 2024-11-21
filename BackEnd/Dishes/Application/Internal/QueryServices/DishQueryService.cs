using Backend.Dishes.Domain.Model.Aggregates;
using Backend.Dishes.Domain.Model.Queries;
using Backend.Dishes.Domain.Repositories;
using Backend.Dishes.Domain.services;

namespace Backend.Dishes.Application.Internal.QueryServices;

public class DishQueryService(IDishRepository dishRepository)
    : IDishQueryService
{
    public async Task<IEnumerable<Dish>> Handle(GetAllDishesQuery query)
    {
        return await dishRepository.ListAsync();
    }

    public async Task<Dish?> Handle(GetDishByIdQuery query)
    {
        return await dishRepository.FindByIdAsync(query.Id);
    }
}