using BackEnd.Chefs.Domain.Model.Aggregates;
using BackEnd.Chefs.Domain.Model.Queries;
using BackEnd.Chefs.Domain.Repositories;
using BackEnd.Chefs.Domain.Services;

namespace BackEnd.Chefs.Application.Internal.QueryServices;

public class ChefQueryService(IChefRepository chefRepository) 
    : IChefQueryService
{
    public async Task<IEnumerable<Chef>> Handle(GetAllChefsQuery query)
    {
        return await chefRepository.ListAsync();
    }

    public async Task<Chef?> Handle(GetChefByIdQuery query)
    {
        return await chefRepository.FindByIdAsync(query.Id);
    }
}