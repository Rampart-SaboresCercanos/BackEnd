using Backend.Dishes.Domain.Model.Aggregates;
using Backend.Dishes.Domain.Model.Commands;
using Backend.Dishes.Domain.Repositories;
using Backend.Dishes.Domain.services;
using BackEnd.Shared.Domain.Repositories;

namespace Backend.Dishes.Application.Internal.CommandService;

public class DishCommandService(IDishRepository dishRepository, IUnitOfWork unitOfWork)
    : IDishCommandService
{
    public async Task<Dish?> Handle(CreateDishCommand command)
    {
        var dish = new Dish(command);
        try
        {
            await dishRepository.AddAsync(dish);
            await unitOfWork.CompleteAsync();
            return dish;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<Dish?> Handle(UpdateDishCommand command)
    {
        var dish = await dishRepository.FindByIdAsync(command.Id);
        if (dish == null) return null;

        dish.ChefId = command.ChefId;
        dish.NameOfDish = command.NameOfDish;
        dish.Ingredients = command.Ingredients;
        dish.PreparationSteps = command.PreparationSteps;
        dish.Favorite = command.Favorite;

        try
        {
            dishRepository.Update(dish);
            await unitOfWork.CompleteAsync();
            return dish;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<bool> DeleteDishCommand(int id)
    {
        var dish = await dishRepository.FindByIdAsync(id);
        if (dish == null) return false;

        try
        {
            dishRepository.Remove(dish);
            await unitOfWork.CompleteAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}