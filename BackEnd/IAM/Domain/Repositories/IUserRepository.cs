using BackEnd.IAM.Domain.Model.Aggregates;
using BackEnd.Shared.Domain.Repositories;

namespace BackEnd.IAM.Domain.Repositories;

/// <summary>
/// Represents the user repository. 
/// </summary>
public interface IUserRepository : IBaseRepository<User>
{
    /// <summary>
    /// Finds a user by username. 
    /// </summary>
    /// <param name="username">
    /// The username to search for.
    /// </param>
    /// <returns>
    /// The user if found; otherwise, null.
    /// </returns>
    Task<User?> FindByUsernameAsync(string username);
    
    /// <summary>
    /// Checks if a user with the specified username exists. 
    /// </summary>
    /// <param name="username">
    /// The username to check for.
    /// </param>
    /// <returns>
    /// True if a user with the specified username exists; otherwise, false.
    /// </returns>
    bool ExistsByUsername(string username);

}