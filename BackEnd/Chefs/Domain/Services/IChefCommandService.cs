using BackEnd.Chefs.Domain.Model.Aggregates;
using BackEnd.Chefs.Domain.Model.Command;

namespace BackEnd.Chefs.Domain.Services
{
    public interface IChefCommandService
    {
        Task<Chef?> Handle(CreateChefCommand command);   // Maneja la creación de un chef
        Task<Chef?> Handle(UpdateChefCommand command);   // Maneja la actualización de un chef
        Task<Chef?> Handle(DeleteChefCommand command);   // Maneja la eliminación de un chef
    }
}