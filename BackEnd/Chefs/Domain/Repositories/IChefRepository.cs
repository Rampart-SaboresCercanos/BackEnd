using BackEnd.Chefs.Domain.Model.Aggregates;
using BackEnd.Shared.Domain.Repositories;

namespace BackEnd.Chefs.Domain.Repositories;

public interface IChefRepository : IBaseRepository<Chef>
{
    Task<IEnumerable<Chef>> FindAllAsync();
    Task<Chef?> FindByIdAsync(int id);
    Task<IEnumerable<Chef>> FindByNameAsync(string name);  
    Task<IEnumerable<Chef>> FindByRatingAsync(double rating);  
    Task UpdateAsync(Chef chef);
    Task RemoveAsync(Chef chef);
}