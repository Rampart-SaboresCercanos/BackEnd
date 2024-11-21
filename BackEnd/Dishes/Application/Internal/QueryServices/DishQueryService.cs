using BackEnd.Dishes.Domain.Model.Aggregates;
using BackEnd.Dishes.Domain.Model.Queries;
using BackEnd.Dishes.Domain.Repositories;
using BackEnd.Dishes.Domain.services;

namespace BackEnd.Dishes.Application.Internal.QueryServices;

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