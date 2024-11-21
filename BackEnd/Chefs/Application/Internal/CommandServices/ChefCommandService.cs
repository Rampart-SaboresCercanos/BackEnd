using BackEnd.Chefs.Domain.Model.Aggregates;
using BackEnd.Chefs.Domain.Model.Command;
using BackEnd.Chefs.Domain.Repositories;
using BackEnd.Chefs.Domain.Services;
using BackEnd.Shared.Domain.Repositories;

namespace BackEnd.Chefs.Application.Internal.CommandServices;

public class ChefCommandService(IChefRepository chefRepository, IUnitOfWork unitOfWork) 
    : IChefCommandService
{
    public async Task<Chef?> Handle(CreateChefCommand command)
    {
        var chef = new Chef(command);
        try
        {
            await chefRepository.AddAsync(chef);
            await unitOfWork.CompleteAsync();
            return chef;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Chef?> Handle(UpdateChefCommand command)
    {
        var chef = await chefRepository.FindByIdAsync(command.Id);
        if (chef == null)
        {
            throw new Exception("Chef not found");
        }

        chef.Name = command.Name;
        chef.Gender = command.Gender;
        chef.Rating = command.Rating;
        chef.IsFavorite = command.IsFavorite;
        
        try
        {
            chefRepository.Update(chef);
            await unitOfWork.CompleteAsync();
            return chef;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> DeleteChefCommand(int id)
    {
        var chef = await chefRepository.FindByIdAsync(id);
        if (chef == null)
        {
            throw new Exception("Chef not found");
        }

        try
        {
            chefRepository.Remove(chef);
            await unitOfWork.CompleteAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
