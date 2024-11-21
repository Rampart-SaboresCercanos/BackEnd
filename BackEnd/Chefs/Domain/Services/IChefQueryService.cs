using BackEnd.Chefs.Domain.Model.Aggregates;
using BackEnd.Chefs.Domain.Model.Queries;

namespace BackEnd.Chefs.Domain.Services;

public interface IChefQueryService
{
    /// <summary>
    /// Handle get all chefs query
    /// </summary>
    /// <param name="query">
    /// The <see cref="GetAllChefsQuery"/> query
    /// </param>
    /// <returns>
    /// A collection of <see cref="Chef"/> objects
    /// </returns>
    Task<IEnumerable<Chef>> Handle(GetAllChefsQuery query);
        
    /// <summary>
    /// Handle get chef by id query
    /// </summary>
    /// <param name="query">
    /// The <see cref="GetChefByIdQuery"/> query
    /// </param>
    /// <returns>
    /// A <see cref="Chef"/> if found, otherwise null
    /// </returns>
    Task<Chef?> Handle(GetChefByIdQuery query);
}