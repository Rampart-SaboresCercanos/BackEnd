using BackEnd.Chefs.Domain.Model.Aggregates;
using BackEnd.Chefs.Domain.Model.Command;

namespace BackEnd.Chefs.Domain.Services
{
    public interface IChefCommandService
    {
        /// <summary>
        /// Handle create chef command
        /// </summary>
        /// <param name="command">
        /// A <see cref="CreateChefCommand"/> command
        /// </param>
        /// <returns>
        /// The <see cref="Chef"/>
        /// </returns>
        Task<Chef?> Handle(CreateChefCommand command);
        
        /// <summary>
        /// Handle update chef command
        /// </summary>
        /// <param name="command">
        /// A <see cref="UpdateChefCommand"/> command
        /// </param>
        /// <returns>
        /// The <see cref="Chef"/>
        /// </returns>
        Task<Chef?> Handle(UpdateChefCommand command);
        
        /// <summary>
        /// Handle delete chef command
        /// </summary>
        /// <param name="id">
        /// A <see cref="DeleteChefCommand"/> id
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>
        /// </returns>
        Task<bool> DeleteChefCommand(int id);
    }
}