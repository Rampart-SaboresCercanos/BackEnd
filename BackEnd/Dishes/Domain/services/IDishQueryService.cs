using BackEnd.Dishes.Domain.Model.Aggregates;
using BackEnd.Dishes.Domain.Model.Queries;

namespace BackEnd.Dishes.Domain.services;

public interface IDishQueryService 
{
    /// <summary>
    /// Handle get all dishes query
    /// </summary>
    /// <param name="query">
    /// The <see cref="GetAllDishesQuery"/> query
    /// </param>
    /// <returns>
    /// A collection of <see cref="Dish"/> objects
    /// </returns>
    Task<IEnumerable<Dish>> Handle(GetAllDishesQuery query);
    
    /// <summary>
    ///  Handle get dish by id query
    /// </summary>
    /// <param name="query">
    /// The <see cref="GetDishByIdQuery"/> query
    /// </param>
    /// <returns>
    ///  A <see cref="Dish"/> if found, otherwise null    
    /// </returns>
    Task<Dish> Handle(GetDishByIdQuery query);
}