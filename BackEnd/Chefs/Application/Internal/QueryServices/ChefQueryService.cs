using BackEnd.Chefs.Domain.Model.Aggregates;
using BackEnd.Chefs.Domain.Model.Queries;
using BackEnd.Chefs.Domain.Repositories;
using BackEnd.Chefs.Domain.Services;

namespace BackEnd.Chefs.Application.Internal.QueryServices;

public class ChefQueryService(IChefRepository chefRepository) : IChefQueryService
{
    public async Task<IEnumerable<Chef>> Handle(GetAllChefsQuery query)
    {
        return await chefRepository.FindAllAsync();
    }

    public async Task<Chef> Handle(GetChefByIdQuery query)
    {
        var chef = await chefRepository.FindByIdAsync(query.Id);
        if (chef == null)
        {
            throw new Exception("Chef not found.");
        }

        return chef;
    }

    public async Task<IEnumerable<Chef>> Handle(GetChefsByNameQuery query)
    {
        return await chefRepository.FindByNameAsync(query.Name);
    }

    public async Task<IEnumerable<Chef>> Handle(GetChefsByRatingQuery query)
    {
        return await chefRepository.FindByRatingAsync(query.Rating);
    }
}