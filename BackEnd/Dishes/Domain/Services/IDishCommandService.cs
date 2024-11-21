using Backend.Dishes.Domain.Model.Aggregates;
using Backend.Dishes.Domain.Model.Commands;

namespace Backend.Dishes.Domain.services;

public interface IDishCommandService
{
    /// <summary>
    /// Handle create dish command
    /// </summary>
    /// <param name="command">
    /// A <see cref="CreateDishCommand"/> command
    /// </param>
    /// <returns>
    /// The <see cref="Dish"/>
    /// </returns>
    Task<Dish?> Handle(CreateDishCommand command);
    
    /// <summary>
    /// Handle update dish command
    /// </summary>
    /// <param name="command">
    /// A <see cref="UpdateDishCommand"/> command
    /// </param>
    /// <returns>
    /// The <see cref="Dish"/>
    /// </returns>
    Task<Dish?> Handle(UpdateDishCommand command);
    
    /// <summary>
    /// Handle delete dish command
    /// </summary>
    /// <param name="id">
    /// A <see cref="DeleteDishCommand"/> id
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>
    /// </returns>
    Task<bool> DeleteDishCommand (int id);
}