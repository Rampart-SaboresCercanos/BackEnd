using BackEnd.Chefs.Domain.Model.Aggregates;
using BackEnd.Chefs.Domain.Model.Queries;

namespace BackEnd.Chefs.Domain.Services
{
    public interface IChefQueryService
    {
        Task<IEnumerable<Chef>> Handle(GetAllChefsQuery query);          // Recupera todos los chefs
        Task<Chef> Handle(GetChefByIdQuery query);                       // Recupera un chef por su ID
        Task<IEnumerable<Chef>> Handle(GetChefsByRatingQuery query);     // Recupera chefs según su calificación
    }
}