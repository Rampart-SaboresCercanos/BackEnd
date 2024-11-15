using BackEnd.Chefs.Domain.Model.Aggregates;
using BackEnd.Chefs.Domain.Model.Command;
using BackEnd.Chefs.Domain.Repositories;
using BackEnd.Chefs.Domain.Services;
using BackEnd.Shared.Domain.Repositories;

namespace BackEnd.Chefs.Application.Internal.CommandServices;

public class ChefCommandService(IChefRepository chefRepository, IUnitOfWork unitOfWork) : IChefCommandService
{
    public async Task<Chef?> Handle(CreateChefCommand command)
    {
        var chef = new Chef(command);
        try
        {
            await chefRepository.AddAsync(chef);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return chef;
    }

    public async Task<Chef?> Handle(UpdateChefCommand command)
    {
        var chef = await chefRepository.FindByIdAsync(command.ChefId);
        if (chef == null)
        {
            throw new Exception("Chef not found");
        }

        if (command.Name != null)
        {
            chef.Name = command.Name;
        }

        if (command.Gender != null)
        {
            chef.Gender = command.Gender;
        }

        if (command.Rating.HasValue)
        {
            chef.Rating = command.Rating.Value;
        }

        if (command.Favorite.HasValue)
        {
            chef.Favorite = command.Favorite.Value;
        }

        try
        {
            await chefRepository.UpdateAsync(chef);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return chef;
    }

    public async Task<Chef?> Handle(DeleteChefCommand command)
    {
        var chef = await chefRepository.FindByIdAsync(command.ChefId);
        if (chef == null)
        {
            throw new Exception("Chef not found");
        }

        try
        {
            await chefRepository.RemoveAsync(chef);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return chef;
    }
}
